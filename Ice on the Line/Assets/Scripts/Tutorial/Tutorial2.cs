using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : MonoBehaviour
{

    public GameObject tutorial3;
    public IceBlock iceBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!iceBlock.CanSnap)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                gameObject.SetActive(false);
                tutorial3.SetActive(true);
            }
        }
    }
}
