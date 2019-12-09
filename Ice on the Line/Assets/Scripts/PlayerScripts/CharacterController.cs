using EasyMobile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterController : MonoBehaviour, IPlayer
{
    public bool isTutorial;

    public bool respawnedOnce = false;

    public GameObject pauseButton;
    public GameObject youDiedScreen;
    public Image timerCircle;
    public GameObject gameOverScreen;

    public LayerMask layerMask;

    private Animator animator;

    private List<Vector2> waypoints = new List<Vector2>();
    private float minDistance = 0.1f;

    [SerializeField]
    private Vector2 velocity;

    [SerializeField]
    private float movementSpeed = 0.1f;

    private int levelHeight = 40;

    private GameObject pathfinding;

    private bool isPlayerInvincible = false;

    [SerializeField]
    private AudioClip walkingSoundClip = null;

    [SerializeField]
    private int gFishRespawn = 10;

    [SerializeField]
    private GameObject fishMagnet;

    // properties from the interface
    public int Distance { get; set; }

    public float speedDebuff = 1f;

    public GameObject waterSplashPrefab;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
        Advertising.RewardedAdCompleted += Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped += Advertising_RewardedAdSkipped;

        pathfinding = GameObject.Find("A*");
    }

    private void Advertising_RewardedAdSkipped(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        Time.timeScale = 1;
        InGame.playerAlive = false;
        Die();
    }

    private void Advertising_RewardedAdCompleted(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        DoRespawn();
    }

    private void DoRespawn()
    {
        //Re enable player sprite renderer
        spriteRenderer.enabled = true;

        Analytics.CustomEvent("FinishedRewardedAd");
        waypoints.Clear();
        Time.timeScale = 1;

        pauseButton.SetActive(true);
        youDiedScreen.SetActive(false);
        StopCoroutine("TimerTick");

        // Debug.Log("Should respawn at: " + CalculateRespawnPosition().ToString());

        // transform.position = CalculateRespawnPosition();

        transform.position = CalculateRespawnPosition();

        movementSpeed = 20;
        

        pathfinding.transform.position = new Vector3(pathfinding.transform.position.x, transform.position.y + 1, pathfinding.transform.position.z);
        Invoke("MakePlayerAliveAfterDelay", 1f);
    }

    private Vector2 CalculateRespawnPosition()
    {
        float dyingYPosition = transform.position.y;
        int levelWhereYouDied = Mathf.FloorToInt(dyingYPosition / levelHeight);

        return new Vector2(transform.position.x, (levelWhereYouDied + 1) * 40);
    }

    private void MakePlayerAliveAfterDelay()
    {
        // Debug.Log("ASDSADASDASDASD");
        
        pathfinding.SendMessage("UpdateGrid");
        pathfinding.SendMessage("InitializeMap");
        isPlayerInvincible = false;
        InGame.playerAlive = true;
        movementSpeed = 5;
    }

    public void WatchAdToRespawn()
    {
        Time.timeScale = 0;
        Advertising.ShowRewardedAd();
        Analytics.CustomEvent("StartedRewardedAd");
    }

    // Update is called once per frame
    void Update()
    {
        // centering the fish magnet
        fishMagnet.transform.position = new Vector2(transform.position.x, transform.position.y);

        if (waypoints.Count > 0)
        {
            float distance = Vector2.Distance(transform.position, waypoints[0]);
            CheckDistance(distance);
            if (waypoints.Count > 0)
            {
                animator.enabled = true;
                Vector2 direction = new Vector2(waypoints[0].x - transform.position.x, waypoints[0].y - transform.position.y);
                transform.up = direction    ;
                velocity = Vector2.MoveTowards(transform.position, waypoints[0], movementSpeed * speedDebuff * Time.deltaTime);
                transform.position = velocity;

                if (!SoundManager.instance.efxSource.isPlaying)
                    SoundManager.instance.PlaySingle(walkingSoundClip);
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
            /*if (hit.collider.gameObject.transform.parent.gameObject.layer == 8)
            {
                GameObject obj = hit.collider.gameObject.transform.parent.gameObject;
                //Debug.Log(obj.transform.position);
                obj.SendMessage("PlayerOnTop");
            }*/
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

                // Set playerAlive to false
                InGame.playerAlive = false;

                if (!respawnedOnce)
                {
                    // Instantiate Falling in water animation prefab
                    Instantiate(waterSplashPrefab, transform.position, Quaternion.identity);
                    // Disable the sprite rendeder
                    spriteRenderer.enabled = false;

                    // Make Player Invincible while waiting to respawn
                    isPlayerInvincible = true;
                    respawnedOnce = true;

                    Invoke("RespawnMethod", 0.8f);
                }
                else
                {
                    if (!isPlayerInvincible)
                    {
                        Die();
                    }
                }
                
            }
        }
    }

    private void RespawnMethod()
    {
        if (!Advertising.IsRewardedAdReady())
            youDiedScreen.transform.Find("RespawnAd").GetComponent<Button>().interactable = false;

        if (GameManager.instance.GFish < gFishRespawn)
            youDiedScreen.transform.Find("RespawnGoldenFish").GetComponent<Button>().interactable = false;


        waypoints.Clear();
        pauseButton.SetActive(false);
        youDiedScreen.SetActive(true);
        StartCoroutine("TimerTick");
    }

    private void OnDestroy()
    {
        Advertising.RewardedAdCompleted -= Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped -= Advertising_RewardedAdSkipped;
    }

    private IEnumerator TimerTick()
    {
        for (float i = 0; i < 3; i += 0.01f)
        {
            timerCircle.fillAmount = ReMap(i, 0, 3, 0, 1);
            yield return new WaitForSeconds(.01f);
        }

        Die();
    }

    private float ReMap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
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
        if (!isPlayerInvincible)
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

    public void Die()
    {
        // Instantiate Falling in water animation prefab
        if (!respawnedOnce)
            Instantiate(waterSplashPrefab, transform.position, Quaternion.identity);

        Analytics.CustomEvent("Died at level ", new Dictionary<string, object>
        {
            { "level", (int)(transform.position.y / levelHeight) + 1 }
        });
        Distance = (int)gameObject.transform.position.y;
        InGameEvents.GameOver(this);
        GameObject.Find("UIManager").GetComponent<EndlessGameUIManager>().LoadLocalUserScore();
        Invoke("EndGame", 0.8f);
        // Remove 1 life when the player dies
        /*
        GameManager.instance.RemoveLives(1);
        GameManager.instance.SaveProgress();
        */

        
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        InGame.playerAlive = false;
        // Set the max travel distance on death
        

        // Set playerAlive to false

        youDiedScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        pauseButton.SetActive(false);
        

        GameManager.instance.SaveProgress();
        QuestManager.instance.UpdateQuests();

        
        Destroy(gameObject);
    }

    public void ApplySpeedDebuff(float value, float time)
    {
        speedDebuff = value;
        Invoke("RemoveSpeedDebuff", time);
    }

    // Reset debuff to normal value
    public void RemoveSpeedDebuff()
    {
        speedDebuff = 1;
    }
}
