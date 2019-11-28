using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CampaignData
{
    public int campaignVersion;
    public List<Level> levels;

    public CampaignData (int campaignVersion, List<Level> levels)
    {
        this.campaignVersion = campaignVersion;
        this.levels = levels;
    }
}
