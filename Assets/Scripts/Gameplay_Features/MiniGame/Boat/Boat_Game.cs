using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat_Game : Minigame
{
    public List<GameObject> prefab;

    [SerializeField] private Boat_Boat _boat;

    private float _targetTimeSpawn;
    private float _currentTimeSpawn = 0.0f;

    private int _targetTimeWin; 
    [SerializeField] private Boat_ProgressBar _timer;

    private bool _gameFinish = false;
    private bool _gameStart = false;

    [SerializeField] List<BoatMer> _mer;

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
                foreach(BoatMer mer in _mer)
                {
                    mer.ChangeForWinSpeed();
                }
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

    public bool IsGameStart()
    {
        return _gameStart;
    }

    public override void PauseMinigame()
    {
        _timer.isRunning = false;
        _gameStart = false;
        foreach(BoatMer mer in _mer)
        {
            mer.isPause = true;
        }
        foreach (Boat_SimpleRock rock in _rocks.GetComponentsInChildren<Boat_SimpleRock>())
        {
            rock.isPause = true;
        }


    }

    public override void ResumeMinigame()
    {
        _timer.isRunning = true;
        _gameStart = true;
        foreach (BoatMer mer in _mer)
        {
            mer.isPause = false;
        }
        foreach (Boat_SimpleRock rock in _rocks.GetComponentsInChildren<Boat_SimpleRock>())
        {
            rock.isPause = false;
        }
    }
}
