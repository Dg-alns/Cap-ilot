using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{

    static public List<T> CreateList<T>(string nameGameObject)
    {
        GameObject go = GameObject.Find(nameGameObject);

        T[] allChild = go.GetComponentsInChildren<T>();

        List<T> objs = new List<T>(allChild);


        return objs;
    }
}
