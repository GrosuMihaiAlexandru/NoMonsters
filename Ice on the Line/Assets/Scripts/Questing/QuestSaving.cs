using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for saving the quest state
[System.Serializable]
public class QuestSaving
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public ItemReward Reward { get; set; }
    public bool Completed { get; set; }

    public QuestSaving(List<Goal> goals, string questName, string description, ItemReward reward, bool completed)
    {
        this.Goals = goals;
        this.QuestName = questName;
        this.Description = description;
        this.Reward = reward;
        this.Completed = completed;
    }
}
