using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectSnowflakeQuestEasy : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "CollectSnowflakeQuestEasy";
        // InGame description
        Description = "Collect 3 snowflakes";
        Reward = new ItemReward(100);

        // Add the goals to the list
        Goals.Add(new CollectGoal(1, "Collect 3 snowflakes", false, 0, 3));
        Goals.ForEach(g => g.Init());
    }
}

