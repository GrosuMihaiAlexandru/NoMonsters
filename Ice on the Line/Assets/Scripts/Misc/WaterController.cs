using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    private Transform player;

    private List<Transform> water = new List<Transform>();

    private float ySize = 13.359f;

    private float currentDistance = 2;

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

        if (player.transform.position.y - currentDistance + offset >= ySize)
        {
            switch(counter)
            {
                case 0:
                    water[0].position = new Vector3(water[0].position.x, water[0].position.y + 4 *ySize, water[0].position.z);
                    currentDistance += ySize;
                    counter++;
                    break;
                case 1:
                    water[1].position = new Vector3(water[1].position.x, water[1].position.y + 4 * ySize, water[1].position.z);
                    currentDistance += ySize;
                    counter++;
                    break;
                case 2:
                    water[2].position = new Vector3(water[2].position.x, water[2].position.y + 4 * ySize, water[2].position.z);
                    currentDistance += ySize;
                    counter++;
                    break;
                case 3:
                    water[3].position = new Vector3(water[3].position.x, water[3].position.y + 4 * ySize, water[3].position.z);
                    currentDistance += ySize;
                    counter = 0;
                    break;
            }
            //position = new Vector3(transform.position.x, transform.position.y + ySize, transform.position.z);
        }
    }
}
