using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyMobile;
using UnityEngine.SocialPlatforms;

public class EndlessGameUIManager : MonoBehaviour
{
    public Text distanceText;
    public Text bestDistanceText;
    public Text fishText;
    public Text fishMutiplier;

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
        InGameEvents.OnGameOver += DisplayGameOver;
    }

    void Update()
    {
        fishMutiplier.text = Fish.fishMultiplier.ToString();
    }

    public void OnDestroy()
    {
        InGameEvents.OnItemCollected -= CollectFish;
        InGameEvents.OnGameOver -= DisplayGameOver;
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

    public void Menu()
    {
        SceneManager.LoadScene("MainScreen");

        //Unpause the game when going to menu
        Time.timeScale = 1;
    }

    public void CampaignMenu()
    {
        SceneManager.LoadScene("Campaign");

        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);

        // Set player to alive and unpause the game
        InGame.playerAlive = true;
        InGame.gamePaused = false;
        Time.timeScale = 1;
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene("CampaignLevel");
        InGame.playerAlive = true;
        InGame.gamePaused = false;
        Time.timeScale = 1;
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

    // Event for updating the collected amount of fish to be displayed in gameOver
    public void CollectFish(ICollectible collectible)
    {
        if (collectible.ID == 0)
            collectedFish += Fish.fishMultiplier;
    }

    // Display the final distance of the player in gameOver
    public void DisplayGameOver(IPlayer player)
    {
        playerFinalDistance = player.Distance;
        Debug.Log("Distace:" + playerFinalDistance);
        distanceText.text = "DISTANCE: " + playerFinalDistance.ToString();
        fishText.text = collectedFish.ToString();
    }

    public void SubmitScoreToLeaderboard()
    {
        if (GameServices.IsInitialized())
            GameServices.ReportScore(playerFinalDistance, EM_GameServicesConstants.Leaderboard_Max_Distance);

    }

    public void LoadLocalUserScore()
    {
        if (GameServices.IsInitialized())
            GameServices.LoadLocalUserScore(EM_GameServicesConstants.Leaderboard_Max_Distance, OnLocalUserScoreLoaded);
    }

    void OnLocalUserScoreLoaded(string leaderboardName, IScore score)
    {
        if (score != null)
        {
            if (playerFinalDistance > score.value)
            {
                SubmitScoreToLeaderboard();
            }
        }
        else
        {
            SubmitScoreToLeaderboard();
            Debug.Log("No score found");
        }
    }

}
