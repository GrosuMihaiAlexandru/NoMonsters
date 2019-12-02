using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceSnapQuestHard : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "IceSnapQuestHard";
        // InGame description
        Description = "Snap 180 ice blocks";
        Reward = new ItemReward(750);

        // Add the goals to the list
        Goals.Add(new IceSnapGoal("Snap 180 ice blocks", false, 0, 180));
        Goals.ForEach(g => g.Init());
    }
}

