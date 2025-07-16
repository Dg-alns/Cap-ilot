using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public enum SPAWN
{
    None,
    FirstStartGame,
    Port,
    Bateau,
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

    private void Start()
    {
        Assert.True(spaw != SPAWN.None);
    }
}
