using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThermometerSliderScriptAndMusicHelper : MonoBehaviour
{
    Slider thermometerSlider;

    private Temperature temperature;

    private Vector3 normalScale;

    [SerializeField]
    private List<Sprite> thermometerSprites = new List<Sprite>();

    private Image thermometerRenderer;

    private int selectedSpriteAndSongSpeed = -1;

    [SerializeField]
    private List<AudioClip> speed1Clips = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> speed2Clips = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> speed3Clips = new List<AudioClip>();

    private int selectedSong = 0;

    // Start is called before the first frame update
    void Start()
    {
        selectedSong = Random.Range(0, 3);

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
        if (selectedSpriteAndSongSpeed != index)
        {
            thermometerRenderer.sprite = thermometerSprites[index];
            selectedSpriteAndSongSpeed = index;

            if (index == 0)
            {
                SoundManager.instance.musicSource.clip = speed1Clips[selectedSong];
                SoundManager.instance.musicSource.Play();
            }
            else if (index == 1)
            {
                SoundManager.instance.musicSource.clip = speed2Clips[selectedSong];
                SoundManager.instance.musicSource.Play();
            }
            else
            {
                SoundManager.instance.musicSource.clip = speed3Clips[selectedSong];
                SoundManager.instance.musicSource.Play();
            }
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
