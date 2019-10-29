using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Displays the active quests Ingame
public class InGameQuestUI : MonoBehaviour
{
    public List<GameObject> quests;
    
    // Images for updating the checkboxes
    public Sprite checkBox;
    public Sprite checkBoxTicked;

    public Text updateQuestsTimer;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform x in transform)
        {
            quests.Add(x.gameObject);
        }
        Invoke("ReadQuests", 0.1f);

        transform.parent.parent.gameObject.SetActive(false);
    }

    void Start()
    {


    }

    void Update()
    {
        updateQuestsTimer.text = "Refreshes in: " +  QuestManager.instance.TimeLeftUntilNewQuests();
    }

    public void ReadQuests()
    {
        Debug.Log("ReadingQuests");
        int i = 0;
        for (; i < QuestManager.instance.Quests.Count; i++)
        {
            Debug.Log(QuestManager.instance.Quests[i].QuestName);
            quests[i].GetComponent<QuestObject>().questName = QuestManager.instance.Quests[i].QuestName;
        }
        for (int j = i; j < 3; j++)
        {
            //quests[j].SetActive(false);
        }
    }

    public void UpdateQuestsDisplay()
    {
        ReadQuests();
        foreach (var x in quests)
        {
            x.GetComponent<QuestObject>().UpdateQuestProgressOnUI();
        }
    }
    

    public void Quest1()
    {
        Quest quest = QuestManager.instance.Quests[0];
    }

    public void Quest2()
    {

    }

    public void Quest3()
    {

    }
}
