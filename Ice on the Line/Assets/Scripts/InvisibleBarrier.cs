using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBarrier : MonoBehaviour
{
    public LayerMask layerMask;

    public IceBlock iceBlock = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, layerMask);
        
        if (hit)
        {
            iceBlock = hit.collider.gameObject.transform.parent.gameObject.GetComponent<IceBlock>();
            iceBlock.barrierUnderneath = true;
        }
        else
        {
            if (iceBlock != null)
            {
                iceBlock.barrierUnderneath = false;
                iceBlock = null;
            }

        }
    }

}
