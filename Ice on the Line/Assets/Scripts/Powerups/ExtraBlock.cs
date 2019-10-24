using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExtraBlock : Powerup
{
    public List<GameObject> extraBlocks;

    private bool usedPowerup;

    private GameObject extraBlock;

    float deltaX, deltaY;

    bool moveAllowed = false;
    bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        ID = 0;
        level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.extrablock);
        count = GameManager.instance.GetPowerupUses(GameManager.Powerup.extrablock);
        UpdateText();
    }   
   
    // The Implementation of the powerup mechanics
    void Update()
    {

        if (usedPowerup)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                //Debug.Log("Being held");
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        extraBlock = Instantiate(extraBlocks[0]);
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
        if (count > 0)
        {
            UsePowerup();
            usedPowerup = true;
            Debug.Log("ClickedButton");
        }
    }

    // Event triggerd on PowerupRelease
    public void OnRelease()
    {
        if (extraBlock)
        {
            usedPowerup = false;
            extraBlock.GetComponentsInChildren<BoxCollider2D>().ToList().ForEach(x => x.enabled = true);
            Debug.Log("ReleasedButton");
        }
    }

    public override void UsePowerup()
    {
        InGameEvents.PowerupUsed(this);
        count--;
        GameManager.instance.SetPowerupUses(GameManager.Powerup.extrablock, count);
        GameManager.instance.SaveProgress();
        UpdateText();
    }

    public override void AddPowerup(int value)
    {
        count += value;
        GameManager.instance.SetPowerupUses(GameManager.Powerup.extrablock, count);
        GameManager.instance.SaveProgress();
    }

}
