using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoadRewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
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
        print("Load rewarded ad");
        Advertisement.Load(adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        if(placementId.Equals(adUnitId))
        {
            print("rewarded loaded");
            ShowAd();
        }
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("rewarded failed to load");
    }


    public void ShowAd()
    {
        print("Showing rewarded");
        Advertisement.Show(adUnitId, this);
    }



    public void OnUnityAdsShowClick(string placementId)
    {
        print("Clicked rewarded");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adUnitId)&&showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            print("rewarded show ad completed");
           
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("rewarded show ad failured");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("rewarded show ad start");
    }
}
