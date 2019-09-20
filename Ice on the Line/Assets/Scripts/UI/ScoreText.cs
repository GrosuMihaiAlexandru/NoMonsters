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
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = game.Score.ToString();
        //Debug.Log("Score " + game.Score);
    }
}