using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    public static int campaignVersion = 2;

    public static void SaveData(int fish, int score, int lives, int[] upgrades, int[] uses, bool[] characters, QuestSaving[] quests, ulong questTime, ulong lifeTime, int specialCurrency = 0, bool tutorial = true)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/newPlayerData.iotl";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(fish, score, lives, upgrades, uses, characters, quests, questTime, lifeTime, specialCurrency, tutorial);

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
            SaveData(0, 0, 0, new int[] { 0, 0, 0, 0, 0 }, new int[] { 3, 3, 0 }, new bool[] { true, false, false }, null, 0, 0, 0, false);
            return LoadData();
        }
    }

    public static void SaveCampaignProgress(List<Level> levels)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/campaignData1.iotl";

        FileStream stream = new FileStream(path, FileMode.Create);

        Debug.Log("saving: v" + campaignVersion);
        CampaignData data = new CampaignData(campaignVersion, levels);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static List<Level> LoadAndUpdateLevels(int version, List<Level> levels)
    {
        Debug.Log(version);
        List<Level> newLevels = levels; 
        while (version < campaignVersion)
        {
            Debug.Log("updating version: v" + version + " to version: " + (version + 1));
            switch(version)
            {
                case 1:
                    List<Level> versionLevels2 = new List<Level>
                    {
                        new Level(21, "Level21", false, 0, false),
                        new Level(22, "Level22", false, 0, true),
                        new Level(23, "Level23", false, 0, true),
                        new Level(24, "Level24", false, 0, true),
                        new Level(25, "Level25", false, 0, true),
                        new Level(26, "Level26", false, 0, true),
                        new Level(27, "Level27", false, 0, true),
                        new Level(28, "Level28", false, 0, true),
                        new Level(29, "Level29", false, 0, true),
                        new Level(30, "Level30", false, 0, true),
                        new Level(31, "Level31", false, 0, true),
                        new Level(32, "Level32", false, 0, true),
                        new Level(33, "Level33", false, 0, true),
                        new Level(34, "Level34", false, 0, true),
                        new Level(35, "Level35", false, 0, true),
                        new Level(36, "Level36", false, 0, true),
                        new Level(37, "Level37", false, 0, true),
                        new Level(38, "Level38", false, 0, true),
                        new Level(39, "Level39", false, 0, true),
                        new Level(40, "Level40", false, 0, true)
                    };
                    newLevels.AddRange(versionLevels2);
                    break;
                case 2:
                    break;
            }
            version++;
        }
        return newLevels;
    }

    public static List<Level> LoadCampaignProgress()
    {
        string path = Application.persistentDataPath + "/campaignData1.iotl";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            CampaignData data = formatter.Deserialize(stream) as CampaignData;
            stream.Close();

            List<Level> levels = LoadAndUpdateLevels(data.campaignVersion, data.levels);

            return levels;
        }
        else
        {
            Debug.Log("Save file not found " + path);
            Debug.Log("Creating default file...");

            List<Level> levels = CreateLatestVersionLevels();
            SaveCampaignProgress(levels);
            return LoadCampaignProgress();
        }
    }

    private static List<Level> CreateLatestVersionLevels()
    {
        List<Level> levels = new List<Level>
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
                new Level(20, "Level20", false, 0, true),
                new Level(21, "Level21", false, 0, false),
                new Level(22, "Level22", false, 0, true),
                new Level(23, "Level23", false, 0, true),
                new Level(24, "Level24", false, 0, true),
                new Level(25, "Level25", false, 0, true),
                new Level(26, "Level26", false, 0, true),
                new Level(27, "Level27", false, 0, true),
                new Level(28, "Level28", false, 0, true),
                new Level(29, "Level29", false, 0, true),
                new Level(30, "Level30", false, 0, true),
                new Level(31, "Level31", false, 0, true),
                new Level(32, "Level32", false, 0, true),
                new Level(33, "Level33", false, 0, true),
                new Level(34, "Level34", false, 0, true),
                new Level(35, "Level35", false, 0, true),
                new Level(36, "Level36", false, 0, true),
                new Level(37, "Level37", false, 0, true),
                new Level(38, "Level38", false, 0, true),
                new Level(39, "Level39", false, 0, true),
                new Level(40, "Level40", false, 0, true)
            };
        return levels;
    }
}
