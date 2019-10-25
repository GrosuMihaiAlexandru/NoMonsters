using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class QuestObject : MonoBehaviour
{
    public int questAvailable;

    public string questName;

    public Text description;
    public Text progress;
    public Text rewards;
    public Image completed;

    private InGameQuestUI questUI;

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("Started");
        questUI = transform.parent.GetComponent<InGameQuestUI>();

        description = transform.GetChild(0).GetComponent<Text>();
        progress = transform.GetChild(1).GetComponent<Text>();
        rewards = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        completed = transform.GetChild(3).GetComponent<Image>();

    }

    public void UpdateQuestProgressOnUI()
    {
        Debug.Log(questName);
        int i = QuestManager.instance.Quests.FindIndex(x => x.QuestName == questName);
        Debug.Log(i);
        Debug.Log(QuestManager.instance.Quests[i].Description);
        description.text = QuestManager.instance.Quests[i].Description;
        progress.text = QuestManager.instance.Quests[i].Goals[0].CurrentAmount + "/" + QuestManager.instance.Quests[i].Goals[0].RequiredAmount;
        rewards.text = QuestManager.instance.Quests[i].Reward.Fish.ToString();

        if (QuestManager.instance.Quests[i].Completed)
        {
            completed.sprite = questUI.checkBoxTicked;
        }
        else
        {
            completed.sprite = questUI.checkBox;
        }
    }
}
