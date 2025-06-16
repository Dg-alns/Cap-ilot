using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private float _width;
    [SerializeField] private float _speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        _width = Screen.currentResolution.width;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_width / 2 < Input.mousePosition.x)
                Debug.Log("ClickRight");
            else
                Debug.Log("ClickLeft");
        }
    }
}
