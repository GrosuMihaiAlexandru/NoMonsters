using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelector : MonoBehaviour
{
    public GameObject LevelStartPanel;

    private LevelController levelController;

    private int currentPage;
    public List<UILevel> levelList;

    private Image[] stars;

    public Sprite fullStar;
    public Sprite emptyStar;

    public Button PlayButton;
    public Text levelText;

    void Awake()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        stars = LevelStartPanel.transform.Find("Stars").transform.GetComponentsInChildren<Image>();
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
            levelText.text = "Level " + level.ID;
            ResetStars();
            SetStars(level.Stars);
            PlayButton.onClick.RemoveAllListeners();
            PlayButton.onClick.AddListener(() => levelController.StartLevel(level.LevelName));
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
