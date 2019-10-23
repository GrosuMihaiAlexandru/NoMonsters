using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGamesQuestMedium : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "PlayGamesQuestMedium";
        // InGame description
        Description = "Play 3 game";
        Reward = new ItemReward(225);

        // Add the goals to the list
        Goals.Add(new GamePlayedGoal("Play 3 Game", false, 0, 3));
        Goals.ForEach(g => g.Init());
    }
}
