using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int level;
    [SerializeField]
    private int count;

    [SerializeField]
    private Text text;

    void Start()
    {
    }

    public void AddPowerup(int value)
    {
        count += value;
        GameManager.instance.SetPowerupUses(GameManager.Powerup.extrablock, count);
        GameManager.instance.SaveProgress();
    }

    public void UsePowerup()
    {
        count--;
        GameManager.instance.SetPowerupUses(GameManager.Powerup.extrablock, count);
        GameManager.instance.SaveProgress();
        UpdateText();
    }

    public void UpdateText()
    {
        if (count <= 99)
            text.text = "x" + count;
        else
            text.text = "x99";
    }
   
    public int Count { get { return count; } set { count = value; } }
    public int Level { get { return level; } set { level = value; } }

}
