using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDPRScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int accepted = PlayerPrefs.GetInt("GDPRAccepted", 0);

        if (accepted != 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void AcceptPressed()
    {
        PlayerPrefs.SetInt("GDPRAccepted", 1);
        gameObject.SetActive(false);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public void OpenTermsAndConditions()
    {
        Application.OpenURL("http://www.oulugamelab.net/t-a-c");
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("http://www.oulugamelab.net/policy");
    }
}
