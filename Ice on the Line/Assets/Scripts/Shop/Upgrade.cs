using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Info about each Upgrade  
public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private int upgradeNumber;

    [SerializeField]
    private int baseCost = 100;

    [SerializeField]
    private string upgradeName;

    public int maxLevel = 10;

    public int level = 1;
    public int cost;

    public void Awake()
    {
        PlayerData data = SaveSystem.LoadData();
        switch (upgradeNumber)
        {
            /*
            case 0:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.scoreMultiplier);
                break;
                */
            case 1:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.temperatureSpeed);
                break;
            case 2:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.snowflake);
                break;
            case 3:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.fishMagnet);
                break;
            case 4:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.fishDouble);
                break;
            case 5:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.timeFreeze);
                break;
            default:
                break;
        }
        CalculateCost();
    }

    // Upgrade and update level in GameManager
    public void LevelUpgrade(int fishAmount)
    {
        if (fishAmount >= cost)
        {
            GameManager.instance.RemoveFish(cost);
            level++;
            CalculateCost();
            switch (upgradeNumber)
            {
                /*
                case 0:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.scoreMultiplier, level);
                    break;
                    */
                case 1:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.temperatureSpeed, level);
                    break;
                case 2:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.snowflake, level);
                    break;
                case 3:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.fishMagnet, level);
                    break;
                case 4:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.fishDouble, level);
                    break;
                case 5:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.timeFreeze, level);
                    break;
                default:
                    break;
            }
            GameManager.instance.SaveProgress();
        }
    }
    
    // Calculate the cost for the next Upgrade
    private void CalculateCost()
    {
        Debug.Log(upgradeName + ": " + level);
        cost = (int) (baseCost * Mathf.Pow(1.5f, level));
    }
}
