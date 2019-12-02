using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerupGoal : Goal
{
    public int PowerupID { get; set; }

    public PowerupGoal(int powerupID, string description, bool completed, int currentAmount, int requiredAmount) : base(description, completed, currentAmount, requiredAmount)
    {
        this.PowerupID = powerupID;
        RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        InGameEvents.OnPowerupUsed += PowerupUsed;
    }

    void PowerupUsed(Powerup powerup)
    {
        CurrentAmount++;
        Evaluate();
    }
}
