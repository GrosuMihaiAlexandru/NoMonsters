using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Powerup))]
public class ExtraBlock : MonoBehaviour
{
    public List<GameObject> extraBlocks;

    private bool usedPowerup;

    private GameObject extraBlock;

    float deltaX, deltaY;

    bool moveAllowed = false;
    bool clicked = false;

    private Powerup powerup;

    // Start is called before the first frame update
    void Start()
    {
        powerup = GetComponent<Powerup>();
        powerup.Level = GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.extrablock);
        powerup.Count = GameManager.instance.GetPowerupUses(GameManager.Powerup.extrablock);
        powerup.UpdateText();
    }

    // Update is called once per frame
    void Update()
    {

        if (usedPowerup)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                Debug.Log("Being held");
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

    public void OnClick()
    {
        if (powerup.Count > 0)
        {
            powerup.UsePowerup();
            GameManager.instance.SetPowerupUses(GameManager.Powerup.extrablock, powerup.Count);
            usedPowerup = true;
            Debug.Log("ClickedButton");
        }
    }

    public void OnRelease()
    {
        if (extraBlock)
        {
            usedPowerup = false;
            extraBlock.GetComponentsInChildren<BoxCollider2D>().ToList().ForEach(x => x.enabled = true);
            Debug.Log("ReleasedButton");
        }
    }
}
