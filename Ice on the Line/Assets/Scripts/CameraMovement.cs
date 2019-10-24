using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Advanced camera
    /*
    public Rigidbody2D player;
    public Vector2 focusAreaSize;

    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom / 2));

        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftX;
            bottom += shiftX;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
    */

     //Basic Camera
    public Transform targetToFollow;
    [Range (0f, 1f)]public float cameraSmooth = 1f;

    private Vector3 cameraOffset;

    [SerializeField]
    private Renderer levelWidth = null;

    private void Start()
    {
        cameraOffset = new Vector3(0, transform.position.y - targetToFollow.position.y, 0);
        setCameraSizeToMatchHorizontally(levelWidth.bounds.size.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetToFollow)
        transform.position = Vector3.Lerp(transform.position, new Vector3 (transform.position.x, targetToFollow.position.y, transform.position.z) + cameraOffset, cameraSmooth);
    }

    // Reference: https://www.youtube.com/watch?v=3xXlnSetHPM
    private void setCameraSizeToMatchHorizontally(float sizeX)
    {
        Camera.main.orthographicSize = sizeX * Screen.height / Screen.width * 0.5f;
    }
}
