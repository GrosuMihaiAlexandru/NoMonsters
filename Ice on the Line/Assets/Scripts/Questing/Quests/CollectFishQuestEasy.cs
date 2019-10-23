using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Example Quest
/// To create a new quest just make a new script that inherits Quest and fill in the following data
/// 
/// Goals:
/// CollectGoal
/// PowerupGoal
/// DistanceGoal
/// TotalDistanceGoal
/// IceSnapGoal
/// GamePlayedGoal
/// </summary>
public class CollectFishQuestEasy : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "CollectFishQuestEasy";
        // InGame description
        Description = "Collect 25 fish";
        Reward = new ItemReward(100);
        
        // Add the goals to the list
        Goals.Add(new CollectGoal(0, "Collect 25 fish", false, 0, 25));
        Goals.ForEach(g => g.Init());
    }
}

