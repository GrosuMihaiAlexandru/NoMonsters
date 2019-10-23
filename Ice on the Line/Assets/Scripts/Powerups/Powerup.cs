using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    
    public int ID { get; set; }

    [SerializeField]
    protected int level;
    [SerializeField]
    protected int count;

    // The text which displays powerup uses
    [SerializeField]
    protected Text text;

    public abstract void AddPowerup(int value);
   
    public abstract void UsePowerup();

    public void UpdateText()
    {
        if (count <= 99)
            text.text = "x" + count;
        else
            text.text = "x99";
    }

}
