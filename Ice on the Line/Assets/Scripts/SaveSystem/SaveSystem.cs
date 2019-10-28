using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{

    public static void SaveData(int fish, int score, int[] upgrades, int[] uses, QuestSaving[] quests, ulong questTime, bool tutorial = true)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.iotl";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(fish, score, upgrades, uses, quests, questTime, tutorial);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/playerData.iotl";

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
            SaveData(0, 0, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, null, 0, false);
            return LoadData();
        }
    }
    
}
