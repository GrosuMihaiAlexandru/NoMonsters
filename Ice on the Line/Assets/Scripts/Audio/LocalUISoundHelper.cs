using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalUISoundHelper : MonoBehaviour
{
    public void PlayUISound(AudioClip clip)
    {
        SoundManager.instance.PlaySingle(clip);
    }
}
