using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NextSceneDestination", menuName = "ScriptableObjects/NextSceneDestination")]
public class NextSceneDestination : ScriptableObject
{
    [SerializeField] private string _sceneName = "";

    public void SetNextSceneDestination(string sceneName)
    {
        _sceneName = sceneName;
    }
    public string GetNextSceneDestination()
    {
        return _sceneName;
    }

}
