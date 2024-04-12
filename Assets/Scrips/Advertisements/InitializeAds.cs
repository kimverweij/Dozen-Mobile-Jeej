using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    // Start is called before the first frame update
    public string androidGameId;
    public string iosGameId;

    public bool isTestingMode = true;

    string gameId;

    private void Awake()
    {
        InitializeMyAds();
    }
    public void OnInitializationComplete()
    {
        print("Ads intitialized!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print("Failed to initialize!!");
    }

    void InitializeMyAds()
    {
        #if UNITY_IOS
            gameId = iosGameId;
        #elif UNITY_ANDROID
                gameId = androidGameId;
        #elif UNITY_EDITOR
                gameId = androidGameId; // for testing
        #endif

        if(!Advertisement.isInitialized&&Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTestingMode, this);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
