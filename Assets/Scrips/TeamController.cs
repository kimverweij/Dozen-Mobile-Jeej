using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TeamController : MonoBehaviour
{
    // Start is called before the first frame update

    public Color teamColor; 

    public GameObject coloredSquareBottom;
    public GameObject coloredSquareTop;
    public GameObject playerItem;
    public GameObject playerHolder;
    public GameObject removeButton;

    public GameObject teamNumberImage;

    public Sprite[] teamNumberImageContainer;  

    private int teamNumber =0 ;


    private List<GameObject> playerItemList = new List<GameObject>();
    private List<PlayerItemController> playerItemListController = new List<PlayerItemController>();


    private CreateTeams modifyTeams;

    private void Awake()
    {
    }
    void Start() { }


    public void DefineTeam(int _teamNumber)
    {
        teamNumber = _teamNumber;
        teamColor = GlobalSettings.instance.teamColors[teamNumber];
        // Set correct team number image
        teamNumberImage.GetComponent<Image>().sprite = teamNumberImageContainer[teamNumber];

        // Color bottom and top parts
        coloredSquareBottom.GetComponent<Image>().color = teamColor;
        coloredSquareTop.GetComponent<Image>().color = teamColor;
    }


    public void AddPlayerItems(int maxteamsize, int teamindex)
    {
        // add 5 player slots so they can be used to fill up the teams
        for(int i =0; i< GlobalSettings.instance.maxTeams; i++ )
        {
            GameObject playerItemClone = Instantiate(playerItem, playerHolder.transform);
            playerItemClone.SetActive(false);
            playerItemList.Add(playerItemClone);

            // fill list for player item controller script
            playerItemListController.Add(playerItemList[i].GetComponent<PlayerItemController>());
            playerItemListController[i].SetIndex(teamindex, i);
        }
    }

    public void ShowPlayerItems(int teamindex,  List<string> playerlist)
    {
        for(int i =0;i < GlobalSettings.instance.maxTeams; i++)
        {
            playerItemListController[i].SetIndex(teamindex, i);
            if(i < playerlist.Count)
            {
                playerItemList[i].SetActive(true);
                playerItemList[i].name = playerlist[i];
                playerItemListController[i].SetPlayerName(playerlist[i]);

            }
            else
            {
                playerItemList[i].SetActive(false);
            }
        }
    }

    public void SetModifyTeams(CreateTeams _modifyTeams)
    {
        modifyTeams = _modifyTeams;
    }

   

    public void RemoveTeam()
    {
        modifyTeams.RemoveTeam(teamNumber);
    }

}
