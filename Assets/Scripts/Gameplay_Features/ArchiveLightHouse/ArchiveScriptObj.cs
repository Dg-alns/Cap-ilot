using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Scriptable Objects/ScriptableArchive")]
public class ArchiveScriptObj : ScriptableObject
{
    
    public string _title;
    public string _link;
    [TextAreaAttribute]
    public string _content;
}
