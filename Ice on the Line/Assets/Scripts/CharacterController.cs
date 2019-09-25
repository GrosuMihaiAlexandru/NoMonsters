using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject gameOverScreen;

    public LayerMask layerMask;

    private Animator animator;

    private List<Vector2> waypoints = new List<Vector2>();
    private float minDistance = 0.1f;

    [SerializeField]
    private Vector2 velocity;

    [SerializeField]
    private float movementSpeed = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    /*void FixedUpdate()
    {
        
        // Arrow Keys Movement
        
        float velocityX;
        float velocityY;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {

                //Debug.Log("Is touching");
                velocityX = Input.GetAxisRaw("Horizontal") * moveSpeed;
                velocityY = Input.GetAxisRaw("Vertical") * moveSpeed;
                Vector2 velocity = new Vector2(velocityX, velocityY);

                HorizontalGround(ref velocity);
                VerticalGround(ref velocity);

                rigidbody2D.velocity = velocity;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }

    }*/

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count > 0)
        {
            float distance = Vector2.Distance(transform.position, waypoints[0]);
            CheckDistance(distance);
            if (waypoints.Count > 0)
            {
                animator.enabled = true;
                Vector2 direction = new Vector2(waypoints[0].x - transform.position.x, waypoints[0].y - transform.position.y);
                transform.up = direction    ;
                velocity = Vector2.MoveTowards(transform.position, waypoints[0], movementSpeed * Time.deltaTime);
                transform.position = velocity;
            }
        }
        else
        {
            animator.enabled = false;
        }

        // Detecting what the player is on
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, layerMask);
        if (hit)
        {
            // Notify the object the player is on
            if (hit.collider.gameObject.transform.parent.gameObject.layer == 8)
            {
                GameObject obj = hit.collider.gameObject.transform.parent.gameObject;
                //Debug.Log(obj.transform.position);
                obj.SendMessage("PlayerOnTop");
            }
        }
        else
        {
            Destroy(gameObject);
            // Set playerAlive to false
            GameManager.playerAlive = false;
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            GameObject.Find("GameManager").GetComponent<GameManager>().SaveProgress();
        }
    }   
    
    private void Rotate(Vector2 targetPos)
    {
        
    }

    private void CheckDistance(float distance)
    {
        if (distance <= minDistance)
        {
            waypoints.RemoveAt(0);
        }
    }

    /*
    private bool Grounded()
    {
        RaycastHit2D hitBottomLeft = Physics2D.Raycast(rayOrigins.bottomLeft, Vector2.zero);
        RaycastHit2D hitBottomRight = Physics2D.Raycast(rayOrigins.bottomRight, Vector2.zero);
        RaycastHit2D hitTopLeft = Physics2D.Raycast(rayOrigins.topLeft, Vector2.zero);
        RaycastHit2D hitTopRight = Physics2D.Raycast(rayOrigins.topRight, Vector2.zero);

        if (hitBottomLeft && hitBottomRight && hitTopLeft && hitTopRight)
            return true;
        else
            return false;
    }

    // X axis
    
    private void HorizontalGround(ref Vector2 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);

        float offset = 0.08f * directionX;

        Vector2 rayCastBottom = (directionX == -1) ? rayOrigins.bottomLeft : rayOrigins.bottomRight;
        Vector2 rayCastTop = (directionX == -1) ? rayOrigins.topLeft : rayOrigins.topRight;

        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2 (rayCastBottom.x + offset, rayCastBottom.y), Vector2.zero);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(rayCastTop.x + offset, rayCastTop.y), Vector2.zero);

        if (!hit1 || !hit2)
        {
            velocity = Vector2.zero;
        }
    }

    private void VerticalGround(ref Vector2 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);

        float offset = 0.08f * directionY;

        Vector2 rayCastLeft = (directionY == -1) ? rayOrigins.bottomLeft : rayOrigins.topLeft ;
        Vector2 rayCastRight = (directionY == -1) ? rayOrigins.bottomRight : rayOrigins.topRight;

        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(rayCastLeft.x, rayCastLeft.y + offset), Vector2.zero);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(rayCastRight.x, rayCastRight.y + offset), Vector2.zero);

        if (!hit1 || !hit2)
        {
            velocity = Vector2.zero;
            velocity -= new Vector2(velocity.x, velocity.y + offset);
        }
    }
    

    private void UpdateRayOrigins()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        rayOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    struct RayOrigins
    {
        public Vector2 bottomLeft, bottomRight;
        public Vector2 topLeft, topRight;
    }
    */
    public void SetPath(List<Node> path)
    {
        // Clear the old path
        waypoints.Clear();
        // Add the new path to waypoints
        foreach (Node n in path)
        {
            this.waypoints.Add(n.worldPosition);
        }
    }
}
