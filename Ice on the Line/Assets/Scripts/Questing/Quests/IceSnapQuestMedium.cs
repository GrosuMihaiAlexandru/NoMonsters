using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceSnapQuestMedium : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "IceSnapQuestMedium";
        // InGame description
        Description = "Snap 100 ice blocks";
        Reward = new ItemReward(350);

        // Add the goals to the list
        Goals.Add(new IceSnapGoal("Snap 100 ice blocks", false, 0, 100));
        Goals.ForEach(g => g.Init());
    }
}

