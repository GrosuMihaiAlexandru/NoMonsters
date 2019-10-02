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

    // Global upgrade levels
    [SerializeField]
    private int scoreMultiplierLevel;
    [SerializeField]
    private int temperatureSpeedLevel;
    [SerializeField]
    private int snowflakeLevel;

    // Powerup levels
    [SerializeField]
    private int extraBlockLevel;

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
    }

    void Start()
    {
        PlayerData data = SaveSystem.LoadData();
        tutorialDone = data.finishedTutorial;
        fish = data.fish;
        Gfish = data.Gfish;

        scoreMultiplierLevel = data.upgradesLevels[0];
        temperatureSpeedLevel = data.upgradesLevels[1];
        snowflakeLevel = data.upgradesLevels[2];
        extraBlockLevel = data.upgradesLevels[3];
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
        List<int> upgrades = new List<int> { scoreMultiplierLevel, temperatureSpeedLevel, snowflakeLevel, extraBlockLevel };
        SaveSystem.SaveData(fish, Gfish, upgrades.ToArray());
    }

    public void SaveTutorial()
    {
        List<int> upgrades = new List<int> { scoreMultiplierLevel, temperatureSpeedLevel, snowflakeLevel, extraBlockLevel };
        SaveSystem.SaveData(fish, Gfish, upgrades.ToArray(),true);
    }

    public int Score { get { return score; } set { score = value; } }
    
    public int Fish { get { return fish; } private set { fish = value; } }

    public int ScoreMultiplierLevel { get { return scoreMultiplierLevel; } set { scoreMultiplierLevel = value; } }

    public int TemperatureSpeedLevel { get { return temperatureSpeedLevel; } set { temperatureSpeedLevel = value; } }

    public int SnowflakeLevel {  get { return snowflakeLevel; } set { snowflakeLevel = value; } }

    public int ExtraBlockLevel { get { return extraBlockLevel; } set { extraBlockLevel = value; } }

}
