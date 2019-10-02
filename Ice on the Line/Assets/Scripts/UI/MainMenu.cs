using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    GameManager game;

    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void PlayGame()
    {
        if (game.tutorialDone)
            SceneManager.LoadScene("MainScreen");
        else
            SceneManager.LoadScene("Tutorial");
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainScreen");

        //Unpause the game when going to menu
        Time.timeScale = 1;
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("Tutorial");

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

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
}
