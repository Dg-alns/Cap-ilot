using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Scriptable Objects/ScriptableQuestion")]
public class ScriptableQuestion : ScriptableObject
{
    public string question;
    public string correctAnswer;
    public List<string> wrongAnswer;
}
