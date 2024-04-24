using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScreenManager;

public class MainScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //   

        Debug.Log("START@@");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenShop()
    {
        Debug.Log("Open Shop");
        ScreenManager.Instance.GoToScreen(ScreenTypeEnum.Shop);
    }

    public void openSite()
    {
        Application.OpenURL("https://www.hetdozenspel.nl/");
    }

    public void CreateGame()
    {
        Debug.Log("Open Create Game");
        ScreenManager.Instance.GoToScreen(ScreenTypeEnum.AddPlayers);
    }

}
