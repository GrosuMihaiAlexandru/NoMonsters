using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour, ICollectible
{
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
        GameManager.instance.AddFish(value);
    }
}
