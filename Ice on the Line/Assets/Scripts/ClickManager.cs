using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the controlls of the game
public class ClickManager : MonoBehaviour
{
    public LayerMask layerMask;

    // The gameObject that is pressed on
    private GameObject iceBlock;
    private Rigidbody2D rb;


    // touch offset allows the object not to shake when it starts moving
    float deltaX, deltaY;

    // object movement not allowed if you touches not the ball at the first time
    bool moveAllowed = false;

    bool isBeingHeld = false;

    float timer = 0;

    [SerializeField]
    private float holdTime = 0.1f;

    Vector3 initialPos;

    void Update()
    {
        if (InGame.playerAlive && !InGame.gamePaused)
        {
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                // get touch to take a deal with
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero, 0, layerMask);
                timer += Time.deltaTime;
                switch (touch.phase)
                {
                    // if you touch the screen
                    case TouchPhase.Began:
                        //Debug.Log("Touch Began");
                        // if you touch the IceBlock
                        if (hit)
                        {
                            //Debug.Log("BeganHit");
                            if (hit.collider.gameObject.transform.parent.gameObject.tag == "IceBlock")
                            {
                                iceBlock = hit.collider.gameObject.transform.parent.gameObject;

                                for (int i = 0; i < iceBlock.transform.childCount; i++)
                                {
                                    Transform t = iceBlock.transform.GetChild(i);
                                    //Debug.Log(t.position);
                                    //t.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.8f);
                                }
                                // get the offset between position you touches
                                // and the center of the game object
                                deltaX = touchPos.x - iceBlock.transform.position.x;
                                deltaY = touchPos.y - iceBlock.transform.position.y;
                                // if touch begins within the ball collider
                                // then it is allowed to move
                                moveAllowed = true;
                                // restrict some rigidbody properties so it moves
                                // more  smoothly and correctly
                                iceBlock.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                                initialPos = iceBlock.transform.position;
                            }
                        }
                        break;

                    // you move your finger
                    case TouchPhase.Moved:
                        // if you touches the ball and movement is allowed then move
                        if (timer > holdTime)
                        {
                            if (moveAllowed)
                            {
                                iceBlock.SendMessage("HoldStarted");

                                iceBlock.GetComponent<Rigidbody2D>().MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                            }
                        }
                        break;


                    // you release your finger
                    case TouchPhase.Ended:
                        // if it's only a short tap the block rotates 
                        if (timer <= 0.2f)
                        {
                            if (iceBlock)
                            {
                                if ((iceBlock.transform.position - initialPos).magnitude < 0.1)
                                {
                                    iceBlock.SendMessage("Rotation");
                                    moveAllowed = false;
                                    timer = 0;
                                    break;
                                }
                            }
                        }
                        //Debug.Log("Touch Ended");
                        // restore initial parameters
                        // when touch is ended
                        moveAllowed = false;
                        // Tell the object that holding has ended
                        timer = 0;
                        break;
                }
                
                if (Input.touchCount > 1)
                {
                    if (iceBlock)
                    {
                        Touch t2 = Input.GetTouch(1);
                        if (t2.phase == TouchPhase.Began)
                        {
                            iceBlock.SendMessage("Rotation");
                        }
                    }
                }
            }
#endif
#if UNITY_IOS

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            // get touch to take a deal with
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit && !isBeingHeld)
            {
                //Debug.Log("BeganHit");
                if (hit.collider.gameObject.transform.parent.gameObject.tag == "IceBlock")
                {
                    iceBlock = hit.collider.gameObject.transform.parent.gameObject;
                    isBeingHeld = true;
                    deltaX = pos.x - iceBlock.transform.position.x;
                    deltaY = pos.y - iceBlock.transform.position.y;
                    iceBlock.SendMessage("HoldStarted");
                }
            }

            
        }
        if (isBeingHeld)
        {
            iceBlock.GetComponent<Rigidbody2D>().MovePosition(new Vector2(pos.x - deltaX, pos.y - deltaY));
        }
        if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            iceBlock.SendMessage("Rotation");
        }
        
#endif
        }
    }

    private void unlockMovement()
    {
    }
}
