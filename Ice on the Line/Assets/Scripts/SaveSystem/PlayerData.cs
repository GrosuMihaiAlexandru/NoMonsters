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
    /// Levels for each Upgrade in the game
    /// 0 - scoreMultiplierLevel
    /// 1 - temperatureSpeedLevel
    /// 2 - snowflakeLevel
    /// 3 - extraBlockLevel
    /// </summary>
    public int[] upgradesLevels = new int[4];

    /// <summary>
    /// Uses left for each Powerup
    /// 0 - extraBlock
    /// 1 - jetpack
    /// 2 - freeze
    /// </summary>
    public int[] powerupUses = new int[3];

    public PlayerData(int fish, int Gfish, int[] upgrades, int[] uses, bool tutorial = true)
    {
        this.fish = fish;
        this.Gfish = Gfish;
        finishedTutorial = tutorial;
        upgradesLevels = upgrades;
        powerupUses = uses;
    }
}
