using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TotalDistanceGoal : Goal
{
    public TotalDistanceGoal(string description, bool completed, int currentAmount, int requiredAmount) : base(description, completed, currentAmount, requiredAmount) { }

    public override void Init()
    {
        base.Init();
        InGameEvents.OnGameOver += TotalDistanceTraveled;
    }

    void TotalDistanceTraveled(IPlayer player)
    {
        CurrentAmount += player.Distance;
        Evaluate();
    }
}
