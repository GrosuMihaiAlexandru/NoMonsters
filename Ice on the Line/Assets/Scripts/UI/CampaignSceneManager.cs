using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class CampaignSceneManager : MonoBehaviour, IUpdateDisplayable
{
    public GameObject extraOptionsPanel;

    // The currency display
    public Text fish;
    public Text Gfish;
    public Text lives;

    // ToggleVolume
    public Button soundToggleButton;
    public Sprite soundOn;
    public Sprite soundOff;

    public Slider livesSlider;

    private bool extraOptions = false;

    void Start()
    {
        UpdateDisplay();

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
    }

    public void ToggleExtraOptions()
    {
        if (extraOptions)
        {
            extraOptions = false;
            extraOptionsPanel.SetActive(false);
        }
        else
        {
            extraOptions = true;
            extraOptionsPanel.SetActive(true);

        }
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

    public void OpenTermsAndConditions()
    {
        Application.OpenURL("http://www.oulugamelab.net/t-a-c");
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("http://www.oulugamelab.net/policy");
    }

    public void UpdateDisplay()
    {
        fish.text = GameManager.instance.Fish.ToString();
        Gfish.text = GameManager.instance.GFish.ToString();
        lives.text = GameManager.instance.Lives.ToString() + " / " + GameManager.instance.maxLives;
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        livesSlider.value = GameManager.instance.Lives;
    }

    public void ShowLeaderboardUI()
    {
        if (GameServices.IsInitialized())
            GameServices.ShowLeaderboardUI();
    }

    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void Shop()
    {
        Analytics.CustomEvent("Opened Shop");
        SceneManager.LoadScene("Shop");
    }
}
