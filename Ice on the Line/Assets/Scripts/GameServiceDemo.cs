using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameServiceDemo : MonoBehaviour
{
    public Text scoreText;

    void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowLeaderboardUI()
    {
        if (GameServices.IsInitialized())
            GameServices.ShowLeaderboardUI();
    }

    public void ShowAchievementsUI()
    {
        if (GameServices.IsInitialized())
            GameServices.ShowAchievementsUI();
    }

    public void SubmitScoreToLeaderboard()
    {
        if (GameServices.IsInitialized())
            GameServices.ReportScore(100, EM_GameServicesConstants.Leaderboard_Max_Distance);

    }

    public void LoadLocalUserScore()
    {
        GameServices.LoadLocalUserScore(EM_GameServicesConstants.Leaderboard_Max_Distance, OnLocalUserScoreLoaded);
    }

    void OnLocalUserScoreLoaded(string leaderboardName, IScore score)
    {
        if (score != null)
        {
            scoreText.text = "Your score is:" + score.value.ToString();
        }
        else
        {
            scoreText.text = "No score found on " + leaderboardName;
        }
    }


}
