using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceQuestHard : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "DistanceQuestHard";
        // InGame description
        Description = "Reach a distance of 320 blocks";
        Reward = new ItemReward(650);

        // Add the goals to the list
        Goals.Add(new DistanceGoal("Reach a distance of 320 blocks", false, 0, 320));
        Goals.ForEach(g => g.Init());
    }
}

