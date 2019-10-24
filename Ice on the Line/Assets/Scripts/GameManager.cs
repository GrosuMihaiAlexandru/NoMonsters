using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // The currencies of the game
    [SerializeField]
    private int fish;
    [SerializeField]
    private int Gfish;

    // Check to display tutorial or not
    [SerializeField]
    public bool tutorialDone;

    // The score of of the game
    [SerializeField]
    private int score = 0;

    public enum Upgrade { scoreMultiplier, temperatureSpeed, snowflake, extrablock}
    public enum Powerup { extrablock, jetpack, freeze}
    // Global Upgrade levels
    [SerializeField]
    private int[] upgradeLevels = new int[4];

    // Powerups uses
    [SerializeField]
    private int[] powerupUses = new int[3];

    void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Reading saved data
        PlayerData data = SaveSystem.LoadData();
        tutorialDone = data.finishedTutorial;
        fish = data.fish;
        Gfish = data.Gfish;

        // reading Upgrade levels
        upgradeLevels = data.upgradesLevels;
        // reading Powerup uses
        powerupUses = data.powerupUses;

    }

    void Start()
    {
        
    }

    public void ReloadData()
    {
        PlayerData data = SaveSystem.LoadData();
        tutorialDone = data.finishedTutorial;
        fish = data.fish;
        Gfish = data.Gfish;
    }

    public void AddFish(int amount)
    {
        fish += amount;
    }

    public void RemoveFish(int amount)
    {
        if (fish >= amount)
            fish -= amount;
        else
            Debug.Log("Insuficient fish");
    }

    public void AddGfish(int amount)
    {
        Gfish += amount;
    }

    public void RemoveGfish(int amount)
    {
        if (Gfish >= amount)
            Gfish -= amount;
        else
            Debug.Log("Insuficient Gfish");
    }

    public void SaveProgress()
    {
        // making an array of quests from the quest list
        QuestSaving[] quests = QuestManager.instance.SaveQuests().ToArray();
        ulong questTime = QuestManager.instance.QuestsAssignedTime;
        SaveSystem.SaveData(fish, Gfish, upgradeLevels, powerupUses, quests, questTime);
    }

    public void SaveTutorial()
    {
        QuestSaving[] quests = QuestManager.instance.SaveQuests().ToArray();
        ulong questTime = QuestManager.instance.QuestsAssignedTime;
        SaveSystem.SaveData(fish, Gfish, upgradeLevels, powerupUses, quests, questTime, true);
    }

    public int GetUpgradeLevels(Upgrade upgrade)
    {
        return upgradeLevels[(int)upgrade];
    }

    public void SetUpgradeLevels(Upgrade upgrade, int value)
    {
        upgradeLevels[(int)upgrade] = value;
    }

    public int GetPowerupUses(Powerup powerup)
    {
        return powerupUses[(int)powerup];
    }

    public void SetPowerupUses(Powerup powerup, int value)
    {
        powerupUses[(int)powerup] = value;
    }
    public int Score { get { return score; } set { score = value; } }
    
    public int Fish { get { return fish; } private set { fish = value; } }

}
