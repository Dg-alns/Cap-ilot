using System.Collections.Generic;
using UnityEngine;

public enum SPAWN
{
    None,
    FirstStartGame,
    Port,
    Boat,
    Archipel,
    Phare,
    Village,
    Couloir,
    Chambre,
    Restaurant,
    Terrain,
    Etage1,
    Etage2,
    Cour,
    Parc
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
