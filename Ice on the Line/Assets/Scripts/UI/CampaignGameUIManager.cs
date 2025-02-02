﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyMobile;
using UnityEngine.SocialPlatforms;

public class CampaignGameUIManager : MonoBehaviour
{

    public Text fishCollectedVictory;
    public Text fishCollectedGameOver;

    // ToggleVolume
    public Button soundToggleButton;
    public Sprite soundOn;
    public Sprite soundOff;

    [SerializeField]
    private int collectedFish = 0;
    //private int distance = 0;

    private GameObject player;

    private int playerFinalDistance;

    void Start()
    {
        player = GameObject.Find("Player");
        collectedFish = 0;

        // Sound Button
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            soundToggleButton.GetComponent<Image>().sprite = soundOn;
            SoundManager.instance.ToggleAllSounds(false);
        }
        else
        {
            soundToggleButton.GetComponent<Image>().sprite = soundOff;
            SoundManager.instance.ToggleAllSounds(true);
        }

        InGameEvents.OnItemCollected += CollectFish;
    }



    public void PauseVolume()
    {
        UpdateIconAndVolume();
    }

    void UpdateIconAndVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            Debug.Log("a11111");
            SoundManager.instance.ToggleAllSounds(true);

            soundToggleButton.GetComponent<Image>().sprite = soundOff;
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            Debug.Log("a2@22222");
            SoundManager.instance.ToggleAllSounds(false);

            soundToggleButton.GetComponent<Image>().sprite = soundOn;
            PlayerPrefs.SetInt("Muted", 0);
        }
    }

    public void ReturnToMenuAndLoseLife()
    {
        GameManager.instance.RemoveLives(1);
        GameManager.instance.SaveProgress();

        SceneManager.LoadScene("Campaign");
        Time.timeScale = 1;
        ShowInterstitialAdWithChance(40);
    }
    public void CampaignMenu()
    {
      
        SceneManager.LoadScene("Campaign");
        Time.timeScale = 1;
        ShowInterstitialAdWithChance(40);
    }

    public void RetryLevel()
    {
        GameManager.instance.RemoveLives(1);
        GameManager.instance.SaveProgress();

        SceneManager.LoadScene("CampaignLevel");
        InGame.playerAlive = true;
        InGame.gamePaused = false;
        Time.timeScale = 1;
        ShowInterstitialAdWithChance(20);
    }

    public void PauseGameAction()
    {
        Time.timeScale = 0;
        InGame.gamePaused = true;
    }

    public void UnPauseGameAction()
    {
        Time.timeScale = 1;
        InGame.gamePaused = false;
    }

    public void CollectFish(ICollectible collectible)
    {
        if (collectible.ID == 0)
            collectedFish++;
    }

    public void LoadNextLevel()
    {
        LevelController.instance.SelectNextLevel(LevelController.instance.selectedLevel.LevelName);
        SceneManager.LoadScene("CampaignLevel");
        ShowInterstitialAdWithChance(40);
    }

    public void ShowInterstitialAdWithChance(int percentage) // chance should be less than 1, or it will always show
    {
        int x = Random.Range(1, 101);
        bool shouldShow = x < percentage;
        Debug.Log(shouldShow);
        if (shouldShow && Advertising.IsInterstitialAdReady())
        {
            Advertising.ShowInterstitialAd();
        }
    }

}
