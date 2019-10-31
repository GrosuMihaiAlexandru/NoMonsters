using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainScreen : MonoBehaviour
{
    // Displaying the current fish amount
    public Text fish;
    // Displaying the current Gfish amount
    public Text Gfish;


    void Start()
    {
        fish.text = GameManager.instance.Fish.ToString();
        Gfish.text = GameManager.instance.GFish.ToString();

    }

    public void UpdateDisplay()
    {
        fish.text = GameManager.instance.Fish.ToString();
        Gfish.text = GameManager.instance.GFish.ToString();
    }

    public void PlayGame()
    {
        if (GameManager.instance.tutorialDone)
            SceneManager.LoadScene("SampleScene");
        else
            SceneManager.LoadScene("Tutorial");
    }

    public void Shop()
    {
        Analytics.CustomEvent("Opened Shop");
        SceneManager.LoadScene("Shop");
    }

}
