using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    private Transform player;

    private List<Transform> water = new List<Transform>();

    private int ySize = 13;

    private int counter = 0;

    private float offset;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        foreach (Transform t in transform)
            water.Add(t);
    }

    void Start()
    {
        offset = player.position.y - transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.y - transform.position.y + offset >= ySize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + ySize, transform.position.z);
        }
    }
}
