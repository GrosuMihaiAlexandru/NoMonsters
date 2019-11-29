﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CampaignLevelEnd : MonoBehaviour
{
    private int starsCollected = 0;

    private GameObject victoryScreen;

    private Image[] stars;

    public Sprite emptyStar;
    public Sprite fullStar;

    private GameObject starsObject;

    void Awake()
    {
        victoryScreen = GameObject.Find("GameUI").transform.Find("Victory").gameObject;
        starsObject = victoryScreen.transform.Find("VictoryBox").transform.Find("Stars").gameObject;
        stars = starsObject.transform.GetComponentsInChildren<Image>();
    }

    void Start()
    {
        // Might FuckUp if victory screen location is changed
        Debug.Log(victoryScreen);
        InGameEvents.OnItemCollected += InGameEvents_OnItemCollected;
    }

    private void InGameEvents_OnItemCollected(ICollectible collectible)
    {
        if (collectible.ID == 11)
        {
            starsCollected++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("EndReached");

            Analytics.CustomEvent("Level finished", new Dictionary<string, object>
            {
                { "level", LevelController.instance.selectedLevel.ID }
            });

            LevelController.instance.CompleteLevel(LevelController.instance.selectedLevel.LevelName, starsCollected);
            LevelController.instance.UnlockNextLevel(LevelController.instance.selectedLevel.LevelName);
            LevelController.instance.SaveCampaignProgression();
            ResetStars();
            SetStars(starsCollected);
            victoryScreen.SetActive(true);
        }
    }

    public void ResetStars()
    {
        for (int i = 0; i < 3; i++)
        {
            this.stars[i].sprite = emptyStar;
        }
    }

    public void SetStars(int stars)
    {
        for (int i = 0; i < stars; i++)
        {
            this.stars[i].sprite = fullStar;
            //Debug.Log("Yellow!");
        }
    }
}
