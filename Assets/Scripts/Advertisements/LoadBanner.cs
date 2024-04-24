using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoadBanner : MonoBehaviour
{

    public string androidAdUnitId;
    public string iosUnitId;

    string adUnitId;

    BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_IOS
    adUnitId = iosUnitId;
#elif UNITY_ANDROID
    adUnitId = androidAdUnitId;
#endif

       Advertisement.Banner.SetPosition(bannerPosition);
    }

    public void LoadMyBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = onBannerLoaded,
            errorCallback = onBannerLoadedError

        };
        Advertisement.Banner.Load(adUnitId, options);
    }

    void onBannerLoaded()
    {
        print("Banner loaded");
        ShowBanner();
    }
    void onBannerLoadedError(string error)
    {
        print("Banner failed to load " + error);
    }

    public void ShowBanner()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = BannerShow,
            hideCallback = BannerHiden,
            clickCallback = BannerClicked

        };
        Advertisement.Banner.Show(adUnitId, options);
    }

    void BannerShow()
    {

    }

    void BannerClicked()
    {

    }

    void BannerHiden()
    {

    }
    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    
}
