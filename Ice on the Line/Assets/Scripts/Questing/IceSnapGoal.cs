using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IceSnapGoal : Goal
{
    public IceSnapGoal(string description, bool completed, int currentAmount, int requiredAmount) : base(description, completed, currentAmount, requiredAmount){ }

    public override void Init()
    {
        base.Init();
        InGameEvents.OnIceBlockSnap += IceBlockSnapped;
    }

    void IceBlockSnapped()
    {
        CurrentAmount++;
        Evaluate();
    }
}
