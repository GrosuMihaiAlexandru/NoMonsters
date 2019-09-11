using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IceBlock : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    public GameObject hologram;

    private int hologramMax;
            
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    private bool isFixed = false;
    private bool canSnap = true;

    private List<GameObject> holograms;

    private GameObject astar;

    private bool canCallAstar = false;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        hologramMax = transform.childCount;

        astar = GameObject.Find("A*");

        holograms = new List<GameObject>();

        for (int i = 0; i < hologramMax; i++)
        {
            holograms.Add(Instantiate(hologram));
        }
    }

    void Update()
    {
        // Displaying the Holograms (
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform block = transform.GetChild(i);
            // Holograms always at round numbers
            Vector3 blockPosition = new Vector3(Mathf.Round(block.position.x), Mathf.Round(block.position.y), 0);
            holograms[i].transform.position = blockPosition;
        }

        if (isBeingHeld)
        {
            // Rotation
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.Rotate(Vector3.forward, 90);
            }

            // Activating holograms
            foreach (GameObject hologram in holograms)
            {
                hologram.SetActive(true);

                Vector2 position = new Vector2(hologram.transform.position.x, hologram.transform.position.y);

                RaycastHit2D hitUp = Physics2D.Raycast(position + Vector2.up, Vector2.zero);
                RaycastHit2D hitDown = Physics2D.Raycast(position + Vector2.down, Vector2.zero);
                RaycastHit2D hitLeft = Physics2D.Raycast(position + Vector2.left, Vector2.zero);
                RaycastHit2D hitRight = Physics2D.Raycast(position + Vector2.right, Vector2.zero);

                if (hitUp)
                {
                    if (hitUp.collider.tag == "FixedBlock" || hitUp.collider.tag == "Player")
                    {
                        //Debug.Log("Object can snap up");
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (canSnap)
                            {
                                canSnap = false;
                                isFixed = true;
                                SnapBlock();
                            }
                        }
                    }
                }
                if (hitDown)
                {
                    if (hitDown.collider.tag == "FixedBlock" || hitDown.collider.tag == "Player")
                    {
                        //Debug.Log("Object can snap down");
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (canSnap)
                            {
                                canSnap = false;
                                isFixed = true;
                                SnapBlock();
                            }
                        }   
                    }
                }
                if (hitLeft)
                {
                    if (hitLeft.collider.tag == "FixedBlock" || hitLeft.collider.tag == "Player")
                    {
                        //Debug.Log("Object can snap left");
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (canSnap)
                            {
                                canSnap = false;
                                isFixed = true;
                                SnapBlock();
                            }
                        }
                    }
                }
                if (hitRight)
                {
                    if (hitRight.collider.tag == "FixedBlock" || hitRight.collider.tag == "Players")
                    {
                        //Debug.Log("Object can snap right");
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (canSnap)
                            {
                                canSnap = false;
                                isFixed = true;
                                SnapBlock();
                            }
                        }
                    }
                }
            }

        }
        else
        {
            // Disactivating holograms
            foreach (GameObject hologram in holograms)
            {
                hologram.SetActive(false);
            }

        }

        if (canCallAstar)
        {
            canCallAstar = false;
            astar.SendMessage("UpdateGrid");
            astar.SendMessage("BreadthFirstSearch");
        }

    }
       

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBeingHeld && !isFixed)
        {
           //DisplayPreview();

            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            // gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
            rigidBody2D.MovePosition(new Vector2(mousePos.x - startPosX, mousePos.y - startPosY));
        }
    }

    private void SnapBlock()
    {
        // Destroy the rigidbody so that it won't move after it snapped
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform block = transform.GetChild(i);
            // Place each block in it's coresponding hologram position
            block.position = holograms[i].transform.position;
            // Change the tag of each block to "FixedBlock"
            block.tag = "FixedBlock";
            block.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
        }
        // Update walkable Grid
    }

    private void DetectClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            startPosX = mousePos.x - transform.position.x;
            startPosY = mousePos.y - transform.position.y;
            isBeingHeld = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;
            if (canSnap == false)
                canCallAstar = true;

        }
    }
    /*
    private void DisplayPreview()
    {
            for (int i = 0; i < transform.childCount; i++)
            {
            Transform block = transform.GetChild(i);
            Debug.Log(i);
            Vector3 blockPosition = new Vector3(Mathf.Round(block.position.x), Mathf.Round(block.position.y), 0);
            if (hologramCount < hologramMax)
            {
                hologramCount++;
                Instantiate(hologram);
                hologram.transform.position = blockPosition;
            }
        }
    }*/
    /*
    private bool HologramAtPosition(Vector3 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(pos.x, pos.y), Vector2.zero, 0, hologramMask);
        if (hit)
        {
            Debug.Log("Obj in place");
            return true;
        }
        else
        {
            Debug.Log("No obj in place");
            return false;
        }
    }
    */
}
