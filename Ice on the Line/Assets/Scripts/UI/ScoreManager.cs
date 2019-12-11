using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private GameObject player;

    public Text finalScore;
    public Text inGameScore;

    [SerializeField]
    private int score = 0;

    private int scoreMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        scoreMultiplier = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.scoreMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        if (InGame.playerAlive)
        {
            //score = (int)((player.transform.position.y * 100) + (player.transform.position.y * 100 * 0.5 * scoreMultiplier));
            score = (int) player.transform.position.y;
            finalScore.text = score.ToString();
            inGameScore.text = score + "m";
        }
        //Debug.Log("Score " + game.Score);
    }
}