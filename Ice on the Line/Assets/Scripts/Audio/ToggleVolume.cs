using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVolume : MonoBehaviour
{
    private SoundManager soundManager;
    public Button soundToggleButton;
    public Sprite soundOn;
    public Sprite soundOff;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            soundToggleButton.GetComponent<Image>().sprite = soundOn;
            soundManager.ToggleAllSounds(false);
        }
        else
        {
            soundToggleButton.GetComponent<Image>().sprite = soundOff;
            soundManager.ToggleAllSounds(true);
        }
        transform.parent.gameObject.SetActive(false);
    }

    public void PauseVolume()
    {
        UpdateIconAndVolume();
    }

    void UpdateIconAndVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            Debug.Log("a11111");
            soundManager.ToggleAllSounds(true);

            soundToggleButton.GetComponent<Image>().sprite = soundOff;
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            Debug.Log("a2@22222");
            soundManager.ToggleAllSounds(false);

            soundToggleButton.GetComponent<Image>().sprite = soundOn;
            PlayerPrefs.SetInt("Muted", 0);
        }
    }
}
