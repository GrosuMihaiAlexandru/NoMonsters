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

        UpdateShopUI();
        fish.text = GameManager.instance.Fish.ToString();
        Gfish.text = GameManager.instance.GFish.ToString();

        extraBlockUses.text = GameManager.instance.GetPowerupUses(GameManager.Powerup.extrablock).ToString();
    }

    public void UpgradeScore()
    {
        if (upgrades[0].level < 10)
        {
            upgrades[0].LevelUpgrade(GameManager.instance.Fish);
            UpdateShopUI();

            Analytics.CustomEvent("UpgradeScore level " + upgrades[0].level);
        }
    }

    public void UpgradeTemperature()
    {
        if (upgrades[1].level < 10)
        {
            upgrades[1].LevelUpgrade(GameManager.instance.Fish);
            UpdateShopUI();

            Analytics.CustomEvent("UpgradeTemperature level " + upgrades[1].level);
        }
    }

    public void UpgradeSnowflake()
    {
        if (upgrades[2].level < 10)
        {
            upgrades[2].LevelUpgrade(GameManager.instance.Fish);
            UpdateShopUI();

            Analytics.CustomEvent("UpgradeSnowflake level " + upgrades[2].level);
        }
    }

    public void UpgradeFishMagnet()
    {
        if (upgrades[3].level < 5)
        {
            upgrades[3].LevelUpgrade(GameManager.instance.Fish);
            UpdateShopUI();

            Analytics.CustomEvent("UpgradeFishMagnet level " + upgrades[3].level);
        }
    }

    // Updates the shop and save the upgrades
    private void UpdateShopUI()
    {
        foreach (Upgrade d in upgrades)
        {
            d.gameObject.GetComponentsInChildren<Text>()[0].text = "Level: " + d.level;
            if (d.level == d.maxLevel)
                d.gameObject.GetComponentsInChildren<Text>()[1].text = "Upgrade: Max";
            else
                d.gameObject.GetComponentsInChildren<Text>()[1].text = "Upgrade: " + d.cost;
        }
        fish.text = GameManager.instance.Fish.ToString();
        GameManager.instance.SaveProgress();
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
