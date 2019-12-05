using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour, IConsumable
{
    public int ID { get; set; }
    public int Count { get; set; }

    private bool usedConsumable;

    private GameObject teleport;

    public Text countText;

    float deltaX, deltaY;

    public GameObject teleportPrefab;

    public LayerMask layer;

    private GameObject astar;

    void Awake()
    {
        astar = GameObject.Find("A*");
    }

    // Start is called before the first frame update
    void Start()
    {
        ID = 2;

        Count = GameManager.instance.GetPowerupUses(GameManager.Consumable.teleport);
        UpdateDisplay();
    }

    // Update is called once per frame
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
                        teleport = Instantiate(teleportPrefab);
                        teleport.transform.position = touchPos;

                        break;
                    case TouchPhase.Moved:

                        teleport.SendMessage("HoldStarted");
                        teleport.transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
                        break;
                    case TouchPhase.Ended:
                        Debug.Log("Ended");
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
            //Debug.Log("ClickedButton");
        }
    }

    public void OnRelease()
    {
        if (teleport)
        {
            usedConsumable = false;
            Vector2 pos = new Vector2(Mathf.Round(teleport.transform.position.x), Mathf.Round(teleport.transform.position.y));
            //Debug.Log(pos);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0, layer);
            if (hit && hit.transform.tag == "FixedBlock")
            {
                // Teleporting the player
                GameObject.Find("Player").transform.position = pos;
                // Updating grid
                astar.SendMessage("UpdateGrid");
                astar.SendMessage("BreadthFirstSearch");

                Use();
            }
            Destroy(teleport);
            //Debug.Log("ReleasedButton");
        }
    }

    public void Add(int amount)
    {

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
