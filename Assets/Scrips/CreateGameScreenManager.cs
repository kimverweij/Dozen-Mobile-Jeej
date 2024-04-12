using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ScreenManager;

public class CreateGameScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    


    private int minPlayerCount   = 4;
    private int maxPlayerCount   = 25;

    public TextMeshProUGUI textHint;

    public Button addPlayerButton;
    public Button nextButton;

    public ManagePlayers managePlayers;


    void Start()
    {
        nextButton.interactable      = false;
        addPlayerButton.interactable = true;
    }
    public void SetHint(int count)
    {
        if (count < minPlayerCount)
        {
            textHint.text = "Voeg minimaal " + minPlayerCount + " spelers toe";
            nextButton.interactable = false;
        }
         else if (count == maxPlayerCount)
        {
            textHint.text = "Maximaal aantal spelers zijn toegevoegd";
            addPlayerButton.interactable = false;
        } else{
            textHint.text = "Je kunt maximaal 25 spelers toevoegen";
            addPlayerButton.interactable = true;
            nextButton.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveCreateGame()
    {
        Debug.Log("Leave Create Game");
        ScreenManager.Instance.GoToScreen(ScreenTypeEnum.MainScreen);
    }

    public void AccessCreateTeams()
    {
        Debug.Log("Leave Create Game");
        GlobalSettings.instance.PlayerNames = managePlayers.PassPlayerNames();
        ScreenManager.Instance.GoToScreen(ScreenTypeEnum.CreateTeams);
    }

}
