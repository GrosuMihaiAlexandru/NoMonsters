﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class ExtraBlock : MonoBehaviour, IConsumable
{
    // General Consumable variables
    public int ID { get; set; }
    public int Count { get; set; }

    // Consumable Functionality variables
    public GameObject extraBlockPrefab;

    private bool usedConsumable;

    private GameObject extraBlock;

    float deltaX, deltaY;

    bool moveAllowed = false;
    bool clicked = false;

    public Text countText;

    // Start is called before the first frame update
    void Start()
    {
        ID = 0;

        Count = GameManager.instance.GetPowerupUses(GameManager.Consumable.extrablock);
        UpdateDisplay();
    }   
   
    // The Implementation of the powerup mechanics
    void Update()
    {

        if (usedConsumable)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                //Debug.Log("Being held");
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        extraBlock = Instantiate(extraBlockPrefab);
                        extraBlock.transform.position = touchPos;
                        extraBlock.GetComponentsInChildren<BoxCollider2D>().ToList().ForEach(x => x.enabled = false);
                        moveAllowed = true;

                        break;
                    case TouchPhase.Moved:

                        extraBlock.SendMessage("HoldStarted");
                        extraBlock.transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
                        break;
                    case TouchPhase.Ended:
                        Debug.Log("Ended");
                        moveAllowed = false;
                        break;
                }
            }
        }
    }

    // Event triggered on PowerupClick
    public void OnClick()
    {
        if (Count > 0)
        {
            Use();
            usedConsumable = true;
            //Debug.Log("ClickedButton");
        }
    }

    // Event triggerd on PowerupRelease
    public void OnRelease()
    {
        if (extraBlock)
        {
            usedConsumable = false;
            extraBlock.GetComponentsInChildren<BoxCollider2D>().ToList().ForEach(x => x.enabled = true);
            //Debug.Log("ReleasedButton");
        }
    }

    public void Use()
    {
        InGameEvents.ConsumableUsed(this);
        Count--;
        GameManager.instance.SetPowerupUses(GameManager.Consumable.extrablock, Count);
        GameManager.instance.SaveProgress();
        UpdateDisplay();
    }

    public void Add(int amount)
    {
        Count += amount;
        GameManager.instance.SetPowerupUses(GameManager.Consumable.extrablock, Count);
        GameManager.instance.SaveProgress();
    }

    public void UpdateDisplay()
    {
        if (Count <= 99)
            countText.text = "x" + Count;
        else
            countText.text = "x99";
    }
}
