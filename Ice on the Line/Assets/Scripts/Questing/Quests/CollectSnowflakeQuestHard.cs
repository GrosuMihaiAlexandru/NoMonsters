using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectSnowflakeQuestHard : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "CollectSnowflakeQuestHard";
        // InGame description
        Description = "Collect 12 snowflakes";
        Reward = new ItemReward(500);

        // Add the goals to the list
        Goals.Add(new CollectGoal(1, "Collect 12 snowflakes", false, 0, 12));
        Goals.ForEach(g => g.Init());
    }
}

