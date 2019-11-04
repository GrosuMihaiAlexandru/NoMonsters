using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrollRectSnap : MonoBehaviour
{
    // The Scroll Panel
    public RectTransform panel;
    // Holding the Characters for selection
    public Image[] images;
    // Center to compare the distance for each character
    public RectTransform center;

    // The distance of each image from the center of the screen
    public float distanceFromCenter;

    public float[] distReposition;
    // All buttons' distance to the center
    private float[] distance;
    private bool dragging = false;
    // Holding the distance between the images
    private int imageDistance;
    // The number of the button with the smallest distance to center
    public int MinImageNum { get; private set; }
    private int imageLength;
    private bool messageSent = false;
    [SerializeField]
    private float lerpSpeed = 5f;

    [SerializeField]
    private bool changedCharacter = false;

    void Start()
    {
        imageLength = images.Length;
        distance = new float[imageLength];
        distReposition = new float[imageLength];

        // This formula works perfectly for odd numbers but has probles for even numbers
        // The last image keeps switching places between the left and right edge because both distances are about equal
        distanceFromCenter = Screen.width / 2 * images.Length;

        // Get distance between images
        imageDistance = (int)Mathf.Abs(images[1].GetComponent<RectTransform>().anchoredPosition.x - images[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    void Update()
    {

        for (int i = 0; i < images.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - images[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

            if (distReposition[i] > distanceFromCenter)
            {
                float curX = images[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = images[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX + (imageLength * imageDistance), curY);
                images[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }

            if (distReposition[i] < -distanceFromCenter)
            {
                float curX = images[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = images[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX - (imageLength * imageDistance), curY);
                images[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;

            }
        }

        // Get the min distance
        float minDistance = Mathf.Min(distance);

        if (dragging || !changedCharacter)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (minDistance == distance[i])
                {
                    MinImageNum = i;
                }
            }
        }
        if (!dragging)
        {
            LerpToImage(-images[MinImageNum].GetComponent<RectTransform>().anchoredPosition.x);
        }
    }

    void LerpToImage(float position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * lerpSpeed);

        if (Mathf.Abs(position - newX) < 7f)
        {
            lerpSpeed = 300f;
            newX = position;
        }

        //if (Mathf.Abs(newX) >= Mathf.Abs(position) - 2f && Mathf.Abs(newX) <= Mathf.Abs(position) + 2f && !messageSent)
       // {
           // GameManager.instance.selectedCharacter = MinImageNum;
           // Debug.Log("Send Message: " + images[MinImageNum].name);
           // messageSent = true;
       // }

        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        lerpSpeed = 5f;
        messageSent = false;
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }

    public void NextCharacter()
    {
        lerpSpeed = 5f;
        messageSent = false;
        // Delay until being able to press the button again
        if (!changedCharacter)
        {
            if (MinImageNum == images.Length - 1)
            {
                changedCharacter = true;
                MinImageNum = 0;
                Invoke("ChangeBack", 1f);
            }
            else
            {
                changedCharacter = true;
                MinImageNum++;
                Invoke("ChangeBack", 1f);
            }
        }
    }

    public void PreviousCharacter()
    {
        lerpSpeed = 5f;
        messageSent = false;
        if (!changedCharacter)
        {
            if (MinImageNum == 0)
            {
                changedCharacter = true;
                MinImageNum = images.Length - 1;
                Invoke("ChangeBack", 0.5f);
            }
            else
            {
                changedCharacter = true;
                MinImageNum--;
                Invoke("ChangeBack", 0.5f);
            }
        }
    }

    void ChangeBack()
    {
        changedCharacter = false;
    }
}
