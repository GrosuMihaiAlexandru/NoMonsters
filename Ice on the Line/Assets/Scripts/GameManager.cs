using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // The currencies of the game
    [SerializeField]
    private int fish;
    [SerializeField]
    private int Gfish;

    // player reference to calculate the score based on the distance of the player
    public CharacterController player;
        
    // Game over if false
    public static bool playerAlive;
    public static bool gamePaused;

    // The score of of the game
    [SerializeField]
    private int score = 0;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
        playerAlive = true;
        PlayerData data = SaveSystem.LoadData();
        fish = data.fish;
        Gfish = data.Gfish;
    }

    void Update()
    {
        if (playerAlive)
        {
            // calculate score
            score = (int)player.transform.position.y * 100;
        }
    }

    public void AddFish(int amount)
    {
        fish += amount;
    }

    public void RemoveFish(int amount)
    {
        if (fish >= amount)
            fish -= amount;
        else
            Debug.Log("Insuficient fish");
    }

    public void AddGfish(int amount)
    {
        Gfish += amount;
    }

    public void RemoveGfish(int amount)
    {
        if (Gfish >= amount)
            Gfish -= amount;
        else
            Debug.Log("Insuficient Gfish");
    }

    public void SaveProgress()
    {
        SaveSystem.SaveData(fish, Gfish);
    }

    public int Score { get { return score; } set { score = value; } }
    
}
