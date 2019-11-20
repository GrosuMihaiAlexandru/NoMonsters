using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        levels = new List<Level>
        {
            new Level(1, "Tutorial1", false, 0, false),
            new Level(2, "Tutorial2", false, 0, true),
            new Level(3, "Tutorial3", false, 0, true),
            new Level(4, "Tutorial4", false, 0, true),
            new Level(5, "Tutorial5", false, 0, true),
            new Level(6, "Tutorial6", false, 0, true),
            new Level(7, "Tutorial7", false, 0, true),
            new Level(8, "Tutorial8", false, 0, true),
            new Level(9, "Tutorial9", false, 0, true),
            new Level(10, "Tutorial10", false, 0, true),
            new Level(11, "Tutorial11", false, 0, true),
            new Level(12, "Tutorial12", false, 0, true),
            new Level(13, "Tutorial13", false, 0, true),
            new Level(14, "Tutorial14", false, 0, true),
            new Level(15, "Tutorial15", false, 0, true),
            new Level(16, "Tutorial16", false, 0, true),
            new Level(17, "Tutorial16", false, 0, true),
            new Level(18, "Tutorial16", false, 0, true),
            new Level(19, "Tutorial16", false, 0, true),
            new Level(20, "Tutorial16", false, 0, true)
        };

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

}
