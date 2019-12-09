using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePowerupDisplay : MonoBehaviour
{
    public PowerupManager PowerupManager;

    public bool powerupActive = false;

    public Image powerupImage;
    public Image powerupIcon;
    public Image fillAmount;

    public List<Sprite> powerupSprites;

    public Image timerCircle;

    public enum Powerup { magnet, fishDoubler, timeFreeze}

    // The 2 colors for blinking
    private Color defaultColor = new Color32(255, 255, 255, 200);
    private Color fadeColor = new Color32(255, 255, 255, 125);

    public Image timeFreezeEffect;

    void Start()
    {
        InGameEvents.OnPowerupCollected += ActivatePowerupDisplay;
        MakeObjectInvisible();
    }

    public void OnDestroy()
    {
        InGameEvents.OnPowerupCollected -= ActivatePowerupDisplay;
    }

    private IEnumerator PowerupTickUpdate(float time)
    {
        for(float i = 0; i <= time; i += 0.2f)
        {
            timerCircle.fillAmount = ReMap(i, 0, time, 0, 1);
            yield return new WaitForSeconds(0.2f);
        }

        // Hide the object when it's done running
        MakeObjectInvisible();
    }

    private IEnumerator PowerupBlinking(float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        for (float i = 0; i <= time*2; i++)
        {
            //Debug.Log(i);
            if (i % 2 != 0)
            {
                powerupImage.color = fadeColor;
                powerupIcon.color = fadeColor;
            }
            else
            {
                powerupImage.color = defaultColor;
                powerupIcon.color = defaultColor;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    public IEnumerator PowerupScreenEffectFade(float time, Image image, float alpha)
    {
        image.color = new Color32(255, 255, 255, 150);
        for (float i = time; i >= 0; i-= 0.2f)
        {
            image.color = new Color32(255, 255, 255, (byte)ReMap(i, 0, time, 0, alpha));
            yield return new WaitForSeconds(0.2f);
        }
        image.color = new Color32(255, 255, 255, 0); 
    }

    public void ActivatePowerupDisplay(IPowerup powerup)
    {
        MakeObjectVisible();
        //Debug.Log(powerup.ID);

        // Set the right icon of the powerup
        
        // Get the duration of the powerup
        float time;
        switch(powerup.ID)
        {
            case 10:
                time = PowerupManager.GetPowerupTimer(PowerupManager.Powerup.magnet);
                powerupIcon.sprite = powerupSprites[0];
                break;
            case 11:
                time = PowerupManager.GetPowerupTimer(PowerupManager.Powerup.fishDoubler);
                powerupIcon.sprite = powerupSprites[1];
                break;
            case 12:
                Debug.Log("Time freeze");
                time = PowerupManager.GetPowerupTimer(PowerupManager.Powerup.timeFreeze);
                powerupIcon.sprite = powerupSprites[2];
                StartCoroutine(PowerupScreenEffectFade(time, timeFreezeEffect, 150));
                break;
            default:
                time = 0;
                break;
        }

        if (time != 0)
        {
            StartCoroutine(PowerupTickUpdate(time));
            StartCoroutine(PowerupBlinking(time / 4, time / 4 * 3));
        }
    }

    private float ReMap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    private void MakeObjectVisible()
    {
        //Debug.Log(powerupIcon);
        powerupIcon.enabled = true;
        powerupImage.enabled = true;
        fillAmount.enabled = true;
    }

    private void MakeObjectInvisible()
    {
        powerupIcon.enabled = false;
        powerupImage.enabled = false;
        fillAmount.enabled = false;
    }
}
