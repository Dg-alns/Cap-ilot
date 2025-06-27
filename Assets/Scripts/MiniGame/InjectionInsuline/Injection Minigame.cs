using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectionMinigame : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private List<float> _spawnTiming;

    private float _time;

    [SerializeField] private int _nbSpawn;

    private void Start()
    {
        _time = 0;
        _nbSpawn = 20;
        _spawnTiming = new List<float>();
        _spawnTiming.Add(Random.Range(0.0f, 1.5f));

        for (int i = 1; i < _nbSpawn; i++)
        {
            _spawnTiming.Add(Random.Range(_spawnTiming[i-1] + 1.0f , _spawnTiming[i-1] + 2.5f));
        }
    }
    private void Update()
    {
        if (_spawnTiming.Count > 0)
        {
            _time += Time.deltaTime;
            if (_spawnTiming[0] < _time)
            {
                //Debug.Log("_spawnTiming[0] : " + _spawnTiming[0]);
                //Debug.Log("_time : " + _time);
                GameObject timingCircle = GameObject.Instantiate(_prefab, transform);
                //timingCircle.transform.position = new Vector2(Random.Range(-(Camera.main.orthographicSize * Camera.main.aspect)/2, (Camera.main.orthographicSize * Camera.main.aspect)/2), Random.Range(-Camera.main.orthographicSize/2, Camera.main.orthographicSize/2));
                timingCircle.transform.position = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
                timingCircle.transform.SetAsFirstSibling();
                _spawnTiming.RemoveAt(0);
            }
        }
    }

}
