using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalDistanceQuestEasy : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "TotalDistanceQuestEasy";
        // InGame description
        Description = "Reach a total distance of 350 blocks";
        Reward = new ItemReward(175);

        // Add the goals to the list
        Goals.Add(new TotalDistanceGoal("Reach a total distance of 350 blocks", false, 0, 350));
        Goals.ForEach(g => g.Init());
    }
}

