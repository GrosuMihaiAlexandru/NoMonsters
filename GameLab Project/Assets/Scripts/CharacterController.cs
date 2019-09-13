using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;

    public LayerMask layerMask;

    RayOrigins rayOrigins;

    private List<Vector2> waypoints = new List<Vector2>();
    private float minDistance = 0.1f;

    [SerializeField]
    private float movementSpeed = 0.1f;

    private bool startedMoving = false;

    float totalYDistanceTravelled = 0;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
                transform.position = Vector2.MoveTowards(transform.position, waypoints[0], movementSpeed);
        }
        //UpdateRayOrigins();
    }

    private void CheckDistance(float distance)
    {
        if (distance <= minDistance)
        {
            waypoints.RemoveAt(0);
        }
    }

   

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

    public void SetPath(List<Node> path)
    {
        foreach (Node n in path)
        {
            this.waypoints.Add(n.worldPosition);
        }
    }
}
