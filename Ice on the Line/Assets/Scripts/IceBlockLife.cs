using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the Iceblock health and damages them based on the amount of time the player has stayed on them and on the globalTemperature
/// As the global temperature increases the player damages the ice faster
/// Walking on the ice can't damage it, only standing still on it
/// This script also handles the animation of IceBlocks based on their health
/// </summary>
public class IceBlockLife : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    private Temperature temperature;
    private Animator animator;

    private int maxHealth = 100;
    [SerializeField]
    private int currentHealth;

    // The ice can't be damaged if true
    private bool indestructable;

    // The amount of time after which the player is standing still
    private float minimumStandingTime =  0.5f;

    // Check to see if the player is on top of the iceblock
    private bool playerOnTop = false; 

    // The time when the player began standing still
    private float beganStandingTime;

    // Flag to set beganStandingTime once
    private bool countdownCanStart = true;
    //Flag to check if coroutine has started
    private bool coroutineStarted = false;

    // The initial damage value
    private int baseDamage = 10;
    // The current damage value
    private float damageMultiplier;


    private void Start()
    {
        currentHealth = maxHealth;
        temperature = GameObject.Find("InGame").GetComponent<Temperature>();

        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();
        animator.SetInteger("Health", currentHealth);
        animator.enabled = false;
    }

    private void Update()
    {

        CalculateMultiplier(temperature.GlobalTemperature);
        if (playerOnTop)
        {
            // Begin the countdown to detect that the player is standing still for minimumStandingTime
            if (countdownCanStart)
            {
                countdownCanStart = false;
                beganStandingTime = Time.time;
            }

            if (Time.time - beganStandingTime >= minimumStandingTime)
            {
                if (!coroutineStarted)
                    StartCoroutine(DamageIce());
            }
        }

        if (currentHealth <= 0)
        {
            DestroyBlock();
        }
        
    }

    // Calculates the rate at which the ice melts based on the temperature
    private void CalculateMultiplier(int temperature)
    {
        if (temperature < -40)
            damageMultiplier = 1;
        else if (temperature < -30)
            damageMultiplier = 1.2f;
        else if (temperature < -20)
            damageMultiplier = 1.4f;
        else if (temperature < -10)
            damageMultiplier = 1.6f;
        else if (temperature < 0)
            damageMultiplier = 2f;
        else if (temperature < 10)
            damageMultiplier = 2.5f;
        else if (temperature < 20)
            damageMultiplier = 3f;
        else if (temperature < 30)
            damageMultiplier = 4f;
        else if (temperature < 40)
            damageMultiplier = 5f;
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }

    public void PlayerOnTop()
    {
        playerOnTop = true;
    }

    IEnumerator DamageIce()
    {
        coroutineStarted = true;
        while (playerOnTop)
        {
            int damage = (int)(baseDamage * damageMultiplier);
            currentHealth -= damage;
            animator.SetInteger("Health", currentHealth);
            //Debug.Log("Health: " + currentHealth + " DamageAmount: " + damage + "Temperature: " + game.GlobalTemperature + " Score: " + game.Score);
            if (currentHealth <= 0)
                break;
            yield return new WaitForSeconds(1f);
        }
    }

}
