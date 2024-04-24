using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ManagePlayers : MonoBehaviour
{
    public CreateGameScreenManager createGameScreenManager;

    private float addPlayerButtonTimeout = 0.15f;

    public List<string> prefinedPlayerNames = new List<string>()
    {
        "John",
        "Jane",
        "Michael",
        "Emma",
        "James",
        "Olivia",
        "William",
        "Ava",
        "Alexander",
        "Sophia",
        "Robert",
        "Emily",
        "David",
        "Mia",
        "Matthew",
        "Isabella",
        "Daniel",
        "Charlotte",
        "Andrew",
        "Chloe",
        "Ethan",
        "Amelia",
        "Joseph",
        "Evelyn",
        "Samuel"
    }; // list with predefined playernames,

    public List<GameObject> playerRows = new List<GameObject>();



    private int maxPlayers = 25;
    public int MaxcountPerRow = 5;



    public List<string> playerNames = new List<string>(new string[25]);
    public int playerCount = 0;

    // Update is called once per frame

    private void Start()
    {
        playerRows[0].SetActive(true);
    }
    void Update()
    {
    }

    public void UpdateInformation()
    {
        updatePlayerList();
        DisplayPlayerItems();
        createGameScreenManager.SetHint(playerCount);
    }

    private void updatePlayerList()
    {
        for (int i = 0; i < playerNames.Count; i++)
        {
            if (i < GlobalSettings.instance.PlayerNames.Count)
            {
                playerNames[i] = GlobalSettings.instance.PlayerNames[i];
            }
            else
            {
                playerNames[i] = null;
            }
        }
    }



    public void HandlePlayerNames(bool add, string name, int index)
    {
        // Debug.Log("Call to handle HandlePlayerNames, current playercount is " + playerCount );
        if (add)
        {
            // Debug.Log("Add Player " + name + " at index: " + index);
            playerNames[index] = name;
            playerCount++;
        }
        else
        {
            // de invoer dient verwijderd te worden, verwijder de huidige index en voeg aan het eind weer toe, zo ontstaan er geen gaten.
            playerNames.RemoveAt(index);
            playerNames.Add(null);
            playerCount--;
            DisplayPlayerItems();
        }

        createGameScreenManager.SetHint(playerCount);
    }


    private void DisplayPlayerItems()
    {
        // door deze verschuiving kan het zijn dat niet alle player items zijn ge(de)activeerd. we moeten kijken of
        for (int i = 0; i < playerNames.Count; i++) // we loopen maximaal door alle playernames heen
        {
            // we malken ook 2 vars aan voor de row en col om de juist child aan te spreken
            int row = i / playerRows.Count; // Integer deling om de rij-index te krijgen
            int col = i % playerRows.Count;

            if (String.IsNullOrEmpty(playerNames[i])) // als er dus geen waarde/value is toegekent aan deze index in de namenlijst, kunnen we dat item deactiveren. 
            {
                playerRows[row].transform.GetChild(col).GetComponent<ManagePlayerItem>().ResetPlayerItem(); // deactiveer item
            }
            else // er is waarde toegekent dus willen we dit graag laten zien 
            {
                playerRows[row].transform.GetChild(col).GetComponent<ManagePlayerItem>().ReactivatePlayerItem(playerNames[i]);
            }
        }
    }

    public void UpdatePlayerName(string name, int index)
    {
        playerNames[index] = name;
    }

    public void addPlayerItem()
    {
        int row = playerCount / playerRows.Count;
        int posInRow = playerCount % playerRows.Count;

        playerRows[row].SetActive(true);
        playerRows[row].transform.GetChild(posInRow).GetComponent<ManagePlayerItem>().SetInputTextPlayerItem();
        StartCoroutine(timeOutAddButton());
    }

    IEnumerator timeOutAddButton()
    {
        createGameScreenManager.addPlayerButton.interactable = false; // Disable the button temporarily

        // Add player item code
        yield return new WaitForSeconds(addPlayerButtonTimeout);

        createGameScreenManager.addPlayerButton.interactable = true; // Enable the button again after the timeout
    }
    


    public List<string> PassPlayerNames()
    {
        // orginele list bestaat uit null waardes en 25 count lang, global list heeft de size van aantal spelers. 
        List<string> tempPlayerNames=  new List<string>(); // leeg maken zodat hij weer opnieuw gevuld kan worden.
        for (var i =0; i < playerNames.Count; i++)
        {
            if (!String.IsNullOrEmpty(playerNames[i]))
            {
                tempPlayerNames.Add(playerNames[i]);
            }
            else
            {
                break;
            }
        }
        return tempPlayerNames;
    }
}
