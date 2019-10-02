using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public static bool playerAlive;
    public static bool gamePaused;

    public CharacterController player;

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
        playerAlive = true;
        player = GameObject.Find("Player").GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
