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
    // The unlock status of characters
    private bool[] characters = new bool[3];

    public int selectedCharacter;

    public int Candy { get; set; }

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
        //Debug.Log(tutorialDone);
        fish = data.fish;
        Gfish = data.Gfish;
        Candy = data.specialCurrency;

        // reading Upgrade levels
        upgradeLevels = data.upgradesLevels;
        // reading Powerup uses
        powerupUses = data.powerupUses;
        // reading Characters unlock status
        Debug.Log(data.characters);
        characters = data.characters;
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
        SaveSystem.SaveData(fish, Gfish, upgradeLevels, powerupUses, characters, quests, questTime, Candy);
    }

    public void SaveTutorial()
    {
        QuestSaving[] quests = new QuestSaving[3];
        ulong questTime = 0;
        SaveSystem.SaveData(fish, Gfish, upgradeLevels, powerupUses, characters, quests, questTime, Candy, true);
    }

    public int GetUpgradeLevels(Upgrade upgrade)
    {
        //Debug.Log((int)upgrade);
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

    public void AddPowerupUses(Powerup powerup, int value)
    {
        powerupUses[(int)powerup] += value;
    }

    public bool GetCharacterUnlockStatus(int character)
    {
        return characters[character];
    }

    public void UnlockCharacter(int character)
    {
        characters[character] = true;
    }

    public void AddCandy(int value)
    {
        Candy += value;
    }

    public void RemoveCandy(int amount)
    {
        if (Candy >= amount)
            Candy -= amount;
        else
            Debug.Log("Insuficient Candy");
    }

    public void GiveCandy()
    {
        Candy = 300;
    }

    public int Score { get { return score; } set { score = value; } }
    
    public int Fish { get { return fish; } private set { fish = value; } }

    public int GFish { get { return Gfish; } private set { Gfish = value; } }

}
