using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public List<Level> levels;

    public Level selectedLevel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Reading the Campaign progress
        levels = SaveSystem.LoadCampaignProgress();

    }

    public void StartLevel(string levelName)
    {
        selectedLevel = levels.Find(e => e.LevelName == levelName);
        if (selectedLevel == null)
            Debug.LogError("Level not found");
        else
            SceneManager.LoadScene("CampaignLevel");
    }

    public void CompleteLevel(string levelName)
    {
        levels.Find(i => i.LevelName == levelName).Complete();
    }

    public void CompleteLevel(string levelName, int stars)
    {
        levels.Find(i => i.LevelName == levelName).Complete(stars);
    }

    public void UnlockNextLevel(string currentLevelName)
    {
        levels.SkipWhile(e => e.LevelName != currentLevelName).Skip(1).FirstOrDefault().Unlock();
    }

    public void SelectNextLevel(string currentLevelName)
    {
        selectedLevel = levels.SkipWhile(e => e.LevelName != currentLevelName).Skip(1).FirstOrDefault();
    }

    public void SaveCampaignProgression()
    {
        SaveSystem.SaveCampaignProgress(levels);
    }
}
