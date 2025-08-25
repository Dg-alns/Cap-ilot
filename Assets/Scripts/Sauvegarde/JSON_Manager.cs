using System.IO;
using UnityEngine;

public static class JSON_Manager
{
    public static void SaveData<T>(string fileName, T data)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    //public static void SaveData<T>(string path, T data)
    //{
    //    string json = JsonUtility.ToJson(data, true);
    //    File.WriteAllText(path + json, json);
    //}

    public static T LoadData<T>(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);

    }
    //public static T LoadData<T>(string fileName)
    //{
    //    string json = File.ReadAllText(fileName + "json");
    //    return JsonUtility.FromJson<T>(json);

    //}
}
