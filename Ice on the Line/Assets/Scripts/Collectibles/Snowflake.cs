using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflake : MonoBehaviour, ICollectible
{
    [SerializeField]
    private int temperatureAmount = 10;

    Temperature temperature;

    public int ID { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ID = 1;
        temperature = GameObject.Find("InGame").GetComponent<Temperature>();    
    }

    // Lower the temperature 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Collect();
        }
    }

    public void Collect()
    {
        InGameEvents.ItemCollected(this);
        Destroy(gameObject);
        temperature.LowerTemperature(temperatureAmount + 1 * GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.snowflake));
    }
}
