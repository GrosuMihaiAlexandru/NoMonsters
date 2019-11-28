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
    /// 4 - fishMagnet
    /// 5 - bomb
    /// </summary>
    public int[] upgradesLevels;

    /// <summary>
    /// Uses left for each Consumable
    /// 0 - extraBlock
    /// 1 - bomb
    /// 2 - teleport
    /// </summary>
    public int[] powerupUses;

    // the current active quests with their progress
    public QuestSaving[] activeQuests = new QuestSaving[3];
    // Last time the quests were assigned
    public ulong questsAssignedTime;

    // Characters unlocked status
    public bool[] characters;

    // Special Currency
    public int specialCurrency;

    // Last time the lifes were assigned
    public ulong lifeAssignedTime;

    // The amount of retries for campaign levels
    public int lives;

    public PlayerData(int fish, int Gfish, int lives, int[] upgrades, int[] uses, bool[] characters, QuestSaving[] quests, ulong questTime, ulong lifeTime, int specialCurrency = 0,  bool tutorial = true)
    {
        this.fish = fish;
        this.Gfish = Gfish;
        this.lives = lives;
        finishedTutorial = tutorial;
        upgradesLevels = upgrades;
        powerupUses = uses;
        this.characters = characters;
        activeQuests = quests;
        questsAssignedTime = questTime;
        lifeAssignedTime = lifeTime;
        this.specialCurrency = specialCurrency;
    }
}
