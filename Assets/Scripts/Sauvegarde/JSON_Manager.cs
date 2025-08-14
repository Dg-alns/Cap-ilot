using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Unity.Services.Friends.Models;
using UnityEngine;

static public class JSON_Manager
{

    static public void SaveData<T>(string path, T data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    static public T LoadData<T>(string path)
    {
        string json = File.ReadAllText(path);
        T data = JsonUtility.FromJson<T>(json);
        return data;
    }
}