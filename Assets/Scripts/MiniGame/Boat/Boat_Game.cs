using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat_Game : MonoBehaviour
{
    public List<GameObject> prefab;

    [SerializeField] private Boat_Boat _boat;

    private float _targetTimeSpawn;
    private float _currentTimeSpawn = 0.0f;

    private int _targetTimeWin; 
    [SerializeField] private Boat_ProgressBar _timer;

    private bool _gameFinish = false;
    private bool _gameStart = false;
    private bool _CoroutineStart = false;

    private Transform _rocks;

    // Start is called before the first frame update
    void Start()
    {
        _targetTimeSpawn = Random.Range(2.0f, 3.0f);
        _targetTimeWin = 20;
        _rocks = GameObject.Find("Rocks").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStart == false)
            return;


        // After 20 second without any hit
        if (_gameFinish)
        {
            _boat.Winning();            
            return;
        }

        // If Win
        if (_timer.GetTime() >= _targetTimeWin)
        {
            // Wait for no rock
            if(_rocks.childCount == 0)
            {
                _boat.SetAnime(AnimationBoatState.Win);
                _gameFinish = true;
            }
            return;
        }

        // Spawn Rock
        _currentTimeSpawn += Time.deltaTime;
        if( _currentTimeSpawn > _targetTimeSpawn)
        {
            _currentTimeSpawn = 0.0f;
            int r = Random.Range(0, prefab.Count);
            Instantiate(prefab[r],_rocks);
        }
    }

    public void StartGame()
    {
        _gameStart = true;
        _timer.Run();
    }

}
