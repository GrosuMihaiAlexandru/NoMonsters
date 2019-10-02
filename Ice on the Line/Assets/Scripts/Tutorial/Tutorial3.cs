using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3 : MonoBehaviour
{
    public GameObject tutorialDisplay;
    public GameObject player;
    public GameObject tutorial4;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= 6.8)
        {
            Invoke("DoStuff", 0.25f);

            if (Input.touchCount > 0)
            {
                gameObject.SetActive(false);
                tutorial4.SetActive(true);
            }
        }
    }

    private void DoStuff()
    {
        tutorialDisplay.SetActive(true);
    }
}
