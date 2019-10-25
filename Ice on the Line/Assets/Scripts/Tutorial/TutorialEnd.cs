using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    public GameObject player;
    public GameObject popupDisplay;
    float time;
    void Start()
    {
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
        GameManager.instance.SaveTutorial();
        GameManager.instance.ReloadData();
        SceneManager.LoadScene("MainScreen");
    }
}
