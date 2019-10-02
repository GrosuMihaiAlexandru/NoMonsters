using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the buttons behaviour as well as displaying the information in the UI
/// </summary>
public class ShopUI : MonoBehaviour
{

    // Displaying the current fish amount
    public Text fish;
    // Displaying the current Gfish amount
    public Text Gfish;

    public List<Upgrade> upgrades = new List<Upgrade>();
    
    // Start is called before the first frame update
    void Start()
    {

        foreach(Upgrade d in upgrades)
        {
            d.gameObject.GetComponentInChildren<Text>().text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
    }

    public void UpgradeScore()
    {
        upgrades[0].LevelUpgrade(GameManager.instance.Fish);
        foreach (Upgrade d in upgrades)
        {
            d.gameObject.GetComponentInChildren<Text>().text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
    }

    public void UpgradeTemperature()
    {
        upgrades[1].LevelUpgrade(GameManager.instance.Fish);
        foreach (Upgrade d in upgrades)
        {
            d.gameObject.GetComponentInChildren<Text>().text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
    }

    public void UpgradeSnowflake()
    {
        upgrades[2].LevelUpgrade(GameManager.instance.Fish);
        foreach (Upgrade d in upgrades)
        {
            d.gameObject.GetComponentInChildren<Text>().text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
