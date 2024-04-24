using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoadIntersitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    public string androidAdUnitId;
    public string iosUnitId;

    string adUnitId;

    private void Awake()
    {

#if UNITY_IOS
    adUnitId = iosUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif
    }

    public void LoadAd()
    {
        print("Load intersittial ad");
        Advertisement.Load(adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        print("intersittial loaded");
        ShowAd();
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("intersittial failed to load");
    }


    public void ShowAd()
    {
        print("Showing Ad");
        Advertisement.Show(adUnitId, this);
    }

  

    public void OnUnityAdsShowClick(string placementId)
    {
        print("Clicked Ad");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        print("Intersitial show ad completed");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("Intersitial show ad failured");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Intersitial show ad start");
    }


}
