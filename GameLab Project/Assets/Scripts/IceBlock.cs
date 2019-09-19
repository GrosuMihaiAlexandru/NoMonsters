using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IceBlock : MonoBehaviour
{
    public LayerMask defaultLayer;

    private Rigidbody2D rigidBody2D;

    public GameObject hologram;

    private int hologramMax;

    private bool isBeingHeld = false;

    private bool canSnap = true;

    private List<GameObject> holograms;

    private GameObject astar;

    private bool canCallAstar = false;

    public bool allowRotation = true;
    public bool limitRotation = false;

    private const int gridWidth = 6;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        hologramMax = transform.childCount;

        astar = GameObject.Find("A*");

        // Creating the holograms for each ice Tile
        holograms = new List<GameObject>();
        for (int i = 0; i < hologramMax; i++)
        {
            holograms.Add(Instantiate(hologram));
        }
    }

    void Update()
    {
        // Displaying the Holograms (
        if (holograms.Count > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform block = transform.GetChild(i);
                // Holograms always at round numbers
                Vector3 blockPosition = new Vector3(Mathf.Round(block.position.x), Mathf.Round(block.position.y), 0);
                holograms[i].transform.position = blockPosition;
            }

            if (isBeingHeld)
            {
#if UNITY_ANDROID
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                    HoldEnded();
#endif
                if (Input.GetMouseButtonUp(0))
                    HoldEnded();
                foreach (GameObject hologram in holograms)
                {
                    hologram.SetActive(true);

                    Vector2 position = new Vector2(hologram.transform.position.x, hologram.transform.position.y);

                    RaycastHit2D hitUp = Physics2D.Raycast(position + Vector2.up, Vector2.zero);
                    RaycastHit2D hitDown = Physics2D.Raycast(position + Vector2.down, Vector2.zero);
                    RaycastHit2D hitLeft = Physics2D.Raycast(position + Vector2.left, Vector2.zero);
                    RaycastHit2D hitRight = Physics2D.Raycast(position + Vector2.right, Vector2.zero);

                    // Checking if the object can snap
                    if (hitUp)
                    {
                        if (hitUp.collider.tag == "FixedBlock" || hitUp.collider.tag == "Player")
                        {
                            //Debug.Log("Object can snap up");
#if UNITY_ANDROID
                            if (touch.phase == TouchPhase.Ended)
                            {
                               // Debug.Log(canSnap);
                                if (canSnap)
                                {
                                    canSnap = false;
                                    SnapBlock();
                                }
                            }
#endif
#if UNITY_IOS
                            if (Input.GetMouseButtonUp(0))
                            {
                                // Debug.Log(canSnap);
                                if (canSnap)
                                {
                                    canSnap = false;
                                    SnapBlock();
                                }
                            }
#endif
                        }
                    }
                    if (hitDown)
                    {
                        if (hitDown.collider.tag == "FixedBlock" || hitDown.collider.tag == "Player")
                        {
                            //Debug.Log("Object can snap down");
#if UNITY_ANDROID
                            if (touch.phase == TouchPhase.Ended)
                            {
                                //Debug.Log(canSnap);
                                if (canSnap)
                                {
                                    canSnap = false;
                                    SnapBlock();
                                }
                            }
#endif
#if UNITY_IOS
                            if (Input.GetMouseButtonUp(0))
                            {
                                // Debug.Log(canSnap);
                                if (canSnap)
                                {
                                    canSnap = false;
                                    SnapBlock();
                                }
                            }
                            #endif
                        }
                    }
                    if (hitLeft)
                    {
                        if (hitLeft.collider.tag == "FixedBlock" || hitLeft.collider.tag == "Player")
                        {
                            //Debug.Log("Object can snap left");
#if UNITY_ANDROID
                            if (touch.phase == TouchPhase.Ended)
                            {
                               // Debug.Log(canSnap);
                                if (canSnap)
                                {
                                    canSnap = false;
                                    SnapBlock();
                                }
                            }
#endif
#if UNITY_IOS
                            if (Input.GetMouseButtonUp(0))
                            {
                                // Debug.Log(canSnap);
                                if (canSnap)
                                {
                                    canSnap = false;
                                    SnapBlock();
                                }
                            }
#endif
                        }
                    }
                    if (hitRight)
                    {
                        if (hitRight.collider.tag == "FixedBlock" || hitRight.collider.tag == "Player")
                        {
                            //Debug.Log("Object can snap right");
#if UNITY_ANDROID
                            if (touch.phase == TouchPhase.Ended)
                            {
                                //Debug.Log(canSnap);
                                if (canSnap)
                                {
                                    canSnap = false;
                                    SnapBlock();
                                }
                            }
#endif
#if UNITY_IOS
                            if (Input.GetMouseButtonUp(0))
                            {
                                // Debug.Log(canSnap);
                                if (canSnap)
                                {
                                    canSnap = false;
                                    SnapBlock();
                                }
                            }
#endif
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
        }
        // Calling the pathfinding method
        if (canCallAstar)
        {
            canCallAstar = false;
            astar.SendMessage("UpdateGrid");
            astar.SendMessage("BreadthFirstSearch");
        }

    }

    // Rotation
    public void Rotation()
    {
        if (allowRotation)
        {
            if (limitRotation)
            {
                if (transform.rotation.eulerAngles.z >= 90)
                {
                    transform.Rotate(0, 0, -90);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }

            if (CheckIsValidPosition())
            {

            }
            else
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
            }
            /*
            foreach(Transform t in transform)
            {
                RaycastHit2D hit = Physics2D.Raycast(t.position, Vector2.zero, 0, defaultLayer);
                if (hit)
                {
                    if (hit.collider.tag == "FixedBlock")
                        transform.Rotate(0, 0, -90);
                }
            }
            */
        }
    }

    // Snaps the blocks in place and they become fixed after that
    private void SnapBlock()
    {
        // Destroy the rigidbody so that it won't move after it snapped
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Debug.Log("Object Snapped");
        for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log(holograms[i].transform.position);
            Transform block = transform.GetChild(i);
            // Place each block in it's coresponding hologram position
            //block.position = holograms[i].transform.position;
            // Change the tag of each block to "FixedBlock"
            block.tag = "FixedBlock";
            block.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
        }
        // Destroy all the holograms
        foreach (GameObject o in holograms)
        {
            Destroy(o);
        }
        holograms.Clear();
        // Center the object
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
        allowRotation = false;
        HoldEnded();
    }

    // The player has started moving the block
    public void HoldStarted()
    {
        //Debug.Log("hold started");
        isBeingHeld = true;
        // Making the collider smaller during dragging to make them easier to move
        foreach (Transform block in transform)
        {
            block.GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 0.7f);
        }
    }

    // The player stopped moving the block
    public void HoldEnded()
    {
        isBeingHeld = false;
        if (canSnap == false)
        {
            transform.tag = "FixedBlock";
            canCallAstar = true;
        }

        // Returning the collider size to it's initial value
        foreach (Transform block in transform)
        {
            block.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 1.1f);
        }
    }
        // Mouse Controlls
        /*
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

        }*/

        // Touch Controlls


    bool CheckIsInsideGrid (Vector2 pos)
    {
    return ((int)pos.x >= 1 && (int)pos.x <= gridWidth);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    bool CheckIsValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = Round(child.position);

            if (CheckIsInsideGrid(pos) == false)
            {
                return false;
            }

        }
        return true;
    }


}

