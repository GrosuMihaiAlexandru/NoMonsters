using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int fish;
    public int Gfish;

    public PlayerData(int fish, int Gfish)
    {
        this.fish = fish;
        this.Gfish = Gfish;
    }
}
