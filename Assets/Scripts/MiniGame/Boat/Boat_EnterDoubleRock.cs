using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_EnterDoubleRock : Boat_SimpleRock
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(Random.Range(_widthCam / 2 - _widthSprite, -_widthCam / 2 + _widthSprite), transform.position.y);
        Debug.Log(_widthSprite);
    }

}
