using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenManager : MonoBehaviour, IUpdateDisplayable
{
    // Popup tabs
    public GameObject questPanel;
    public GameObject settingsPanel;
    public GameObject GDPRPanel;

    // The currency display
    public Text fish;
    public Text Gfish;

    // ToggleVolume
    public Button soundToggleButton;
    public Sprite soundOn;
    public Sprite soundOff;

    // Main Menu theme song
    public AudioClip mainMenuClip;

    private bool extraOptions = false;

    public GameObject extraOptionsPanel;

    public GameObject campaignButton;
    public GameObject endlessButton;
    public GameObject cancelStartPanel;

    public Button buyPlayButton;


    void Awake()
    {
        // GDPR
        int accepted = PlayerPrefs.GetInt("GDPRAccepted", 0);
        Debug.Log("GDPR status " + accepted);
        if (accepted == 0)
        {
            GDPRPanel.SetActive(true);
        }

    }

    // Start is called before the first frame update
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
        SoundManager.instance.musicSource.clip = mainMenuClip;
        SoundManager.instance.musicSource.Play();
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

    public void UpdateDisplay()
    {
        fish.text = GameManager.instance.Fish.ToString();
        Gfish.text = GameManager.instance.GFish.ToString();
    }

    public void Shop()
    {
        Analytics.CustomEvent("Opened Shop");
        SceneManager.LoadScene("Shop");
    }

    public void AcceptPressed()
    {
        PlayerPrefs.SetInt("GDPRAccepted", 1);
        gameObject.SetActive(false);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public void OpenTermsAndConditions()
    {
        Application.OpenURL("http://www.oulugamelab.net/t-a-c");
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("http://www.oulugamelab.net/policy");
    }

    public void PlayEndless()
    {
        SceneManager.LoadScene("EndlessGame");
    }

    public void DisplayOptions()
    {
        campaignButton.SetActive(true);
        endlessButton.SetActive(true);
        cancelStartPanel.SetActive(true);
        buyPlayButton.interactable = false;
    }

    public void CancelStartPanel()
    {
        campaignButton.SetActive(false);
        endlessButton.SetActive(false);
        cancelStartPanel.SetActive(false);
        buyPlayButton.interactable = true;
    }

    public void ShowLeaderboardUI()
    {
        if (GameServices.IsInitialized())
            GameServices.ShowLeaderboardUI();
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

    public void OpenCampaignScene()
    {
        SceneManager.LoadScene("Campaign");
    }

}
