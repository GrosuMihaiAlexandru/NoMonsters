using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameEvents : MonoBehaviour
{
    public delegate void CollectibleEventHandler(ICollectible collectible);
    public static event CollectibleEventHandler OnItemCollected;

    public delegate void PowerupEventHandler(Powerup powerup);
    public static event PowerupEventHandler OnPowerupUsed;

    public delegate void GameOverHandler(IPlayer player);
    public static event GameOverHandler OnGameOver;

    public delegate void IceBlockSnappedHandler();
    public static event IceBlockSnappedHandler OnIceBlockSnap;

    public delegate void PowerupCollectedEventHandler(IPowerup powerup);
    public static event PowerupCollectedEventHandler OnPowerupCollected;

    public static void ItemCollected(ICollectible collectible)
    {
        if (OnItemCollected != null)
            OnItemCollected(collectible);
    }

    public static void PowerupUsed(Powerup powerup)
    {
        if (OnPowerupUsed != null)
            OnPowerupUsed(powerup);
    }

    public static void GameOver(IPlayer player)
    {
        if (OnGameOver != null)
            OnGameOver(player);
    }

    public static void IceBlockSnapped()
    {
        if (OnIceBlockSnap != null)
            OnIceBlockSnap();
    }

    public static void PowerupCollected(IPowerup powerup)
    {
        if (OnPowerupCollected != null)
            OnPowerupCollected(powerup);
    }
}
