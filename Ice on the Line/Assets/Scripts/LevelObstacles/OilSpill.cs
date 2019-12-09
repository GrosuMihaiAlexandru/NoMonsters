using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpill : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.99f)]
    private float speedDebuff;

    [SerializeField, Range(1f, 60f)]
    private float time;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Slowed player for " + time);
            collision.GetComponent<CharacterController>().ApplySpeedDebuff(speedDebuff, time);
        }
    }
}
