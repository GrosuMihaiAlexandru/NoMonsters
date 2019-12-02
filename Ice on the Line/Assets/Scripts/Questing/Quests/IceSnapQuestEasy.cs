using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceSnapQuestEasy : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "IceSnapQuestEasy";
        // InGame description
        Description = "Snap 40 ice blocks";
        Reward = new ItemReward(150);

        // Add the goals to the list
        Goals.Add(new IceSnapGoal("Snap 40 ice blocks", false, 0, 40));
        Goals.ForEach(g => g.Init());
    }
}

