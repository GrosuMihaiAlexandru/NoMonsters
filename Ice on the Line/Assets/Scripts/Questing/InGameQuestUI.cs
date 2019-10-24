using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Displays the active quests Ingame
public class InGameQuestUI : MonoBehaviour
{
    private List<Text> descriptions = new List<Text>();
    private List<Text> progress = new List<Text>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform x in transform)
        {
            descriptions.Add(x.GetChild(0).GetComponent<Text>());
            progress.Add(x.GetChild(1).GetComponent<Text>());
        }

        /*
        for(int i = 0; i < descriptions.Count; i++)
        {
            Debug.Log(i + ": " + descriptions[i].text);
        }

        for (int i = 0; i < progress.Count; i++)
        {
            Debug.Log(i + ": " + progress[i].text);
        }
        */
    }

    public void UpdateQuestProgressOnUI()
    {/*
        for (int i = 0; i < descriptions.Count; i++)
        {
            Debug.Log(i + ": " + descriptions[i].text);
        }

        for (int i = 0; i < progress.Count; i++)
        {
            Debug.Log(i + ": " + progress[i].text);
        }
        */

        // Debug.Log(descriptions.Count);
        //Debug.Log(QuestManager.instance.Quests.Count);
        for (int i = 0; i < QuestManager.instance.Quests.Count; i++)
        {
            descriptions[i].text = QuestManager.instance.Quests[i].Description;
            progress[i].text = QuestManager.instance.Quests[i].Goals[0].CurrentAmount + "/" + QuestManager.instance.Quests[i].Goals[0].RequiredAmount;
        }
    }
}
