using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial4 : MonoBehaviour
{
    public GameObject tutorialDisplay;
    public GameObject player;
    public GameObject tutorial5;

    public GameObject deleteBlockers;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= 11.8)
        {
            Invoke("DoStuff", 0.25f);

            if (Input.touchCount > 0)
            {
                Destroy(deleteBlockers);
                gameObject.SetActive(false);
                tutorial5.SetActive(true);
            }
        }
    }

    private void DoStuff()
    {
        tutorialDisplay.SetActive(true);
    }
}
