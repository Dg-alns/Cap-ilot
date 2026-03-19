using System.Collections.Generic;
using UnityEngine;

public enum SPAWN
{
    None,
    FirstStartGame,
    Port,
    Boat,
    Archipel,
    // IlePrincipale
    Phare,
    Village,
    // Hopital
    Couloir,
    Chambre,
    // Alimentation
    Restaurant,
    // Sport
    Terrain,
    // Ecole
    Etage1,
    Etage2,
    Cour,
    // Relation
    Parc,
}

public class Spawn : MonoBehaviour
{
    public SPAWN spaw = SPAWN.None;

    static List<string> AllSpawnList = new List<string>(System.Enum.GetNames(typeof(SPAWN)));

    public static int GetSpaw(string name)
    {
        for (int i = 0; i < AllSpawnList.Count; i++)
        {
            if (name.Contains(AllSpawnList[i]))
            {
                return i;
            }
        }
        return -1;
    }
}
