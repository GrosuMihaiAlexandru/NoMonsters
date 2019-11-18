using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IceBlock : MonoBehaviour
{
    public LayerMask defaultLayer;
    public LayerMask rotationLayer;
    public LayerMask specialLayer;

    private Rigidbody2D rigidBody2D;

    // Reference to prefabs to be instantiated
    public GameObject hologram;
    public GameObject hologram2;

    // Maximum number of holograms depending on block size
    private int hologramMax;

    private bool isBeingHeld = false;

    // Makes sure that the blocks can snap only once
    private bool canSnap = true;

    // Check if there is any invisible barrier underneath
    public bool barrierUnderneath = false;

    private List<GameObject> holograms;

    public GameObject astar;

    private bool canCallAstar = false;

    public bool allowRotation = true;
    public bool limitRotation = false;

    public bool isExtraBlock;
    public bool isBomb;

    private const int gridWidth = 6;

    

    void OnDestroy()
    {
        foreach (GameObject obj in holograms)
            Destroy(obj);
    }

    void Start()
    {
        if (!isExtraBlock)
            rigidBody2D = GetComponent<Rigidbody2D>();
        hologramMax = transform.childCount;

        astar = GameObject.Find("A*");

        // Creating the holograms for each ice Tile
        holograms = new List<GameObject>();
        for (int i = 0; i < hologramMax; i++)
        {
            GameObject newHologram = Instantiate(hologram);
            newHologram.GetComponent<Hologram>().parentBlock = this;
            holograms.Add(newHologram);
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

                bool changeColor = false;

                if (barrierUnderneath)
                {
                    setColorToRed();
                }

                foreach (GameObject hologram in holograms)
                {

                    hologram.SetActive(true);

                    Vector2 position = new Vector2(hologram.transform.position.x, hologram.transform.position.y);

                    if (isExtraBlock)
                    {
                        changeColor = true;
                        if (touch.phase == TouchPhase.Ended)
                        {
                            canSnap = false;
                            SnapBlock();
                        }
                    }
                    else if (isBomb)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 0, defaultLayer);

                        changeColor = false;
                        if (hit && hit.collider.tag == "Obstacle")
                        {
                            Debug.Log("OnObstacle");
                            changeColor = true;
                            if (touch.phase == TouchPhase.Ended)
                            {
                                canSnap = false;
                                SnapBlock();
                            }
                        }

                    }
                    // Checking if the object can snap
                    else
                    {
                        
                        RaycastHit2D hitUp = Physics2D.Raycast(position + Vector2.up, Vector2.zero, 0, defaultLayer);
                        RaycastHit2D hitDown = Physics2D.Raycast(position + Vector2.down, Vector2.zero, 0, defaultLayer);
                        RaycastHit2D hitLeft = Physics2D.Raycast(position + Vector2.left, Vector2.zero, 0, defaultLayer);
                        RaycastHit2D hitRight = Physics2D.Raycast(position + Vector2.right, Vector2.zero, 0, defaultLayer);

                        if (hitUp)
                        {
                            if (hitUp.collider.tag == "WalkableBlock" || hitUp.collider.tag == "Player" || hitUp.collider.tag == "Collectible")
                            {
                                if (!barrierUnderneath)
                                    changeColor = true;
                                //Debug.Log("Object can snap up");
#if UNITY_ANDROID
                                if (touch.phase == TouchPhase.Ended)
                                {
                                    // Debug.Log(canSnap);
                                    if (!barrierUnderneath)
                                    {
                                        if (canSnap && !barrierUnderneath)
                                        {
                                            canSnap = false;
                                            SnapBlock();
                                        }
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
                            if (hitDown.collider.tag == "WalkableBlock" || hitDown.collider.tag == "Player" || hitDown.collider.tag == "Collectible")
                            {
                                if (!barrierUnderneath)
                                    changeColor = true;
                                //Debug.Log("Object can snap down");
#if UNITY_ANDROID
                                if (touch.phase == TouchPhase.Ended)
                                {
                                    //Debug.Log(canSnap);
                                    if (!barrierUnderneath)
                                    {
                                        if (canSnap)
                                        {
                                            canSnap = false;
                                            SnapBlock();
                                        }
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
                            if (hitLeft.collider.tag == "WalkableBlock" || hitLeft.collider.tag == "Player" || hitLeft.collider.tag == "Collectible")
                            {
                                if (!barrierUnderneath)
                                    changeColor = true;
                                //Debug.Log("Object can snap left");
#if UNITY_ANDROID
                                if (touch.phase == TouchPhase.Ended)
                                {
                                    // Debug.Log(canSnap);
                                    if (!barrierUnderneath)
                                    {
                                        if (canSnap && !barrierUnderneath)
                                        {
                                            canSnap = false;
                                            SnapBlock();
                                        }
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
                            if (hitRight.collider.tag == "WalkableBlock" || hitRight.collider.tag == "Player" || hitRight.collider.tag == "Collectible")
                            {
                                if (!barrierUnderneath)
                                    changeColor = true;
                                //Debug.Log("Object can snap right");
#if UNITY_ANDROID
                                if (touch.phase == TouchPhase.Ended)
                                {
                                    //Debug.Log(canSnap);
                                    if (!barrierUnderneath)
                                    {
                                        if (canSnap && !barrierUnderneath)
                                        {
                                            canSnap = false;
                                            SnapBlock();
                                        }
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
                    if (!barrierUnderneath)
                    {
                        if (changeColor)
                            setColorToGreen();
                        else
                            setColorToWhite();
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
        if (canCallAstar && !isBomb)
        {
            canCallAstar = false;
            astar.SendMessage("UpdateGrid");
            astar.SendMessage("BreadthFirstSearch");
        }

    }

    private void setColorToGreen()
    {
        foreach (var t in holograms)
        {
            t.GetComponent<SpriteRenderer>().color = new Color32(77, 229, 84, 255);
        }
    }

    private void setColorToWhite()
    {
        foreach (var t in holograms)
        {
            t.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void setColorToRed()
    {
        foreach (var t in holograms)
        {
            t.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    // Rotation
    public void Rotation()
    {
        if (allowRotation)
        {
            SoundManager.instance.PlayRotateIceBlockSoundClip();
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
                        transform.Rotate(0, 0,-90);
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

            bool rotateBack = false;
            bool canRotate = true;
            List<GameObject> temporaryHolograms = new List<GameObject>();
            // The 4 directions of rotation
            bool[] canMoveUp = new bool[4];
            bool[] canMoveDown = new bool[4];
            bool[] canMoveLeft = new bool[4];
            bool[] canMoveRight = new bool[4];

            bool[] canMoveUpLeft = new bool[4];
            bool[] canMoveUpRight = new bool[4];
            bool[] canMoveDownLeft = new bool[4];
            bool[] canMoveDownRight = new bool[4];

            Vector2[] positions = new Vector2[4];
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                //Debug.Log(t.position);
                GameObject temporaryHologram = Instantiate(hologram2);
                temporaryHologram.transform.position = new Vector3(Mathf.Round(t.position.x), Mathf.Round(t.position.y), 0);
                temporaryHologram.SetActive(false);
                temporaryHolograms.Add(temporaryHologram);
                Destroy(temporaryHologram, 0.3f);


                RaycastHit2D hit = Physics2D.Raycast(t.position, Vector2.zero, 0, rotationLayer);
                if (hit)
                {
                    // Check if all blocks can move to the new position
                    if (hit.collider.tag == "FixedBlock" || hit.collider.tag == "WalkableBlock" || hit.collider.tag == "Player" || hit.collider.tag == "Grid")
                    {
                        canRotate = false;
                    }
                }
                // Check for new positions if the block can't rotate
            }
            if (!canRotate)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform t = transform.GetChild(i);

                    RaycastHit2D up = Physics2D.Raycast(new Vector2(t.position.x, t.position.y + 1), Vector2.zero, 0, rotationLayer);
                    Debug.Log(new Vector2(t.position.x, t.position.y + 1));
                    positions[i] = new Vector2(t.position.x, t.position.y + 1);
                    if (!up || up.collider.tag == "Untagged")
                    {
                        canMoveUp[i] = true;
                    }
                    RaycastHit2D down = Physics2D.Raycast(new Vector2(t.position.x, t.position.y - 1), Vector2.zero, 0, rotationLayer);
                    if (!down || down.collider.tag == "Untagged")
                    {
                        canMoveDown[i] = true;
                    }
                    RaycastHit2D left = Physics2D.Raycast(new Vector2(t.position.x - 1, t.position.y), Vector2.zero, 0, rotationLayer);
                    if (!left || left.collider.tag == "Untagged")
                    {
                        canMoveLeft[i] = true;
                    }
                    RaycastHit2D right = Physics2D.Raycast(new Vector2(t.position.x + 1, t.position.y), Vector2.zero, 0, rotationLayer);
                    if (!right || right.collider.tag == "Untagged")
                    {
                        canMoveRight[i] = true;
                    }

                    RaycastHit2D upLeft = Physics2D.Raycast(new Vector2(t.position.x - 1, t.position.y + 1), Vector2.zero, 0, rotationLayer);
                    if (!upLeft || upLeft.collider.tag == "Untagged")
                    {
                        canMoveUpLeft[i] = true;
                    }
                    RaycastHit2D upRight = Physics2D.Raycast(new Vector2(t.position.x + 1, t.position.y + 1), Vector2.zero, 0, rotationLayer);
                    if (!upRight || upRight.collider.tag == "Untagged")
                    {
                        canMoveUpRight[i] = true;
                    }
                    RaycastHit2D downLeft = Physics2D.Raycast(new Vector2(t.position.x - 1, t.position.y - 1), Vector2.zero, 0, rotationLayer);
                    if (!downLeft || downLeft.collider.tag == "Untagged")
                    {
                        canMoveDownLeft[i] = true;
                    }
                    RaycastHit2D downRight = Physics2D.Raycast(new Vector2(t.position.x + 1, t.position.y - 1), Vector2.zero, 0, rotationLayer);
                    if (!downRight || downRight.collider.tag == "Untagged")
                    {
                        canMoveDownRight[i] = true;
                    }
                }
            }
            Debug.Log(positions[0] + " " + canMoveUp[0] + " " + positions[1] + " " + canMoveUp[1] + " " + positions[2] + " " +  canMoveUp[2] + " " + positions[3] + " " + canMoveUp[3]);
            /*Debug.Log(canMoveDown[0] + " " + canMoveDown[1] + " " + canMoveDown[2] + " " + canMoveDown[3]);
            Debug.Log(canMoveLeft[0] + " " + canMoveLeft[1] + " " + canMoveLeft[2] + " " + canMoveLeft[3]);
            Debug.Log(canMoveRight[0] + " " + canMoveRight[1] + " " + canMoveRight[2] + " " + canMoveRight[3]);
*/
            // Move the iceBlock if there is any position available
            if (canMoveUp.All(x => x))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            }
            else if (canMoveDown.All(x => x))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            }
            else if (canMoveLeft.All(x => x))
            {
                transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            }
            else if (canMoveRight.All(x => x))
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }
            else if (canMoveUpLeft.All(x => x))
            {
                transform.position = new Vector2(transform.position.x - 1, transform.position.y + 1);
            }
            else if (canMoveUpRight.All(x => x))
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
            }
            else if (canMoveDownLeft.All(x => x))
            {
                transform.position = new Vector2(transform.position.x - 1, transform.position.y - 1);
            }
            else if (canMoveDownRight.All(x => x))
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y - 1);
            }
            else if (!canRotate)
            {
                rotateBack = true;
            }

            // Move the iceBlock back when no new position is available
            if (rotateBack)
            {

                foreach(var t in temporaryHolograms)
                {
                    t.SetActive(true);
                }
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
        }
    }


    // Snaps the blocks in place and they become fixed after that
    public void SnapBlock()
    {
        SoundManager.instance.PlaySnapIceBlockSoundClip();

        InGameEvents.IceBlockSnapped();
        // Destroy the rigidbody so that it won't move after it snapped
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        //Debug.Log("Object Snapped");
        // Mark that hold has ended
        HoldEnded();
        // Debug.Log("SnappedBlock");
        for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log(holograms[i].transform.position);
            Transform block = transform.GetChild(i);
           // Debug.Log(block);
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

        // Set animator to active
        if (!isExtraBlock && !isBomb)
            GetComponent<Animator>().enabled = true;

        // Activate the breaking of ice
        if (!isExtraBlock && !isBomb)
            GetComponent<IceBlockLife>().PlayerOnTop();
        // Center the object
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
        allowRotation = false;
        holograms.Clear();

    }

    bool startedHold = false;

    // The player has started moving the block
    public void HoldStarted()
    {
        // SoundManager.instance.PlayStartDragIceBlockSoundClip();
        if (!startedHold)
        {
            SoundManager.instance.PlayStartDragIceBlockSoundClip();
            startedHold = true;
        }

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
        startedHold = false;
        isBeingHeld = false;
        if (canSnap == false)
        {
            transform.tag = "FixedBlock";
            canCallAstar = true;
        }
        else // not snapping
        {
            SoundManager.instance.PlayStopDragWithoutSnapIceBlockSoundClip();
        }

        // Returning the collider size to it's initial value
        foreach (Transform block in transform)
        {
            block.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);
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
    return ((int)pos.x >= 0 && (int)pos.x <= gridWidth + 1);
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

    public bool CanSnap { get { return canSnap; } private set { canSnap = value; } }


}

