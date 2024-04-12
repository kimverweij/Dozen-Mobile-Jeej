using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public List<GameObject> Screens;

    private GameObject _activeScreen;

    // Singleton instantie
    private static ScreenManager _instance;

    public static ScreenManager Instance
    {
        get
        {
            // Als er nog geen instantie is, zoek er een in de scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScreenManager>();

                // Als er geen instantie is gevonden, maak er een aan
                if (_instance == null)
                {
                    GameObject obj = new GameObject("ScreenManager");
                    _instance = obj.AddComponent<ScreenManager>();
                }
            }

            return _instance;
        }
    }
    public enum ScreenTypeEnum
    {
        // Voeg geen specifieke schermen toe, aangezien we dit dynamisch zullen beheren
        SplashScreen,
        LoadingScreen,
        MainScreen,
        AddPlayers,
        CreateTeams,
        Shop
    }
    // Voeg je ScreenTypeEnum hier toe

    public void GoToScreen(ScreenTypeEnum type)
    {
        foreach (GameObject screenObject in Screens)
        {
            ScreenType screenType = screenObject.GetComponent<ScreenType>();

            if (screenType != null && screenType.Type == type)
            {
                if (_activeScreen != null && _activeScreen != screenObject)
                {
                    Debug.Log("Closing screen: " + _activeScreen.name);
                    _activeScreen.SetActive(false);
                }

                _activeScreen = screenObject;
                Debug.Log("Opening screen: " + _activeScreen.name);
                _activeScreen.SetActive(true);
                return;
            }
        }

        Debug.LogWarning("Screen not found: " + type);
        // Debug.LogWarning("Screen not found: " + type);
    }





    public void GoToScreen(int type)
    {
        GoToScreen((ScreenTypeEnum)type);
    }
}
