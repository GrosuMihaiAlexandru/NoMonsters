using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    public GameObject fishMagnet;

    //Upgrade Levels
    private int fishMagnetLevel;
    private int fishDoublerLevel;
    private int timeFreezeLevel;

    // Fish Magnet
    private float magnetRadius;
    private float magnetTimer;

    //Fish Doubler
    private float fishDoublerTimer;

    // Time Freeze
    private float timeFreezeTimer;

    public enum Powerup { magnet, fishDoubler, timeFreeze }

    void Start()
    {
        fishMagnetLevel = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.fishMagnet);
        fishDoublerLevel = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.fishDouble);
        timeFreezeLevel = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.timeFreeze);
        magnetRadius = CalculateMagnetRadius(fishMagnetLevel);
        magnetTimer = CalculateMagnetTimer(fishMagnetLevel);
        fishDoublerTimer = CalculateFishDoublerTimer(fishDoublerLevel);
        timeFreezeTimer = CalculateTimeFreezeTimer(timeFreezeLevel);
    }

    private float CalculateTimeFreezeTimer(int level)
    {
        switch (level)
        {
            case 0:
                return 10f;
            case 1:
                return 14f;
            case 2:
                return 18f;
            case 3:
                return 22f;
            case 4:
                return 26f;
            case 5:
                return 30f;
            default:
                return 10f;
        }
    }

    private float CalculateFishDoublerTimer(int level)
    {
        switch (level)
        {
            case 0:
                return 20f;
            case 1:
                return 28f;
            case 2:
                return 36f;
            case 3:
                return 44f;
            case 4:
                return 52f;
            case 5:
                return 60f;
            default:
                return 20f;
        }
    }

    // Returns the magnet radius based on its level
    private float CalculateMagnetRadius(int level)
    {
        switch (level)
        {
            case 0:
                return 1.5f;
            case 1:
                return 2.5f;
            case 2:
                return 3.5f;
            case 3:
                return 4.5f;
            case 4:
                return 5.5f;
            case 5:
                return 6f;
            default:
                return 1.5f;
        }
    }

    // Returns the magnet time based on its level
    private float CalculateMagnetTimer(int level)
    {
        switch (level)
        {
            case 0:
                return 20f;
            case 1:
                return 28f;
            case 2:
                return 36f;
            case 3:
                return 44f;
            case 4:
                return 52f;
            case 5:
                return 60f;
            default:
                return 20f;
        }
    }

    // Activating the magnet
    public void UseFishMagnet()
    {
        fishMagnet.GetComponent<CircleCollider2D>().radius = magnetRadius;
        fishMagnet.SetActive(true);

        Invoke("DeactivateMagnet", magnetTimer);
    }

    public void UseFishDoubler()
    {
        Fish.DoubleFish();

        Invoke("DeactivateFishDoubler", fishDoublerTimer);
    }

    public void UseTimeFreeze()
    {
        Temperature.FreezeTime();

        Invoke("DeactivateTimeFreeze", timeFreezeTimer);
    }

    public float GetPowerupTimer(Powerup powerup)
    {
        switch(powerup)
        {
            case Powerup.magnet:
                return magnetTimer;
            case Powerup.fishDoubler:
                return fishDoublerTimer;
            case Powerup.timeFreeze:
                return timeFreezeTimer;
            default:
                return 0;
        }
    }

    private void DeactivateFishDoubler()
    {
        Fish.ResetDoubleFish();
    }

    // Deactivating the magnet after the time has passed
    private void DeactivateMagnet()
    {
        fishMagnet.GetComponent<CircleCollider2D>().radius = 1f;
    }

    private void DeactivateTimeFreeze()
    {
        Temperature.UnfreezeTime();
    }
}
