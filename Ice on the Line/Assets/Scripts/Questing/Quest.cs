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
        Completed = Goals.All(g => g.Completed);

    }

    public void GiveReward()
    {
        Debug.Log("Rewarding player");
        Reward.GiveReward();
    }

}