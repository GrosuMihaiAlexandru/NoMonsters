using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // The name of every quest in the game
    public List<string> questsBacklog = new List<string>();

    // The object that holds all of the assigned quests
    [SerializeField]
    private GameObject quests;

    // The type of quest to be assigned
    private string questType;
    // the list of assigned quests
    public List<Quest> Quests { get; set; } = new List<Quest>();

    // Number of assigned quests
    [SerializeField]
    private int questsAssigned = 0;

    private const int maxQuests = 3;

    void Start()
    {
        Debug.Log("Start");

        //NewQuests();

        //reading the saved quests first
        PlayerData data = SaveSystem.LoadData();
        if (data.activeQuests != null)
        {
            QuestSaving[] savedQuests = data.activeQuests;
            //Debug.Log(savedQuests.Length);

            for (int i = 0; i < savedQuests.Length; i++)
            {
                Debug.Log(savedQuests[i].QuestName);
                Quests.Add((Quest)quests.AddComponent(System.Type.GetType(savedQuests[i].QuestName)));
                Quests[i].Goals = savedQuests[i].Goals;
                questsAssigned++;
            }
        }

        // If there are less then 3 quests assigned then assign new quests
        if (questsAssigned < maxQuests)
            Invoke("NewQuests", 1);
    }

    public void UpdateQuests()
    {
        CheckQuest();

        // If there are less then 3 quests assigned then assign new quests
        Debug.Log("assigning new quests");
        while (questsAssigned < maxQuests)
        {
            questType = PickRandomQuests(questsBacklog.Count);
            AssignQuest();
        }
        
    }

    void AssignQuest()
    {
        if (questsAssigned < maxQuests)
        {
            questsAssigned++;
            Debug.Log(questType);
            // Adding the quest component to the gameobject and also adding it to the quest list
            Quests.Add((Quest) quests.AddComponent(System.Type.GetType(questType)));
            //GameManager.instance.SaveProgress();
        }
    }

    void SaveDelay()
    {
        GameManager.instance.SaveProgress();
    }

    void NewQuests()
    {
        while (questsAssigned < maxQuests)
        {
            questType = PickRandomQuests(questsBacklog.Count);
            AssignQuest();
        }
        Invoke("SaveDelay", 1);
        Debug.Log("Saved Quests");
    }
    void CheckQuest()
    {
        List<Quest> removedQuests = new List<Quest>();
        foreach(Quest q in Quests)
        {
            Debug.Log(q.QuestName);
            q.CheckGoals();
            if (q.Completed)
            {
                q.GiveReward();
                questsAssigned--;
                removedQuests.Add(q);
                Destroy(quests.GetComponent(q.QuestName));
            }

        }
        Quests = Quests.Except(removedQuests).ToList();
        GameManager.instance.SaveProgress();
    }

    string PickRandomQuests(int max)
    {
        string questName;
        do
        {
            int r = Random.Range(0, max);
            //Debug.Log(r);
            questName = questsBacklog[r];
            //Debug.Log(questName);
            // Repeat if the quest is already assigned
        } while (quests.GetComponent(System.Type.GetType(questName)) != null);
        return questName;
    }

    public List<QuestSaving> SaveQuests()
    {
        List<QuestSaving> questsSaved = new List<QuestSaving>();
        foreach(Quest q in Quests)
        {
            //Debug.Log(q.QuestName);
            questsSaved.Add(new QuestSaving(q.Goals, q.QuestName, q.Description, q.Reward, q.Completed));
        }

        return questsSaved;
    }

}
