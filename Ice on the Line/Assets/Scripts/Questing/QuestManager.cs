using System;
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

        Debug.Log("Start");

        //reading the saved quests first
        PlayerData data = SaveSystem.LoadData();

        // get the last time quests were assigned
        QuestsAssignedTime = data.questsAssignedTime;
        //Debug.Log(QuestsAssignedTime);

        //NewQuests();

        // If no quests were assigned before
        if (QuestsAssignedTime == 0)
        {
            NewQuests();
        }
        else
        {
            Debug.Log("Quests already assigned");
            // Check if it's time to assign new quests
            ulong timeDiff = ((ulong)DateTime.Now.Ticks - QuestsAssignedTime);
            ulong m = timeDiff / TimeSpan.TicksPerMillisecond;
            // Convert the time in seconds
            float secondsLeft = (float)(msTimeToWait - m) / 1000.0f;
            //Debug.Log("Assigning new quests in: " + secondsLeft);
            // Assign new quests if it's a new day
            if (secondsLeft < 0)
            {
                Invoke("NewQuests", 1);
            }
            // Load the existing quests if there are any
            else
            {
                // Verify that there are active quests
                if (data.activeQuests != null)
                {
                    //Debug.Log("Quests active");
                    QuestSaving[] savedQuests = data.activeQuests;
                    //Debug.Log(savedQuests.Length);

                    for (int i = 0; i < savedQuests.Length; i++)
                    {
                        //Debug.Log("saved quests: " + savedQuests[i].QuestName);
                        Quests.Add((Quest)quests.AddComponent(System.Type.GetType(savedQuests[i].QuestName)));
                        Quests[i].Goals = savedQuests[i].Goals;
                        questsAssigned++;
                    }
                }
            }
        }

        // Quests.Clear();
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

    public ulong QuestsAssignedTime { get; set; }

    [SerializeField]
    private float msTimeToWait = 86400000; // value for 1 day

    void Start()
    {
    }

    //public Text text;

    // Returns the time left until assigning new quests as a string
    public string TimeLeftUntilNewQuests()
    {
        // Check if it's time to assign new quests
        ulong timeDiff = ((ulong)DateTime.Now.Ticks - QuestsAssignedTime);
        ulong m = timeDiff / TimeSpan.TicksPerMillisecond;
        // Convert the time in seconds
        float secondsLeft = (float)(msTimeToWait - m) / 1000.0f;

        string timeLeft = "";
        // Hours
        timeLeft += ((int)secondsLeft / 3600).ToString("00") + "h ";
        secondsLeft -= ((int)secondsLeft / 3600) * 3600;
        // Minutes 
        timeLeft += ((int)secondsLeft / 60).ToString("00") + "m";
        // Seconds
        //timeLeft += (secondsLeft % 60).ToString("00") + "s";

        return timeLeft;
    }

    public void ceva()
    {
        QuestsAssignedTime = (ulong)DateTime.Now.Ticks;
    }



    private void Update()
    {
       // text.text = TimeLeftUntilNewQuests();
    }

    public void UpdateQuests()
    {
        CheckQuest();

        // If there are less then 3 quests assigned then assign new quests
        //Debug.Log("assigning new quests");
        /*while (questsAssigned < maxQuests)
        {
            questType = PickRandomQuests(questsBacklog.Count);
            AssignQuest();
        }*/
        
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
        QuestsAssignedTime = (ulong)DateTime.Now.Ticks;

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
        foreach(Quest q in Quests)
        {
            //Debug.Log(q.QuestName);
            q.CheckGoals();
        }
        GameManager.instance.SaveProgress();
    }

    public void RemoveQuest(string questName)
    {
        int i = Quests.FindIndex(x => x.QuestName == questName);
        if (Quests[i].Completed)
        {
            Debug.Log(Quests[i].QuestName + " completed");
            Quests[i].GiveReward();
            questsAssigned--;
            Destroy(quests.GetComponent(Quests[i].QuestName));
            Quests.RemoveAt(i);

            GameManager.instance.SaveProgress();
        }
    }

    string PickRandomQuests(int max)
    {
        string questName;
        do
        {
            int r = UnityEngine.Random.Range(0, max);
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
