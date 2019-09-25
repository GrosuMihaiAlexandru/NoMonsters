using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    // The temperature indicates how fast the ice melts in the game
    [SerializeField]
    private int globalTemperature;

    // The globalTemperature starting point
    [SerializeField]
    private int initialTemperature;

    // The time between temperature rising update
    [SerializeField]
    private float temperatureUpdateTime = 5f;

    // The last time the globalTemperature was updated
    private float lastUpdateTime;

    // Start is called before the first frame update
    void Start()
    {
        globalTemperature = initialTemperature;
        lastUpdateTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playerAlive)
        {
            float time = Time.time;

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

    public int GlobalTemperature{
        get
        {
            return globalTemperature;
        }
        set
        {
            globalTemperature = value;
        }
    }
}
