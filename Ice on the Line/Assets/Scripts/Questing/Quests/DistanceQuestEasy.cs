using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceQuestEasy : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "DistanceQuestEasy";
        // InGame description
        Description = "Reach a distance of 60 blocks";
        Reward = new ItemReward(100);

        // Add the goals to the list
        Goals.Add(new DistanceGoal("Reach a distance of 60 blocks", false, 0, 60));
        Goals.ForEach(g => g.Init());
    }
}

