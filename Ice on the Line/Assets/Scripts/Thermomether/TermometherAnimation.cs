using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermometherAnimation : MonoBehaviour
{
    private Animator animator;

    private Temperature temperature;

    private int levelTemperature;
    private int lastTemperature;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        temperature = GameObject.Find("InGame").GetComponent<Temperature>();
        lastTemperature = levelTemperature;
        animator = GetComponent<Animator>();
        animator.SetInteger("Temperature", temperature.GlobalTemperature);
    }

    void Update()
    {
        levelTemperature = temperature.GlobalTemperature;

        if (Mathf.Abs(levelTemperature - lastTemperature) > 5)
        {
            lastTemperature = levelTemperature;
            animator.SetInteger("Temperature", temperature.GlobalTemperature);
        }
    }
}
