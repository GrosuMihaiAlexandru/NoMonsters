using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public int fishMagnetLevel;
    public GameObject fishMagnet;

    private float magnetRadius;
    private float magnetTimer;

    void Start()
    {
        fishMagnetLevel = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.fishMagnet);
        magnetRadius = CalculateMagnetRadius(fishMagnetLevel);
        magnetTimer = CalculateMagnetTimer(fishMagnetLevel);
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

    // Deactivating the magnet after the time has passed
    private void DeactivateMagnet()
    {
        fishMagnet.SetActive(false);
    }
}
