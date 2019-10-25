using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour, ICollectible
{
    [SerializeField]
    private int value;

    [SerializeField]
    private AudioClip collectClip;

    public int ID { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ID = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
        SoundManager.instance.PlaySingle(collectClip);
        InGameEvents.ItemCollected(this);
        Destroy(gameObject);
        GameManager.instance.AddFish(value);
    }
}
