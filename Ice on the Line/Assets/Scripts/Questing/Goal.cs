using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goal
{
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public Goal(string description, bool completed, int currentAmount, int requiredAmount)
    {
        Description = description;
        Completed = completed;
        CurrentAmount = currentAmount;
        RequiredAmount = requiredAmount;
    }

    public virtual void Init()
    {
        // default init
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
            Complete();
    }

    public void Complete()
    {
        Completed = true;
        Debug.Log("Goal completed");
    }
}
