using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/savegame.json";

    public static void SaveToFile(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static void LoadFromFile(PlayerData data)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, data);
        }
    }
}