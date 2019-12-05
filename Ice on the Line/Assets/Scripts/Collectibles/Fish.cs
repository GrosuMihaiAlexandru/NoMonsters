using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour, ICollectible
{
    public static int fishMultiplier = 1;
    public static bool doubleFish = false;

    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 playerDirection;
    private float timeStamp;
    private bool flyToPlayer;

    [SerializeField]
    private int value;

    [SerializeField]
    private AudioClip collectClip;

    public int ID { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ID = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flyToPlayer)
        {
            playerDirection = -(transform.position - player.transform.position).normalized;
            rb.velocity = new Vector2(playerDirection.x, playerDirection.y) * 10f * (Time.time / timeStamp);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Collect();
        }
        else if (other.tag == "FishMagnet")
        {
            timeStamp = Time.time;
            player = GameObject.Find("Player");
            flyToPlayer = true;
        }
    }

    public void Collect()
    {
        SoundManager.instance.PlaySingle(collectClip);
        InGameEvents.ItemCollected(this);
        Destroy(gameObject);
        GameManager.instance.AddFish(value * fishMultiplier);
    }

    public static void AddMultiplier()
    {
        if (doubleFish)
        {
            fishMultiplier = (fishMultiplier / 2 + 1) * 2;
        }
        else
        {
            fishMultiplier++;
        }
    }

    public static void DoubleFish()
    {
        fishMultiplier *= 2;
        doubleFish = true;
    }

    public static void ResetDoubleFish()
    {
        doubleFish = false;
        fishMultiplier /= 2;
    }

    public static void ResetMultiplier()
    {
        fishMultiplier = 1;
    }

}
