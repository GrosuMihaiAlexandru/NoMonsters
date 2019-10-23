using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGamesQuestEasy : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "PlayGamesQuestEasy";
        // InGame description
        Description = "Play 1 game";
        Reward = new ItemReward(100);

        // Add the goals to the list
        Goals.Add(new GamePlayedGoal("Play 1 Game", false, 0, 1));
        Goals.ForEach(g => g.Init());
    }
}
