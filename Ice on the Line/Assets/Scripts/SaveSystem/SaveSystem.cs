using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveSystem
{

    public static void SaveData(int fish, int score, int[] upgrades, int[] uses, bool[] characters, QuestSaving[] quests, ulong questTime, int specialCurrency = 0, bool tutorial = true)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/newPlayerData.iotl";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(fish, score, upgrades, uses, characters, quests, questTime, specialCurrency, tutorial);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/newPlayerData.iotl";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            // Creating a default file if the file doesn't exist
            Debug.Log("Save file not found in " + path);
            Debug.Log("Creating default file...");
            SaveData(0, 0, new int[] { 0, 0, 0, 0, 0}, new int[] { 3, 3, 0 }, new bool[] { true, false, false}, null, 0, 0, false);
            return LoadData();
        }
    }
    
    public static void SaveCampaignProgress(List<Level> levels)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/campaignData.iotl";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, levels);
        stream.Close();
    }

    public static List<Level> LoadCampaignProgress()
    {
        string path = Application.persistentDataPath + "/campaignData.iotl";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            List<Level> levels = formatter.Deserialize(stream) as List<Level>;

            return levels;
        }
        else
        {
            Debug.Log("Save file not found " + path);
            Debug.Log("Creating default file...");
            List <Level> levels = new List<Level>
            {
                new Level(1, "Tutorial1", false, 0, false),
                new Level(2, "Tutorial2", false, 0, true),
                new Level(3, "Tutorial3", false, 0, true),
                new Level(4, "Level4", false, 0, true),
                new Level(5, "Level5", false, 0, true),
                new Level(6, "Level6", false, 0, true),
                new Level(7, "Level7", false, 0, true),
                new Level(8, "Level8", false, 0, true),
                new Level(9, "Level9", false, 0, true),
                new Level(10, "Level10", false, 0, true),
                new Level(11, "Level11", false, 0, true),
                new Level(12, "Level12", false, 0, true),
                new Level(13, "Level13", false, 0, true),
                new Level(14, "Level14", false, 0, true),
                new Level(15, "Level15", false, 0, true),
                new Level(16, "Level16", false, 0, true),
                new Level(17, "Level17", false, 0, true),
                new Level(18, "Level18", false, 0, true),
                new Level(19, "Level19", false, 0, true),
                new Level(20, "Level20", false, 0, true)
            };
            SaveCampaignProgress(levels);
            return LoadCampaignProgress();
        }
    }
}
