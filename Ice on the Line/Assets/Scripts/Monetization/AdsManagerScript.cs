using EasyMobile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsManagerScript : MonoBehaviour
{
    private AdMobClientImpl adMobClient;

    [SerializeField]
    private List<RectTransform> thingsToMoveUpWhenBannerAdIsShowing = new List<RectTransform>();

    private bool alreadyMoved = false;

    private void Awake()
    {
        if (!RuntimeManager.IsInitialized())
        {
            RuntimeManager.Init();
            Advertising.GrantDataPrivacyConsent();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        alreadyMoved = false; 

        adMobClient = Advertising.AdMobClient;

        adMobClient.OnBannerAdLoaded += AdMobClient_OnBannerAdLoaded;

        adMobClient.ShowBannerAd(BannerAdPosition.Bottom, BannerAdSize.SmartBanner);
    }

    private void OnDestroy()
    {
        adMobClient.DestroyBannerAd();
        adMobClient.OnBannerAdClosed -= AdMobClient_OnBannerAdLoaded;
    }

    private void AdMobClient_OnBannerAdLoaded(object sender, System.EventArgs e)
    {
        if (alreadyMoved)
            return;

        for (int i = 0; i < thingsToMoveUpWhenBannerAdIsShowing.Count; i++)
        {
            thingsToMoveUpWhenBannerAdIsShowing[i].anchoredPosition += new Vector2(0, adHeightPixels());
        }
        alreadyMoved = true;
    }

    public float adHeightPixels() // workaround until BannerAdSize.SmartBanner.Height starts working (have to write them a bug report)
    {
        int screenHeightPixels = Screen.height;
        float screenHeightDP = 160 * screenHeightPixels / Screen.dpi;

        int adHeightDP;
        if (screenHeightDP > 720)
        {
            // Ad height: 90dp
            adHeightDP = 90;
        }
        else if (screenHeightDP > 400)
        {
            // Ad height: 50dp
            adHeightDP = 50;
        }
        else
        {
            // Ad height: 32dp
            adHeightDP = 32;
        }

        float adHeightPixels = adHeightDP * Screen.dpi / 160;
        return adHeightPixels + 20; // to account for ads sometimes being a bit bigger than expected on Ville's phone :)
    }
}
