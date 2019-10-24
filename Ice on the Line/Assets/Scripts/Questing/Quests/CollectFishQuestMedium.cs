using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFishQuestMedium : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "CollectFishQuestMedium";
        // InGame description
        Description = "Collect 50 fish";
        Reward = new ItemReward(225);

        // Add the goals to the list
        Goals.Add(new CollectGoal(0, "Collect 50 fish", false, 0, 50));
        Goals.ForEach(g => g.Init());
    }
}
