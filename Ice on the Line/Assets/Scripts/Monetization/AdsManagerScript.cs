using EasyMobile;
using UnityEngine;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        // Advertising.ShowBannerAd(BannerAdPosition.Bottom);
        if (Advertising.IsRewardedAdReady())
        {
            print("rewarded ad ready");
        }
    }

    public void ShowBannerAd()
    {
        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
    }

    public void ShowRewardedAd()
    {
        if (Advertising.IsRewardedAdReady())
        {
            Advertising.ShowRewardedAd();
        }
    }
}
