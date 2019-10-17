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

    public int level = 1;
    public int cost;

    public void Awake()
    {
        PlayerData data = SaveSystem.LoadData();
        switch (upgradeNumber)
        {
            case 0:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.scoreMultiplier);
                break;
            case 1:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.temperatureSpeed);
                break;
            case 2:
                level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.snowflake);
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
                case 0:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.scoreMultiplier, level);
                    break;
                case 1:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.temperatureSpeed, level);
                    break;
                case 2:
                    GameManager.instance.SetUpgradeLevels(GameManager.Upgrade.snowflake, level);
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
        cost = (int) (100 * Mathf.Pow(2, level));
    }
}
