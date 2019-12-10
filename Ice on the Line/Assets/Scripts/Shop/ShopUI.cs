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
    public List<Text> extraBlockUses = new List<Text>();
    public List<Text> bombUses = new List<Text>();
    public List<Text> teleportUses = new List<Text>();

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

    public const int extraBlockCost = 200; // fish
    public const int extraBlockCost3Pack = 30; // GFish
    public const int extraBlockCost8Pack = 60; // GFish
    public const int extraBlockCost15Pack = 100; // GFish
    public const int extraBlockCost24Pack = 150; // GFish
    public const int extraBlockCost35Pack = 190; // GFish

    public const int bombCost = 200; // fish
    public const int bombCost3Pack = 80; // GFish
    public const int bombCost6Pack = 130; // GFish
    public const int bombCost12Pack = 240; // GFish
    public const int bombCost18Pack = 330; // GFish
    public const int bombCost24Pack = 400; // GFish

    public const int teleportCost = 2100; // fish
    public const int teleportCost3Pack = 180; // GFish
    public const int teleportCost6Pack = 310; // GFish
    public const int teleportCost9Pack = 420; // GFish
    public const int teleportCost12Pack = 510; // GFish
    public const int teleportCost15Pack = 580; // GFish

    // Start is called before the first frame update
    void Start()
    {
        UpdateShopUI();
        fish.text = GameManager.instance.Fish.ToString();
        Gfish.text = GameManager.instance.GFish.ToString();

        extraBlockUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.extrablock).ToString());
        bombUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.bomb).ToString());
        teleportUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.teleport).ToString());
    }

    /*
    public void UpgradeScore()
    {
        if (upgrades[0].level < 10)
        {
            upgrades[0].LevelUpgrade(GameManager.instance.Fish);
            UpdateShopUI();

            Analytics.CustomEvent("UpgradeScore level " + upgrades[0].level);
        }
    }
    */

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

    public void UpgradeFishDouble()
    {
        if (upgrades[4].level < 5)
        {
            upgrades[4].LevelUpgrade(GameManager.instance.Fish);
            UpdateShopUI();

            Analytics.CustomEvent("UpgradeFishDouble level " + upgrades[4].level);
        }
    }

    public void UpgradeTimeFreeze()
    {
        if (upgrades[5].level < 5)
        {
            upgrades[5].LevelUpgrade(GameManager.instance.Fish);
            UpdateShopUI();

            Analytics.CustomEvent("UpgradeTimeFreeze level " + upgrades[5].level);
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
        if (GameManager.instance.Fish >= extraBlockCost)
        {
            GameManager.instance.RemoveFish(extraBlockCost);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.extrablock, 1);
            fish.text = GameManager.instance.Fish.ToString();
            extraBlockUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.extrablock).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyExtraBlock3()
    {
        if (GameManager.instance.GFish >= extraBlockCost3Pack)
        {
            GameManager.instance.RemoveGFish(extraBlockCost3Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.extrablock, 3);
            Gfish.text = GameManager.instance.GFish.ToString();
            extraBlockUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.extrablock).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyExtraBlock8()
    {
        if (GameManager.instance.GFish >= extraBlockCost8Pack)
        {
            GameManager.instance.RemoveGFish(extraBlockCost8Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.extrablock, 8);
            Gfish.text = GameManager.instance.GFish.ToString();
            extraBlockUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.extrablock).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyExtraBlock15()
    {
        if (GameManager.instance.GFish >= extraBlockCost15Pack)
        {
            GameManager.instance.RemoveGFish(extraBlockCost15Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.extrablock, 15);
            Gfish.text = GameManager.instance.GFish.ToString();
            extraBlockUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.extrablock).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyExtraBlock24()
    {
        if (GameManager.instance.GFish >= extraBlockCost24Pack)
        {
            GameManager.instance.RemoveGFish(extraBlockCost24Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.extrablock, 24);
            Gfish.text = GameManager.instance.GFish.ToString();
            extraBlockUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.extrablock).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyExtraBlock35()
    {
        if (GameManager.instance.GFish >= extraBlockCost35Pack)
        {
            GameManager.instance.RemoveGFish(extraBlockCost35Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.extrablock, 35);
            Gfish.text = GameManager.instance.GFish.ToString();
            extraBlockUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.extrablock).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyBomb()
    {
        if (GameManager.instance.Fish >= bombCost)
        {
            GameManager.instance.RemoveFish(bombCost);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.bomb, 1);
            fish.text = GameManager.instance.Fish.ToString();
            bombUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.bomb).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyBomb3()
    {
        if (GameManager.instance.GFish >= bombCost3Pack)
        {
            GameManager.instance.RemoveGFish(bombCost3Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.bomb, 3);
            Gfish.text = GameManager.instance.GFish.ToString();
            bombUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.bomb).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyBomb6()
    {
        if (GameManager.instance.GFish >= bombCost6Pack)
        {
            GameManager.instance.RemoveGFish(bombCost6Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.bomb, 6);
            Gfish.text = GameManager.instance.GFish.ToString();
            bombUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.bomb).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyBomb12()
    {
        if (GameManager.instance.GFish >= bombCost12Pack)
        {
            GameManager.instance.RemoveGFish(bombCost12Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.bomb, 12);
            Gfish.text = GameManager.instance.GFish.ToString();
            bombUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.bomb).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyBomb18()
    {
        if (GameManager.instance.GFish >= bombCost18Pack)
        {
            GameManager.instance.RemoveGFish(bombCost18Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.bomb, 18);
            Gfish.text = GameManager.instance.GFish.ToString();
            bombUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.bomb).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyBomb24()
    {
        if (GameManager.instance.GFish >= bombCost24Pack)
        {
            GameManager.instance.RemoveGFish(bombCost24Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.bomb, 24);
            Gfish.text = GameManager.instance.GFish.ToString();
            bombUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.bomb).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyTeleport()
    {
        if (GameManager.instance.Fish >= teleportCost)
        {
            GameManager.instance.RemoveFish(teleportCost);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.teleport, 1);
            fish.text = GameManager.instance.Fish.ToString();
            teleportUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.teleport).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyTeleport3()
    {
        if (GameManager.instance.GFish >= teleportCost3Pack)
        {
            GameManager.instance.RemoveGFish(teleportCost3Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.teleport, 3);
            Gfish.text = GameManager.instance.GFish.ToString();
            teleportUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.teleport).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyTeleport6()
    {
        if (GameManager.instance.GFish >= teleportCost6Pack)
        {
            GameManager.instance.RemoveGFish(teleportCost6Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.teleport, 6);
            Gfish.text = GameManager.instance.GFish.ToString();
            teleportUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.teleport).ToString());
            GameManager.instance.SaveProgress();
        }
    }
    public void BuyTeleport9()
    {
        if (GameManager.instance.GFish >= teleportCost9Pack)
        {
            GameManager.instance.RemoveGFish(teleportCost9Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.teleport, 9);
            Gfish.text = GameManager.instance.GFish.ToString();
            teleportUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.teleport).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BuyTeleport12()
    {
        if (GameManager.instance.GFish >= teleportCost12Pack)
        {
            GameManager.instance.RemoveGFish(teleportCost12Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.teleport, 12);
            Gfish.text = GameManager.instance.GFish.ToString();
            teleportUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.teleport).ToString());
            GameManager.instance.SaveProgress();
        }
    }
    public void BuyTeleport15()
    {
        if (GameManager.instance.GFish >= teleportCost15Pack)
        {
            GameManager.instance.RemoveGFish(teleportCost15Pack);
            GameManager.instance.AddPowerupUses(GameManager.Consumable.teleport, 15);
            Gfish.text = GameManager.instance.GFish.ToString();
            teleportUses.ForEach(e => e.text = "x" + GameManager.instance.GetPowerupUses(GameManager.Consumable.teleport).ToString());
            GameManager.instance.SaveProgress();
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void UpgradesTab()
    {
        UIImage.sprite = upgradesTab;
    }

    public void PowerupsTab()
    {
        UIImage.sprite = powerupsTab;
    }

    public void FishTab()
    {
        UIImage.sprite = fishTab;
    }
}
