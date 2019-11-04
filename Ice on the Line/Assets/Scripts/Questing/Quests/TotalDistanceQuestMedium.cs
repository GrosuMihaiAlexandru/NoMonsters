using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalDistanceQuestMedium : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "TotalDistanceQuestMedium";
        // InGame description
        Description = "Reach a total distance of 750 blocks";
        Reward = new ItemReward(375);

        // Add the goals to the list
        Goals.Add(new TotalDistanceGoal("Reach a total distance of 750 blocks", false, 0, 750));
        Goals.ForEach(g => g.Init());
    }
}

