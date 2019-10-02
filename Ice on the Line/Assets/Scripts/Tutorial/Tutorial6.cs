using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial6 : MonoBehaviour
{
    public GameObject tutorialDisplay;
    public GameObject tutorialEnd;

    float time;
    private void Start()
    {
        time = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time - time > 2)
        {
            if (Input.touchCount > 0)
            {
                gameObject.SetActive(false);
                tutorialEnd.SetActive(true);
            }
        }
    }
}
