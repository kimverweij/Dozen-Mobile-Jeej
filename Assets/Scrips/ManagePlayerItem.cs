using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagePlayerItem : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textPlayerItem;
    public TMP_InputField inputTextPlayerItem;

    public GameObject closeButton;
    public ManagePlayers managePlayers;

    private bool isAdded;

    private int index;

    private void Awake()
    {
        inputTextPlayerItem.text = string.Empty;
        closeButton.SetActive(false);
        inputTextPlayerItem.onEndEdit.AddListener(InputComplete);

        index = transform.parent.GetSiblingIndex() * managePlayers.MaxcountPerRow + transform.GetSiblingIndex();
    }

    private void InputComplete(string value)
    {

        // het kan zijn dat er een nieuwe invoer is, een bewerkte invoer, geen invoer, en nieuwe maar lege invoer. 
        // nieuwe niet lege invoer.
        // eerst kijken of dit inderdaad zo is. 
        bool isEmpty = string.IsNullOrEmpty(managePlayers.playerNames[index]);

        if(!string.IsNullOrEmpty(value) ) // er was invoer
        { 
            if(isEmpty) // het is nieuwe invoer
            {
                ReactivatePlayerItem(value);
                managePlayers.HandlePlayerNames(true, value, index);
            }
            else // het is geen nieuwe invoer, dus nu updaten we de value
            {
                managePlayers.UpdatePlayerName(value, index);
            }
        }
        else // er was geen invoer, dus of er is iets verwijderd in de reeks, of de laatste van de reeks
        {
            if (isEmpty) // de nieuwe lege invoer was nog niet toegevoegd dus kan gedeactiveerd worden
            {
                ResetPlayerItem();
            }
            else // de nieuwe invoer was wel toegevoegd en moet daarom uit de lijst verwijderd worden
            {
                managePlayers.HandlePlayerNames(false, value, index);
            }
        }
    }


    void Start()
    {

    }
    public void ReactivatePlayerItem(string name)
    {
        gameObject.SetActive(true);
        closeButton.SetActive(true);
        inputTextPlayerItem.text = name; 
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void RemovePlayerItem()
    {
        managePlayers.HandlePlayerNames(false, null, index);
    }

    public void ResetPlayerItem()
    {
        inputTextPlayerItem.text = string.Empty;
        gameObject.SetActive(false);
        closeButton.SetActive(false);
    }

    public void SetInputTextPlayerItem()
    {
        gameObject.SetActive(true);
        inputTextPlayerItem.ActivateInputField(); // This is recommended to ensure that the input field is activated and ready for input
        inputTextPlayerItem.Select();
        inputTextPlayerItem.caretWidth = 2;
        inputTextPlayerItem.MoveTextEnd(false);
    }
}
