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
    public Text upgradesTabText;
    public Text powerupsTabText;
    public Text fishTabTex;

    private Color32 blueColor = new Color32(11, 75, 113, 255);

    public Sprite upgradesTab;
    public Sprite powerupsTab;
    public Sprite fishTab;

    // The UI background of the shop
    public Image UIImage;

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
            d.gameObject.GetComponentsInChildren<Text>()[0].text = "Level: " + d.level;
            d.gameObject.GetComponentsInChildren<Text>()[1].text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
    }

    public void UpgradeScore()
    {
        upgrades[0].LevelUpgrade(GameManager.instance.Fish);
        foreach (Upgrade d in upgrades)
        {
            d.gameObject.GetComponentsInChildren<Text>()[0].text = "Level: " + d.level;
            d.gameObject.GetComponentsInChildren<Text>()[1].text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
    }

    public void UpgradeTemperature()
    {
        upgrades[1].LevelUpgrade(GameManager.instance.Fish);
        foreach (Upgrade d in upgrades)
        {
            d.gameObject.GetComponentsInChildren<Text>()[0].text = "Level: " + d.level;
            d.gameObject.GetComponentsInChildren<Text>()[1].text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
    }

    public void UpgradeSnowflake()
    {
        upgrades[2].LevelUpgrade(GameManager.instance.Fish);
        foreach (Upgrade d in upgrades)
        {
            d.gameObject.GetComponentsInChildren<Text>()[0].text = "Level: " + d.level;
            d.gameObject.GetComponentsInChildren<Text>()[1].text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void UpgradesTab()
    {
        upgradesTabText.color = Color.white;
        powerupsTabText.color = blueColor;
        fishTabTex.color = blueColor;
        UIImage.sprite = upgradesTab;
    }

    public void PowerupsTab()
    {
        upgradesTabText.color = blueColor;
        powerupsTabText.color = Color.white;
        fishTabTex.color = blueColor;
        UIImage.sprite = powerupsTab;
    }
}
