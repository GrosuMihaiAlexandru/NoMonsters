using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelector : MonoBehaviour
{
    private LevelController levelController;

    private int currentPage;
    public List<UILevel> levelList;

    void Awake()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    void Start()
    {
       for (int i = 0; i < levelController.levels.Count; i++)
        {
            Level level = levelController.levels[i];
            // Set the level of UILevel
            levelList[i].level = level;
            // Set the default level state
            levelList[i].SetDefault();
            levelList[i].GetComponent<Button>().onClick.AddListener(() => SelectLevel(level));
        }
    }

    public void SelectLevel(Level level)
    {
        if (level.Locked)
        {
            Debug.Log("Level Locked");
        }
        else
        {
            Debug.Log("Go to level" + level.ID);
            levelController.StartLevel(level.LevelName);
        }
    }
}
