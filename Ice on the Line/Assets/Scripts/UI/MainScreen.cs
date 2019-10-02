using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        SceneManager.LoadScene("Shop");
    }

}
