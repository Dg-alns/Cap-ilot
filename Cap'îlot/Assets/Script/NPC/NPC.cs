using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int npcId;
    public string npcName;
    [TextArea]
    public List<string> dialogueLines;
}
