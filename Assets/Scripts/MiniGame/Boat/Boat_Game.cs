using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Game : MonoBehaviour
{
    public GameObject prefab;

    [SerializeField] private float _targetTime = 2.0f;
    [SerializeField] private float _currentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if( _currentTime > _targetTime)
        {
            _currentTime = 0.0f;
            Instantiate(prefab);
        }
    }
}
