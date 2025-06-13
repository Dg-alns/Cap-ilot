using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class NavigationMap : MonoBehaviour
{
    private NavMeshSurface Surface;
    // Start is called before the first frame update
    void Start()
    {
        Surface = GetComponent<NavMeshSurface>();
        Surface.BuildNavMeshAsync();
    }

    // Update is called once per frame
    void Update()
    {
        //Surface.UpdateNavMesh(Surface.navMeshData);
    }
}
