using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int fish;
    public int Gfish;

    public bool finishedTutorial;

    /// <summary>
    /// Levels for each upgrade in the game
    /// 0 - scoreMultiplierLevel
    /// 1 - temperatureSpeedLevel
    /// 2 - snowflakeLevel
    /// 3 - extraBlockLevel
    /// </summary>
    public int[] upgradesLevels = new int[4];

    public PlayerData(int fish, int Gfish, int[] upgrades, bool tutorial = true)
    {
        this.fish = fish;
        this.Gfish = Gfish;
        finishedTutorial = tutorial;
        upgradesLevels = upgrades;
    }
}
