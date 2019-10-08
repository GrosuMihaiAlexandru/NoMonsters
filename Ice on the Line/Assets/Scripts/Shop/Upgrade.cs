using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Info about each upgrade  
public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private int upgradeNumber;

    [SerializeField]
    private int baseCost = 100;

    [SerializeField]
    private string upgradeName;

    public int level = 1;
    public int cost;

    public void Awake()
    {
        PlayerData data = SaveSystem.LoadData();
        switch (upgradeNumber)
        {
            case 0:
                level = GameManager.instance.ScoreMultiplierLevel;
                break;
            case 1:
                level = GameManager.instance.TemperatureSpeedLevel;
                break;
            case 2:
                level = GameManager.instance.SnowflakeLevel;
                break;
            default:
                break;
        }
        CalculateCost();
    }

    public void LevelUpgrade(int fishAmount)
    {
        if (fishAmount >= cost)
        {
            GameManager.instance.RemoveFish(cost);
            level++;
            CalculateCost();
            switch (upgradeNumber)
            {
                case 0:
                    GameManager.instance.ScoreMultiplierLevel = level;
                    break;
                case 1:
                    GameManager.instance.TemperatureSpeedLevel = level;
                    break;
                case 2:
                    GameManager.instance.SnowflakeLevel = level;
                    break;
                default:
                    break;
            }
            GameManager.instance.SaveProgress();
        }
    }
    
    // Calculate the cost for the next upgrade
    private void CalculateCost()
    {
        cost = (int) (100 * Mathf.Pow(2, level));
    }
}
