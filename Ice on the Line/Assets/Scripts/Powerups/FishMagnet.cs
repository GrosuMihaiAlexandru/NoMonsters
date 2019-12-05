using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMagnet : MonoBehaviour, IPowerup
{
    public int ID { get; set; }

    void Start()
    {
        ID = 10;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ActivatePowerup();
        }
    }

    public void ActivatePowerup()
    {
        GameObject.Find("Player").GetComponent<PowerupManager>().UseFishMagnet();
        InGameEvents.PowerupCollected(this);
        Destroy(gameObject);
    }
}
