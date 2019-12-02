using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectSnowflakeQuestMedium : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "CollectSnowflakeQuestMedium";
        // InGame description
        Description = "Collect 7 snowflakes";
        Reward = new ItemReward(225);

        // Add the goals to the list
        Goals.Add(new CollectGoal(1, "Collect 7 snowflakes", false, 0, 7));
        Goals.ForEach(g => g.Init());
    }
}

