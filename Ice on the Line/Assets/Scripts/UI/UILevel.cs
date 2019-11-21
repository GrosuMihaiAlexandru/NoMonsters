using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    public Level level;
    public Text levelIDText;
    private Transform starParent;
    public GameObject lockImage;
    public GameObject levelNumber;
    private Image[] stars;

    public Sprite emptyStar;
    public Sprite fullStar;

    void Awake()
    {
        starParent = transform.Find("Stars").transform;
        stars = starParent.GetComponentsInChildren<Image>();
    }

    public void SetDefault()
    {
        levelIDText.text = level.ID.ToString();
        SetStars(level.Stars);
        Unlock(!level.Locked);
    }

    public void Unlock(bool unlock)
    {
        if (unlock)
        {
            lockImage.SetActive(false);
            levelNumber.SetActive(true);
        }
        else
        {
            lockImage.SetActive(true);
            levelNumber.SetActive(false);
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
