using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflake : MonoBehaviour
{
    private static int temperatureAmount = 10;

    Temperature temperature;

    // Start is called before the first frame update
    void Start()
    {
        temperature = GameObject.Find("GameManager").GetComponent<Temperature>();    
    }

    // Lower the temperature 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            temperature.LowerTemperature(temperatureAmount);
        }
    }
}
