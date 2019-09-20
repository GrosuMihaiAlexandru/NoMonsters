using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);

        //Unpause the game when going to menu
        Time.timeScale = 1;
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
