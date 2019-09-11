using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // The gameObject that is pressed on
    public new GameObject gameObject;

    // Update is called once per frame
    void Update()
    {
        // Detecting mouseDown
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit)
            {
                // IceBlocks
                if (hit.collider.gameObject.transform.parent.gameObject.tag == "IceBlock")
                {
                    gameObject = hit.collider.gameObject.transform.parent.gameObject;
                    gameObject.SendMessage("DetectClick");
                }
            }
        }
        // Detecting mouseUp
        if (Input.GetMouseButtonUp(0))
        {
            if (gameObject)
            {
                if (gameObject.tag == "IceBlock")
                   gameObject.SendMessage("DetectClick");
            }
        }
    }
}
