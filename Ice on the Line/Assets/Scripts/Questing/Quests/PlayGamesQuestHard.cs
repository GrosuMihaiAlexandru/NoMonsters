using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGamesQuestHard : Quest
{
    void Start()
    {
        // Name of the quest has to be the same as the class name
        QuestName = "PlayGamesQuestHard";
        // InGame description
        Description = "Play 5 game";
        Reward = new ItemReward(500);

        // Add the goals to the list
        Goals.Add(new GamePlayedGoal("Play 5 Game", false, 0, 5));
        Goals.ForEach(g => g.Init());
    }
}