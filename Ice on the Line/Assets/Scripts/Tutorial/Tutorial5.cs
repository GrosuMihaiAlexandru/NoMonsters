using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial5 : MonoBehaviour
{
    public GameObject tutorialDisplay;
    public GameObject player;
    public GameObject tutorial6;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= 23.8)
        {
            Invoke("DoStuff", 0.25f);

            if (Input.touchCount > 0)
            {
                gameObject.SetActive(false);
                tutorial6.SetActive(true);
            }
        }
    }

    private void DoStuff()
    {
        tutorialDisplay.SetActive(true);
    }
}
