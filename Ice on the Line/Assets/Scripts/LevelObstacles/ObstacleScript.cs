using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private List<Sprite> obstacleSprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, obstacleSprites.Count);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = obstacleSprites[index];

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, layerMask);
        if (hit && hit.transform.gameObject.tag == "FixedBlock")
        {
            hit.transform.gameObject.tag = "Obstacle";
        }
    }
}
