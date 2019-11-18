using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpout : MonoBehaviour, IDestructable
{
    public LayerMask layerMask;

    public IceBlock iceBlock = null;

    private BoxCollider2D boxCollider;

    public void DestroySelf()
    {
        Debug.Log("Destroyed geyser");
        GameObject obstacle = gameObject;

        Destroy(obstacle);

    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, layerMask);

        if (hit)
        {
            Debug.Log(hit.transform.tag);

            iceBlock = hit.collider.gameObject.GetComponent<Hologram>().parentBlock;
            iceBlock.barrierUnderneath = true;
        }
        else
        {
            if (iceBlock != null)
            {
                Debug.Log(iceBlock);
                iceBlock.barrierUnderneath = false;
                iceBlock = null;
            }

        }
    }

}
