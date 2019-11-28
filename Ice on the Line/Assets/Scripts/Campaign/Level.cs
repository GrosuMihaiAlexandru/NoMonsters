using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int ID { get; set; }
    public string LevelName { get; set; }
    public bool Completed { get; set; }
    public int Stars { get; set; }
    public bool Locked { get; set; }
    public int AwardsGiven { get; set; }

    public Level (int id, string levelName, bool completed, int stars, bool locked)
    {
        this.ID = id;
        this.LevelName = levelName;
        this.Completed = completed;
        this.Stars = stars;
        this.Locked = locked;
        GiveRewards(stars);
    }

    public void Complete()
    {
        this.Completed = true;
    }

    public void Complete(int stars)
    {
        if (this.Stars < stars)
        {
            this.Completed = true;
            this.Stars = stars;
        }   
    }

    public void Lock()
    {
        this.Locked = true;
    }

    public void Unlock()
    {
        this.Locked = false;
    }

    public void GiveRewards(int stars)
    {
        if (this.AwardsGiven < stars)
            AwardsGiven = stars;
    }
}
