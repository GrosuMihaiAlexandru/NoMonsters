using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour, IConsumable
{
    public int ID { get; set; }
    public int Count { get; set; }

    public GameObject bombPrefab;

    private bool usedConsumable;
    private GameObject bomb;

    float deltaX, deltaY;

    bool moveAllowed = false;
    bool clicked = false;

    public Text countText;

    public LayerMask layer;

    void Start()
    {
        ID = 1;

        Count = GameManager.instance.GetPowerupUses(GameManager.Consumable.bomb);
        UpdateDisplay();
    }

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
                        bomb = Instantiate(bombPrefab);
                        bomb.transform.position = touchPos;
                        moveAllowed = true;

                        break;
                    case TouchPhase.Moved:

                        bomb.SendMessage("HoldStarted");
                        bomb.transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
                        break;
                    case TouchPhase.Ended:
                        Debug.Log("Ended");
                        moveAllowed = false;
                        break;
                }
            }
        }
    }


    public void OnClick()
    {
        if (Count > 0)
        {
            usedConsumable = true;
            Debug.Log("ClickedButton");
        }
    }

  
    // Event triggerd on PowerupRelease
    public void OnRelease()
    {
        if (bomb)
        {
            usedConsumable = false;
            Vector2 pos = new Vector2(Mathf.Round(bomb.transform.position.x), Mathf.Round(bomb.transform.position.y));
            Debug.Log(pos);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0, layer);
            if (hit && hit.transform.tag == "Obstacle")
            {
                Use();
                hit.transform.GetComponent<IDestructable>().DestroySelf();
            }
            Destroy(bomb);
            // Destroy iceblock script after placing the bomb
            Debug.Log("ReleasedButton");
        }
    }

    public void Add(int amount)
    {
        Count += amount;
        GameManager.instance.SetPowerupUses(GameManager.Consumable.bomb, Count);
        GameManager.instance.SaveProgress();
    }

    public void Use()
    {
        InGameEvents.ConsumableUsed(this);
        Count--;
        GameManager.instance.SetPowerupUses(GameManager.Consumable.bomb, Count);
        GameManager.instance.SaveProgress();
        UpdateDisplay();

    }

    public void UpdateDisplay()
    {
        if (Count <= 99)
            countText.text = "x" + Count;
        else
            countText.text = "x99";
    }
}
