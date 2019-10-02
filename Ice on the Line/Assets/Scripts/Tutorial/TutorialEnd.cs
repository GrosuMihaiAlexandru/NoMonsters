using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    private GameManager game;
    public GameObject player;
    public GameObject popupDisplay;
    float time;
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= 27.8)
        {
            popupDisplay.SetActive(true);
            Invoke("DoStuff", 5f);
        }
    }

    private void DoStuff()
    {
        game.SaveTutorial();
        game.ReloadData();
        SceneManager.LoadScene(1);
    }
}
