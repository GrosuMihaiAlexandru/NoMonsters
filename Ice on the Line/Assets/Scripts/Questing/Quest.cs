using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    // Must have the same name as the class which extends Quest
    public string QuestName { get; set; }
    public string Description { get; set; }
    public ItemReward Reward { get; set; }
    public bool Completed { get; set; }

    public void CheckGoals()
    {

        this.Completed = Goals[0].Completed;
        Debug.Log(Goals.Count);
    }

    public void GiveReward()
    {
        Debug.Log("Rewarding player");
        Reward.GiveReward();
    }

}