using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePowerupDisplay : MonoBehaviour
{
    public bool powerupActive = false;

    private Image powerupImage;

    public List<Sprite> powerupSprites;

    public Image timerCircle;

    private void Awake()
    {
        powerupImage = GetComponentInChildren<Image>();
        StartCoroutine(PowerupTickUpdate(20f));
    }

    
    private IEnumerator PowerupTickUpdate(float time)
    {
        for(float i = 0; i < time; i += 0.01f)
        {
            timerCircle.fillAmount = ReMap(i, 0, 3, 0, 1);
            yield return new WaitForSeconds(.01f);
        }
    }

    private float ReMap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
