using UnityEngine;
using System.IO;

public class Thing : MonoBehaviour
{
    private ThingModel Model;
    private string jsonString;

    private void Awake()
    {
        Model = new ThingModel();
        Model.Name = "Greg";
                Model.Color = "Green";
                Model.Number = 12;
                Model.IsSquare = true;
                string jsonString = JsonUtility.ToJson(Model);
                string fileName = "values.json";
        File.WriteAllText(fileName, jsonString);
/*        jsonString = File.ReadAllText("C:/Users/ederveaux/Documents/GitHub/Cap-ilot/Cap'îlot/values.json");
        Model = JsonUtility.FromJson<ThingModel>(jsonString);
        print(jsonString);
        print($"Name: {Model.Name}, Number: {Model.Number}, Color: {Model.Color}, IsSquare: {Model.IsSquare}");*/
    }
}

[System.Serializable]
public class ThingModel
{
    public string Name;
    public string Color;
    public int Number;
    public bool IsSquare;
}
