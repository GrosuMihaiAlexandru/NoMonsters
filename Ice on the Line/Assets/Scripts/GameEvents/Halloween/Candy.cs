using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour, ICollectible
{
    private int value = 1;

    public int ID { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ID = 1000;
    }

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
        GameManager.instance.AddCandy(value);
    }

}
