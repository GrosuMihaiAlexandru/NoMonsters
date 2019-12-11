using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlaceLoader : MonoBehaviour
{
    public static TAB openingTab = TAB.upgradesTab;
    public static PowerupsPlace powerupsPlace = PowerupsPlace.extraBlock;

    public int bombYValue;
    public int teleportYValue;

    public ShopUI shopUI;

    public GameObject upgradesTab;
    public GameObject powerupsTab;
    public GameObject fishTab;

    public RectTransform powerupsGrid;

    // Start is called before the first frame update
    void Start()
    {
        SetTabAndScrollingOnStart();
    }

    private void SetTabAndScrollingOnStart()
    {
        if (openingTab == TAB.powerupsTab)
        {
            shopUI.PowerupsTab();
            upgradesTab.SetActive(false);
            powerupsTab.SetActive(true);
            fishTab.SetActive(false);

            if (powerupsPlace == PowerupsPlace.bomb)
            {
                powerupsGrid.anchoredPosition = new Vector2(powerupsGrid.anchoredPosition.x, bombYValue);
            }
            else if (powerupsPlace == PowerupsPlace.teleport)
            {
                powerupsGrid.anchoredPosition = new Vector2(powerupsGrid.anchoredPosition.x, teleportYValue);
            }
        }
        else if (openingTab == TAB.fishTab)
        {
            shopUI.FishTab();
            upgradesTab.SetActive(false);
            powerupsTab.SetActive(false);
            fishTab.SetActive(true);
        }

        openingTab = TAB.upgradesTab;
        powerupsPlace = PowerupsPlace.extraBlock;
    }

    public enum TAB { upgradesTab, powerupsTab, fishTab }

    public enum PowerupsPlace { extraBlock, bomb, teleport }
}
