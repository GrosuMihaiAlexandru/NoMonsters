using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalDistanceQuestHard : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "TotalDistanceQuestHard";
        // InGame description
        Description = "Reach a total distance of 1600 blocks";
        Reward = new ItemReward(800);

        // Add the goals to the list
        Goals.Add(new TotalDistanceGoal("Reach a total distance of 1600 blocks", false, 0, 1600));
        Goals.ForEach(g => g.Init());
    }
}

