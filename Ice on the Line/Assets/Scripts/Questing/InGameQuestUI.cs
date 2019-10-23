using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Displays the active quests Ingame
public class InGameQuestUI : MonoBehaviour
{

    private List<GameObject> quests = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            quests.Add(child.gameObject);
        }
    }

    public void UpdateProgress()
    {

    }
}
