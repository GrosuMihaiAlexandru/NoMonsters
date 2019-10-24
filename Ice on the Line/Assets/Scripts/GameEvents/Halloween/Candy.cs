using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour, ICollectible
{
    public int ID { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Collect()
    {
        InGameEvents.ItemCollected(this);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
