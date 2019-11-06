using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThermometerSliderScript : MonoBehaviour
{
    Slider thermometerSlider;

    private Temperature temperature;

    private Vector3 normalScale;

    // Start is called before the first frame update
    void Start()
    {
        normalScale = transform.localScale;

        temperature = GameObject.Find("InGame").GetComponent<Temperature>();
        thermometerSlider = this.gameObject.GetComponent<Slider>();
        StartCoroutine(PulsateIfTooHot());
    }

    // Update is called once per frame
    void Update()
    {
        thermometerSlider.value = calculateTemperaturePercentage();
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
