using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;

    public AudioSource musicSource;

    public static SoundManager instance = null;

    [SerializeField]
    private float lowPitchRange = .95f;

    [SerializeField]
    private float highPitchRange = 1.05f;

    public AudioClip startDragIceBlockSoundClip = null;
    public AudioClip stopDragWithoutSnapIceBlockSoundClip = null;
    public AudioClip snapIceBlockSoundClip = null;
    public AudioClip rotateIceBlockSoundClip = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayStartDragIceBlockSoundClip()
    {
        PlaySingle(startDragIceBlockSoundClip);
    }

    public void PlayStopDragWithoutSnapIceBlockSoundClip()
    {
        PlaySingle(stopDragWithoutSnapIceBlockSoundClip);
    }
    public void PlaySnapIceBlockSoundClip()
    {
        PlaySingle(snapIceBlockSoundClip);
    }
    public void PlayRotateIceBlockSoundClip()
    {
        PlaySingle(rotateIceBlockSoundClip);
    }
    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

}
