using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateTeams : MonoBehaviour
{
    public List<List<string>> teamLists;

    public GameObject teamPannelToClone;
    public GameObject AddTeamButton;

    // instantiate format teams
    private FormatTeams teamFormatter;
    
    private List<GameObject> teamPanels = new List<GameObject>();
    private List<TeamController> teamControllers = new List<TeamController>();

    private int currentTeams;


    // Start is called before the first frame update

    void Start()
    {
        DisplayTeamsInformation();
    }


    private void OnEnable()
    {
        Debug.Log("Ik wordt aangezet!!");
        teamLists = teamFormatter.SetTeams(GlobalSettings.instance.PlayerNames);
        // print team list
        teamFormatter.PrintTeams(teamLists);
        ReCreateTeams();
        DisplayAddTeamButton();
    }

    private void printList()
    {
        for (int i = 0; i < GlobalSettings.instance.PlayerNames.Count; i++)
        {
            Debug.Log(GlobalSettings.instance.PlayerNames[i]);
        }
    }

    public void DisplayTeamsInformation()
    {
        List<string> playerNames = GlobalSettings.instance.PlayerNames;

        teamFormatter = gameObject.AddComponent<FormatTeams>();

        // format teams
        teamLists = teamFormatter.SetTeams(playerNames);
        teamFormatter.Shuffle2DimensionalList(teamLists);
        // print team list
        teamFormatter.PrintTeams(teamLists);


        // fill teams

        InitiateTeams();
        ReCreateTeams();
        DisplayAddTeamButton();
        printList();
    }

    private void InitiateTeams()
    {
        int teamsNeeded = GlobalSettings.instance.maxTeams ;
        for (int i = 0; i < teamsNeeded; i++)
        {
            GameObject teamPanel = Instantiate(teamPannelToClone, transform);
            teamPanels.Add(teamPanel);
            TeamController teamController = teamPanel.GetComponent<TeamController>();
            teamController.AddPlayerItems(GlobalSettings.instance.maxTeams, i);
            teamController.DefineTeam(i);
            teamPanel.SetActive(i <= teamLists.Count - 1);
            teamController.SetModifyTeams(this);
            teamControllers.Add(teamController);
        }
    }

    private void ReCreateTeams()
    {
        bool canRemoveTeam = teamFormatter.CanRemoveTeam(teamLists.SelectMany(innerlist => innerlist).Count(), teamLists.Count);

        // make teams needed visible.
        int teamsNeeded = GlobalSettings.instance.maxTeams;
        for (int i = 0; i < teamsNeeded; i++)
        {
            teamPanels[i].SetActive(i < teamLists.Count);

                // Pass the count of players for the current team
                teamControllers[i].DefineTeam(i);
                teamControllers[i].removeButton.SetActive(canRemoveTeam);
                teamControllers[i].removeButton.GetComponent<Button>().interactable = canRemoveTeam;

               if(i < teamLists.Count)
                {
                    teamControllers[i].ShowPlayerItems(i, teamLists[i]);
                }
            
        }
        DisplayAddTeamButton();
    }

    public void ShuffleTeam()
    {
        teamFormatter.Shuffle2DimensionalList(teamLists);
        DisplayPlayerNames();
    }
    public void SwapPlayers(GameObject playerA, GameObject playerB)
    {
        PlayerItemController playerAItemSettings = playerA.GetComponent<PlayerItemController>();

        PlayerItemController playerBItemSettings = playerB.GetComponent<PlayerItemController>();

        int IndexTeamA     = playerAItemSettings.getTeamIndex();
        int IndexPlayerA   = playerAItemSettings.getPlayerIndex();
        string playerAName = teamLists[IndexTeamA][IndexPlayerA]; 


        int IndexTeamB     = playerBItemSettings.getTeamIndex();
        int IndexPlayerB   = playerBItemSettings.getPlayerIndex();
        string playerBName = teamLists[IndexTeamB][IndexPlayerB]; 

        teamLists[IndexTeamA][IndexPlayerA] = playerBName;
        teamLists[IndexTeamB][IndexPlayerB] = playerAName;

        teamFormatter.PrintTeams(teamLists);
        DisplayPlayerNames();
    }

    public void DisplayPlayerNames()
    {
        for (int i = 0; i < teamLists.Count; i++)
        {
            teamControllers[i].ShowPlayerItems(i,teamLists[i]);
        }
    }

    public void DisplayAddTeamButton()
    {
        bool setTeamButtonInteractive = teamFormatter.CanFormTeams(teamLists.SelectMany(innerlist => innerlist).Count(), teamLists.Count + 1);
        bool showTeamButton = teamLists.Count != GlobalSettings.instance.maxTeams;
        // Show add team button if less then 5 (max teams) are created 

        AddTeamButton.SetActive(showTeamButton);
        AddTeamButton.GetComponent<Button>().interactable = setTeamButtonInteractive;
    }
    
    public void AddTeam()
    {
        teamFormatter.AddAndReformteams(teamLists);
        teamFormatter.PrintTeams(teamLists);
        ReCreateTeams();
    }

    public void RemoveTeam(int _teamnumber)
    {
        List<String> _team = teamLists[_teamnumber];
        teamLists.RemoveAt(_teamnumber);

        // restructure teams and pass new team amount.
        teamFormatter.ReformTeams(teamLists, _team);
        ReCreateTeams();
    }

    public List<String> FormatTeamList()
    {
        List<String> tempPlayerList = new List<String>();
        // teams might be removed or people might have switched position
        for(int i = 0;i < teamLists.Count; i++)
        {
            for(int j = 0;j < teamLists[i].Count;j++)
            {
                tempPlayerList.Add(teamLists[i][j]);
            }
        }
        return tempPlayerList;
    }

}
