using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    public GameObject tutorial2;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            gameObject.SetActive(false);
            tutorial2.SetActive(true);
        }
    }

    
}
