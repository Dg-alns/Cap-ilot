using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_SystemParticle : MonoBehaviour
{
    public Boat_Boat Boat_Boat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Boat_Boat.transform.position;
    }
}
