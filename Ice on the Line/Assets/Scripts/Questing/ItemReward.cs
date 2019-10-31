using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemReward
{
    public enum Powerup { extrablock, jetpack, freeze }

    public int Fish { get; set; }
    public int GFish { get; set; }
    public int[] Powerups { get; set; } = new int[3];

    public ItemReward(int fish = 0, int gFish = 0, Powerup powerup = Powerup.extrablock, int amount = 0)
    {
        this.Fish = fish;
        this.GFish = gFish;
        Powerups[(int)powerup] = amount;
    }

    public void AddPowerup(Powerup powerup, int amount)
    {
        Powerups[(int)powerup] = amount;
    }

    public void GiveReward()
    {
        if (Fish != 0)
            GameManager.instance.AddFish(Fish);
        if (GFish != 0)
            GameManager.instance.AddGfish(GFish);
        for (int i = 0; i < 3; i++)
            switch (i)
            {
                case 0:
                    GameManager.instance.AddPowerupUses(GameManager.Powerup.extrablock, Powerups[i]);
                    break;
                case 1:
                    GameManager.instance.AddPowerupUses(GameManager.Powerup.freeze, Powerups[i]);
                    break;
                case 2:
                    GameManager.instance.AddPowerupUses(GameManager.Powerup.jetpack, Powerups[i]);
                    break;
                default:
                    break;
            }
    }

}

