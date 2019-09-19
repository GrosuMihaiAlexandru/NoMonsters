using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public CharacterController player;

    // Game over if false
    public bool playerAlive = true;

    // The globalTemperature starting point
    private int initialTemperature = -41;

    // The temperature indicates how fast the ice melts in the game
    [SerializeField]
    private int globalTemperature;

    // The time between temperature rising update
    [SerializeField]
    private float temperatureUpdateTime = 5f;

    // The last time the globalTemperature was updated
    private float lastUpdateTime;

    private int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
        globalTemperature = initialTemperature;
        lastUpdateTime = Time.time;
        playerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive)
        {
            float time = Time.time;

            score = (int)player.transform.position.y * 100;

            // Temperature rises by 1 degree everytime temperatureUpdateTime
            if (time - lastUpdateTime >= temperatureUpdateTime)
            {
                globalTemperature++;
                // Update the last time the temperature increased
                lastUpdateTime = Time.time;
            }
        }
    }

    // Activated by collecting lowering temperature powerups
    public void LowerTemperature(int amount)
    {
        globalTemperature -= amount;
    }

    public int GlobalTemperature
    {
        get
        {
            return globalTemperature;
        }
        set
        {
            globalTemperature = value;
        }
    }

    public int Score { get; set; }
    
}
