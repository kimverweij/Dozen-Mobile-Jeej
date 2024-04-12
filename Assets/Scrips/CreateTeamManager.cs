using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScreenManager;

public class CreateTeamManager : MonoBehaviour
{
    public CreateTeams createTeams;
    public ManagePlayers managePlayers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToAddPlayers()
    {
        Debug.Log("BackToAddPlayers");
        // pass new team list
        GlobalSettings.instance.PlayerNames = createTeams.FormatTeamList();
        ScreenManager.Instance.GoToScreen(ScreenTypeEnum.AddPlayers);
        managePlayers.UpdateInformation();
    }
}
