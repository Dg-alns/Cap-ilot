using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private List<NPC> _allNpc;
    public NPC dialogueNpc;
    
    public void Awake()
    {
        _allNpc = new List<NPC>();

        List<Type> npcSubclasses = GetAllSubclassesOf<NPC>();
        /*foreach (var subclass in npcSubclasses)
        {
            Debug.Log("NPC trouvé : " + subclass.Name);
        }*/

        foreach (var subclass in npcSubclasses)
        {
            UnityEngine.Object[] foundNpcs = Resources.FindObjectsOfTypeAll(subclass);

            foreach (var npc in foundNpcs)
            {
                if (npc is NPC npcInstance && npcInstance.gameObject.scene.IsValid())
                {
                    _allNpc.Add(npcInstance);
                    //Debug.Log("NPC ajouté : " + npcInstance.name);
                }
            }
        }

        //Debug.Log("Nombre de NPC trouvés : " + _allNpc.Count);
    }

    public static List<Type> GetAllSubclassesOf<T>()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(T)))
        .ToList();
    }

    public NPC FindNpcById(int id)
    {
        foreach (var npc in _allNpc)
        {
            Debug.Log("Recherche de type : " + npc.npcId);
            if (npc.npcId == id)
            {
                //Debug.Log("Clicked ID : " + npc.npcId);
                //Debug.Log(id);
                //Debug.Log(npc.name);
                dialogueNpc = npc;
                return dialogueNpc;
            }
        }
        
        return null; 
    }

    public void ResetDialogueNPC()
    {
        dialogueNpc = null;
    }
}
