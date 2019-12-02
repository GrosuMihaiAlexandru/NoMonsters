using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFishQuestHard : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "CollectFishQuestHard";
        // InGame description
        Description = "Collect 100 fish";
        Reward = new ItemReward(500);

        // Add the goals to the list
        Goals.Add(new CollectGoal(0, "Collect 100 fish", false, 0, 100));
        Goals.ForEach(g => g.Init());
    }
}
