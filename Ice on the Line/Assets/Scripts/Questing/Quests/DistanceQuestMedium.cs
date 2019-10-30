using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceQuestMedium : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "DistanceQuestMedium";
        // InGame description
        Description = "Reach a distance of 150 blocks";
        Reward = new ItemReward(275);

        // Add the goals to the list
        Goals.Add(new DistanceGoal("Reach a distance of 150 blocks", false, 0, 150));
        Goals.ForEach(g => g.Init());
    }
}

