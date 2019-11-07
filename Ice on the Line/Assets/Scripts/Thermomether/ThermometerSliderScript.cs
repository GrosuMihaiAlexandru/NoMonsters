using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThermometerSliderScript : MonoBehaviour
{
    Slider thermometerSlider;

    private Temperature temperature;

    private Vector3 normalScale;

    [SerializeField]
    private List<Sprite> thermometerSprites = new List<Sprite>();

    private Image thermometerRenderer;

    private int selectedSprite = -1;

    // Start is called before the first frame update
    void Start()
    {
        thermometerRenderer = transform.GetChild(1).GetComponent<Image>();
        normalScale = transform.localScale;

        temperature = GameObject.Find("InGame").GetComponent<Temperature>();
        thermometerSlider = this.gameObject.GetComponent<Slider>();
        StartCoroutine(PulsateIfTooHot());
    }

    // Update is called once per frame
    void Update()
    {
        float temp = calculateTemperaturePercentage();

        thermometerSlider.value = temp;
        if (temp <= 0.33f)
        {
            TryChangingThermometerSprite(0);
        }
        else if (temp > 0.33f && temp <= 0.66f)
        {
            TryChangingThermometerSprite(1);
        }
        else if (temp > 0.66f)
        {
            TryChangingThermometerSprite(2);
        }
    }

    private void TryChangingThermometerSprite(int index)
    {
        if (selectedSprite != index)
        {
            thermometerRenderer.sprite = thermometerSprites[index];
            selectedSprite = index;
        }
    }

    IEnumerator PulsateIfTooHot()
    {
        while (true)
        {
            if (calculateTemperaturePercentage() > 0.66f)
            {
                transform.localScale = normalScale * (0.95f + Mathf.PingPong(0.1f * Time.time, 0.1f));
            }
            else
            {
                transform.localScale = normalScale;
            }
            yield return null;
        }
    }

    float calculateTemperaturePercentage() => 1.0f * (temperature.GlobalTemperature - temperature.InitialTemperature) / (temperature.MaxTemperature - temperature.InitialTemperature);
}
