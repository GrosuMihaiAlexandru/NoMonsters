using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThermometerSliderScript : MonoBehaviour
{
    Slider thermometerSlider;

    private Temperature temperature;

    // Start is called before the first frame update
    void Start()
    {
        temperature = GameObject.Find("InGame").GetComponent<Temperature>();
        thermometerSlider = this.gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Temperature percentage: " + calculateTemperaturePercentage());
        Debug.Log("Global temperature: " + temperature.GlobalTemperature);
        Debug.Log("Max temperature: " + temperature.MaxTemperature);



        thermometerSlider.value = calculateTemperaturePercentage();
    }

    float calculateTemperaturePercentage() => 1.0f * (temperature.GlobalTemperature - temperature.InitialTemperature) / (temperature.MaxTemperature - temperature.InitialTemperature);
}
