using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DistanceGoal : Goal
{
    public DistanceGoal(string description, bool completed, int currentAmount, int requiredAmount) : base(description, completed, currentAmount, requiredAmount) { }

    public override void Init()
    {
        base.Init();
        InGameEvents.OnGameOver += DistanceTraveled;
    }

    void DistanceTraveled(IPlayer player)
    {
        CurrentAmount = player.MaxDistance;
        Evaluate();
    }
}
