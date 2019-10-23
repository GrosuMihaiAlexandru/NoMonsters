using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterController : MonoBehaviour, IPlayer
{
    public bool isTutorial;

    public GameObject gameOverScreen;
    public LayerMask layerMask;

    private Animator animator;

    private List<Vector2> waypoints = new List<Vector2>();
    private float minDistance = 0.1f;

    [SerializeField]
    private Vector2 velocity;

    [SerializeField]
    private float movementSpeed = 0.1f;

    // properties from the interface
    public int MaxDistance { get; set; }

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
            // For tutorial
            if (isTutorial)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                Die();
            }
        }
    }   

    private void CheckDistance(float distance)
    {
        if (distance <= minDistance)
        {
            waypoints.RemoveAt(0);
        }
    }

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

    public void Die()
    {
        // Set the max travel distance on death
        MaxDistance = (int) gameObject.transform.position.y;
        InGameEvents.GameOver(this);
        Destroy(gameObject);
        // Set playerAlive to false
        InGame.playerAlive = false;

        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        GameManager.instance.SaveProgress();
        QuestManager.instance.UpdateQuests();
    }
}
