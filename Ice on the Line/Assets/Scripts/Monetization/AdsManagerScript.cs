using EasyMobile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsManagerScript : MonoBehaviour
{
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
        Advertising.ShowBannerAd(BannerAdPosition.Top);
    }

    private void OnDestroy()
    {
        Advertising.DestroyBannerAd();
    }


    /*
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
    */
}
