using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour, IDestructable
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private List<Sprite> obstacleSprites = new List<Sprite>();

    [SerializeField]
    private List<Sprite> obstacleDestroyedSprites = new List<Sprite>();

    private int index;

    GameObject astar;

    public void DestroySelf()
    {
        Debug.Log("Destroyed Obstacle");
        GameObject obstacle = gameObject;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, layerMask);
        if (hit && hit.transform.gameObject.tag == "Obstacle")
        {
            hit.transform.gameObject.tag = "FixedBlock";
        }
        // Change Sprite to destryed version
        Invoke("ChangeSprite", 0.5f);
        // Update Player Movement after the block is destroyed
        astar.SendMessage("UpdateGrid");
        astar.SendMessage("BreadthFirstSearch");
    }

    private void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = obstacleDestroyedSprites[index];

    }

    // Start is called before the first frame update
    void Start()
    {
        astar = GameObject.Find("A*");
        index = Random.Range(0, obstacleSprites.Count);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = obstacleSprites[index];

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, layerMask);
        if (hit && hit.transform.gameObject.tag == "FixedBlock")
        {
            hit.transform.gameObject.tag = "Obstacle";
        }
    }
}
