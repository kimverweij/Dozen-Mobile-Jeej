using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class FormatTeams : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public List<List<string>> SetTeams(List<string> playerList)
    {
        // Calculate the number of teams (between 2 and 5) and distribute players more evenly
        int numTeams = Mathf.Clamp(playerList.Count / 2, GlobalSettings.instance.minTeams, GlobalSettings.instance.maxTeams);

        // Your team formatting logic here
        List <List<string>> teams = FormTeams(playerList, numTeams);

        return teams;
    }


    List<List<string>> FormTeams(List<string> playerList, int numTeams)
    {
        // Your team formatting logic here
        List<List<string>> teams = new List<List<string>>();

        for (int i = 0; i < numTeams; i++)
        {
            teams.Add(new List<string>());
        }

        int playersAssigned = 0;

        while (playersAssigned < playerList.Count)
        {
            // Add players to teams in a round-robin fashion
            for (int i = 0; i < numTeams && playersAssigned < playerList.Count; i++)
            {
                teams[i].Add(playerList[playersAssigned]);
                playersAssigned++;
            }
        }

        return teams;
    }

    public void ReformTeams(List<List<string>> teamLists, List<string>playerNames)
    {
        // Distribute players to other teams
        foreach (var player in playerNames)
        {
            Debug.Log(player+ " = playername");
            // Find the team with the fewest players and add the player
            int targetTeamIndex = FindTeamWithFewestPlayers(teamLists);
            teamLists[targetTeamIndex].Add(player);
        }
    }

    private int FindTeamWithFewestPlayers(List<List<string>> teamLists)
    {
        int minPlayers = int.MaxValue;
        int targetTeamIndex = -1;

        for (int i = 0; i < teamLists.Count; i++)
        {
            if (teamLists[i].Count < minPlayers)
            {
                minPlayers = teamLists[i].Count;
                targetTeamIndex = i;
            }
        }
        return targetTeamIndex;
    }

    public void AddAndReformteams(List<List<string>> teams)
    {
        List<string> newTeamPlayers = new List<string>();

        // Calculate the ideal team size (spread as evenly as possible with max one player difference)
        int totalPlayers = teams.SelectMany(t => t).Count(); // Total players in the existing teams
        int numTeams = teams.Count + 1;
        int idealTeamSize = totalPlayers / numTeams;

        // Sort teams by size (ascending order)
        var sortedTeams = teams.OrderBy(t => t.Count).ToList();

        // Iterate through the teams and take the last player based on the ideal team size
        foreach (var team in sortedTeams)
        {
            if(idealTeamSize < team.Count)
            {
                newTeamPlayers.Add(team.Last());
                team.RemoveAt(team.Count-1);
            }
        }

        // Add the new team to the list
        teams.Add(newTeamPlayers);
    }

    public void PrintTeams(List<List<string>> teams)
    {
        // Your team printing logic here
        for (int i = 0; i < teams.Count; i++)
        {
            Debug.Log($"Team {i + 1}: {string.Join(", ", teams[i].ToArray())}");
        }
    }

    public void Shuffle2DimensionalList(List<List<string>> list)
    {
        System.Random random = new System.Random();

        // Flatten the 2D list into a single list
        List<string> flattenedList = list.SelectMany(sublist => sublist).ToList();

        // Fisher-Yates shuffle algorithm for the flattened list
        int flattenedCount = flattenedList.Count;
        while (flattenedCount > 1)
        {
            flattenedCount--;
            int k = random.Next(flattenedCount + 1);
            string value = flattenedList[k];
            flattenedList[k] = flattenedList[flattenedCount];
            flattenedList[flattenedCount] = value;
        }

        // Distribute the shuffled names back into sublists
        int currentIndex = 0;
        foreach (List<string> sublist in list)
        {
            int sublistCount = sublist.Count;
            for (int i = 0; i < sublistCount; i++)
            {
                sublist[i] = flattenedList[currentIndex++];
            }
        }
        
    }


    // Check if a new team can be added (do we have enough players to fill each team with at least 2 players?)
    public bool CanFormTeams(int totalPlayers, int newTeamCount)
    {
        return totalPlayers >= GlobalSettings.instance.minTeams * newTeamCount;
    }

    /*
    public bool CanRemoveTeam(int totalPlayers, int currentTeamCount)
    {
        Debug.Log(totalPlayers + " total players en current team count " + currentTeamCount);
        bool canremove = (totalPlayers + GlobalSettings.instance.maxTeams - currentTeamCount) % currentTeamCount == 0 && (totalPlayers / currentTeamCount) <= GlobalSettings.instance.maxTeams;
        // Ensure that removing a team and redistributing its players doesn't exceed the maximum limit per team
        return canremove;
    }
    */

    public bool CanRemoveTeam(int totalPlayers, int currentTeamCount)
    {
        Debug.Log(totalPlayers + " total players en current team count " + currentTeamCount);

        // Bereken het aantal teams na verwijdering van een team
        int remainingTeams = currentTeamCount - 1;

        // Controleer of er na verwijdering niet meer dan 5 spelers in een team zijn
        bool noMoreThanFivePlayers = totalPlayers / remainingTeams <= GlobalSettings.instance.maxPpTeam;

        // Controleer of er tussen 2 en 5 teams zijn na verwijdering
        bool betweenTwoAndFiveTeams = remainingTeams >= GlobalSettings.instance.minTeams && remainingTeams <= GlobalSettings.instance.maxTeams;

        // Retourneer true als aan beide voorwaarden is voldaan, anders false
        return noMoreThanFivePlayers && betweenTwoAndFiveTeams;
    }


}



