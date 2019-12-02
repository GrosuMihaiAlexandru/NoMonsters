using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GamePlayedGoal : Goal
{

    public GamePlayedGoal(string description, bool completed, int currentAmount, int requiredAmount) : base(description, completed, currentAmount, requiredAmount) { }

    public override void Init()
    {
        base.Init();
        InGameEvents.OnGameOver += GameOver;
    }

    void GameOver(IPlayer player)
    {
        CurrentAmount++;
        Evaluate();
    }
}
