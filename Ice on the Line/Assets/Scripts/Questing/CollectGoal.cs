using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectGoal : Goal
{
    public int CollectibleID { get; set; }

    public CollectGoal(int collectibleID, string description, bool completed, int currentAmount, int requiredAmount) : base(description, completed, currentAmount, requiredAmount)
    {
        this.CollectibleID = collectibleID;
    }

    public override void Init()
    {
        base.Init();
        InGameEvents.OnItemCollected += ItemCollected;
    }

    void ItemCollected(ICollectible collectible)
    {
        if (collectible.ID == this.CollectibleID)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
