using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVolume : MonoBehaviour
{
    private AudioManager audioManager;
    public Button soundToggleButton;
    public Sprite soundOn;
    public Sprite soundOff;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        UpdateIconAndVolume();
    }

    public void PauseVolume()
    {
        audioManager.ToggleVolume();
        UpdateIconAndVolume();
    }

    void UpdateIconAndVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            soundToggleButton.GetComponent<Image>().sprite = soundOn;
        }
        else
        {
            AudioListener.volume = 0;
            soundToggleButton.GetComponent<Image>().sprite = soundOff;
        }
    }
}
