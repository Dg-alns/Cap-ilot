using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

static public class JSON_Manager
{

    static public void SaveData<T>(string path, T data)
    {
        string json = JsonConvert.SerializeObject(data,Formatting.Indented);
        File.WriteAllText(path, json);
    }

    static public T LoadData<T>(string path)
    {
        var value = File.ReadAllText(path);
        T data = JsonConvert.DeserializeObject<T>(value);
        return data;
    }
}