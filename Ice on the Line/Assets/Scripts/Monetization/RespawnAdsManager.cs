using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnAdsManager : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        Advertising.RewardedAdCompleted += Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped += Advertising_RewardedAdSkipped;
    }

    private void OnDestroy()
    {
        Advertising.RewardedAdCompleted -= Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped -= Advertising_RewardedAdSkipped;
    }

    private void Advertising_RewardedAdSkipped(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        text.text = "SKIPPED AD";
    }

    private void Advertising_RewardedAdCompleted(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        text.text = "COMPLETED AD";
    }

    public void WatchAnAdToRespawnPressed()
    {
        Advertising.ShowRewardedAd();
    }
}
