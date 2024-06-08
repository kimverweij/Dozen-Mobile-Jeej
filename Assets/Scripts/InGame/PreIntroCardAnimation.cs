using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class PreIntroCardAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform cardTransform;
    public Vector2 cardStartPositionOffscreen = new Vector2(20f, 0f);
    private Vector3 cardPositionOnscreen;

    public float moveDuration = 0.5f; // Duur van de verplaatsingsanimatie
    public float scaleDuration = 0.2f; // Duur van de schaalanimatie
    public float scaleFactor = 1.4f;
    public float scaleStartDelay = 0.3f;
    public float pathHeight = 3f;



    public RectTransform TurnBoardTransform;
    public float moveDurationTurnBoard = 0.3f;
    public float turnInfoBoardYOffscreen = 8f;
    private Vector3 turninfoPosOnScreen;



    void Start()
    {
        cardPositionOnscreen = cardTransform.position;


        turninfoPosOnScreen = TurnBoardTransform.position;
        
        AnimateCard(); 
    }

    public void AnimateCard()
    {

        cardTransform.position = cardStartPositionOffscreen;
        cardTransform.localScale = Vector3.one * scaleFactor;

        TurnBoardTransform.position = new Vector3( 0,turnInfoBoardYOffscreen, 0);
        // Bepaal de controlepunten voor de gebogen beweging
        Vector3[] path = new Vector3[]
        {
            new Vector3(cardStartPositionOffscreen.x, 0f, 0f), // Startpunt net buiten het scherm
            new Vector3((cardStartPositionOffscreen.x + cardPositionOnscreen.x) / 2, pathHeight, 0f), // Controlepunt voor de curve
            new Vector3(cardPositionOnscreen.x, cardPositionOnscreen.y, 0f) // Eindpunt op het scherm
        };

        // Maak een nieuwe sequence
        Sequence sequence = DOTween.Sequence();

        // Voeg de schaalanimatie toe
        sequence.Append(cardTransform.DOScale(scaleFactor, scaleDuration).SetEase(Ease.InOutQuad));

        // Voeg de gebogen beweging toe
        sequence.Join(cardTransform.DOPath(path, moveDuration, PathType.CatmullRom).SetEase(Ease.OutQuad));

        // Voeg de terugkeer naar normale grootte toe na de gebogen beweging
        sequence.Join(cardTransform.DOScale(1f, moveDuration - scaleDuration).SetEase(Ease.InOutQuad).SetDelay(scaleDuration));

        sequence.Join(TurnBoardTransform.DOMoveY(turninfoPosOnScreen.y, moveDurationTurnBoard).SetEase(Ease.InSine));

        // Start de Sequence
        sequence.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
