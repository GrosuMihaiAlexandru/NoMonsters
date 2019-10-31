using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

/// <summary>
/// Handles the buttons behaviour as well as displaying the information in the UI
/// </summary>
public class ShopUI : MonoBehaviour
{
    public Text extraBlockUses;

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

    public const int ExtraBlockCost = 200;
    
    // Start is called before the first frame update
    void Start()
    {

        foreach(Upgrade d in upgrades)
        {
            d.gameObject.GetComponentsInChildren<Text>()[0].text = "Level: " + d.level;
            d.gameObject.GetComponentsInChildren<Text>()[1].text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
        Gfish.text = GameManager.instance.GFish.ToString();

        extraBlockUses.text = GameManager.instance.GetPowerupUses(GameManager.Powerup.extrablock).ToString();
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
        GameManager.instance.SaveProgress();

        Analytics.CustomEvent("UpgradeScore level " + upgrades[0].level);
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
        GameManager.instance.SaveProgress();

        Analytics.CustomEvent("UpgradeTemperature level " + upgrades[1].level);
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
        GameManager.instance.SaveProgress();

        Analytics.CustomEvent("UpgradeSnowflake level " + upgrades[2].level);
    }

    public void BuyExtraBlock()
    {
        if (GameManager.instance.Fish >= ExtraBlockCost)
        {
            GameManager.instance.RemoveFish(ExtraBlockCost);
            GameManager.instance.AddPowerupUses(GameManager.Powerup.extrablock, 1);
            fish.text = GameManager.instance.Fish.ToString();
            extraBlockUses.text = GameManager.instance.GetPowerupUses(GameManager.Powerup.extrablock).ToString();
            GameManager.instance.SaveProgress();
        }
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
