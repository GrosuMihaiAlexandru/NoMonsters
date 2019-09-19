using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private GameManager game;

    Text score;
    
    // Start is called before the first frame update
    private void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log("Score " + game.Score);
    }
}