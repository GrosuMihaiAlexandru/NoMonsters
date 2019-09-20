using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public void PauseGameAction()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGameAction()
    {
        Time.timeScale = 1;
    }
}
