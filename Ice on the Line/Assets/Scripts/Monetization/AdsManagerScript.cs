using EasyMobile;
using System.Collections.Generic;
using UnityEngine;

public class AdsManagerScript : MonoBehaviour
{
    private AdMobClientImpl adMobClient;

    [SerializeField]
    private List<RectTransform> thingsToMoveUpWhenBannerAdIsShowing = new List<RectTransform>();

    private void Awake()
    {
        if (!RuntimeManager.IsInitialized())
        {
            RuntimeManager.Init();
            Advertising.GrantDataPrivacyConsent();
        }
        adMobClient = Advertising.AdMobClient;
        adMobClient.OnBannerAdOpening += AdMobClient_OnBannerAdOpening;
        // adMobClient.OnBannerAdClosed += AdMobClient_OnBannerAdClosed;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowBannerAd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Advertising.DestroyBannerAd();
    }

    public void ShowBannerAd()
    {
        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
    }

    private void AdMobClient_OnBannerAdOpening(object sender, System.EventArgs e)
    {
        for(int i = 0; i < thingsToMoveUpWhenBannerAdIsShowing.Count; i++)
        {
            thingsToMoveUpWhenBannerAdIsShowing[i].anchoredPosition += new Vector2(0, GetBannerHeight());
        }
    }

    /*
    private void AdMobClient_OnBannerAdClosed(object sender, System.EventArgs e)
    {
        for (int i = 0; i < thingsToMoveUpWhenBannerAdIsShowing.Count; i++)
        {
            thingsToMoveUpWhenBannerAdIsShowing[i].anchoredPosition -= new Vector2(0, GetBannerHeight());
        }
    }
    */

    public float GetBannerHeight()
    {
        return Mathf.RoundToInt(50 * Screen.dpi / 160);
    }
}
