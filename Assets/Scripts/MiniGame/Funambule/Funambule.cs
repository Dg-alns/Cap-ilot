using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funambule : Minigame
{
    [Header("Body Part")]
    [SerializeField] private Transform _playerFootPoint;
    [SerializeField] private Transform _playerHeatPoint;
    private float _currentDistanceForWin;
    private Vector3 _playerFootPosition;

    //Decrease Speed Game
    [SerializeField] private float _walkMinZRotation = 5.0f;
    [SerializeField] private float _walkLimitZRotation = 30.0f;
    [SerializeField] private float _loosingZRotation = 45.0f;

    private bool _isFalling = false;
    [SerializeField] private float _fallingTimeDuration = 3.0f;
    private float _fallingTimeCurrent = 0.0f;

    [Header("Body Value")]
    [SerializeField] private float _rotationSpeed = 30.0f;
    [SerializeField] private float _autoBasculeStrengh = 10.0f;

    [Header("Platform Part")]
    [SerializeField] private Transform _platformPoint;
    private float _maxSpeedGame;
    private float _speedGame;

    [Header("Eyes")]
    [SerializeField] private List<GameObject> _Eyes;
    [SerializeField] private float _eyeDuration = 3.0f;
    [SerializeField] private float _eyeTimeAppear = 5.0f;
    [SerializeField] private float _eyeStrengh = 15.0f;
    private bool _isEyeOpen = false;
    private float _timeEye = 0.0f;
    private int _activeEye = -1;
    private int[] _directionEyeRotation = new int[2];
    private float _animationDurationEye = 0.5f;

    [Header("Score")]
    [SerializeField] private Score _score;

    Vector2 startPosition;
    float totalDistance;
    float initialScale = 1.0f;
    float endScale = 2.1f;
    float _dt;
    float zRotation;

    bool isStart = false;
    bool isWin = false;

    private Tools _tools;

    // Start is called before the first frame update
    void Start()
    {
        _directionEyeRotation[0] = -1;
        _directionEyeRotation[1] = 1;

        startPosition = _platformPoint.transform.position;
        _maxSpeedGame = (Mathf.Abs(_playerFootPoint.position.y) + Mathf.Abs(_platformPoint.position.y)) / 30.0f;
        //Debug.Log("Speed : " + _speedGame);
        _playerFootPosition = _playerFootPoint.position;
        totalDistance = Vector2.Distance(_playerFootPosition, _platformPoint.position);

        _tools = FindAnyObjectByType<Tools>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin || !isStart)
            return;

        _dt = Time.deltaTime;

        // Apply the falling time
        if(_isFalling)
        {
            ApplyFalling();
            return;
        }

        // Make appear/disappear the eye and apply thier effect
        EyeMethode();

        // Slide the player to the right/left if his already have a low rotation
        AutoBascule();

        // Click Right or Left side and rotate the playerto the other side 
        ClickSideRotation();

        // More you are in rotation lower will be your speed
        ChangeSpeedByRotation();

        // Move the platform in your position to make a walk illusion
        MovePlatform();

        // Check and apply if you will fall
        CheckOverDegres();

        CheckWin();
    }

    public void EyeMethode()
    {
        _timeEye += _dt;
        if (_isEyeOpen)
        {
            if (_timeEye <= _animationDurationEye) 
            {
                // The eye apply a rotation proportionnal to his scale
                _playerFootPoint.transform.Rotate(new Vector3(0.0f, 0.0f, (_timeEye / _animationDurationEye) * _directionEyeRotation[_activeEye] * _eyeStrengh * _dt));
            }
            else
            {
                // The eye apply the basic rotation 
                _playerFootPoint.transform.Rotate(new Vector3(0.0f, 0.0f, _directionEyeRotation[_activeEye] * _eyeStrengh * _dt));
            }
            if(_eyeDuration <= _timeEye)
            {
                // Close the eye
                SwitchEye();
                _activeEye = -1;
            }
        }
        else
        {
            if(_eyeTimeAppear <= _timeEye)
            {
                // Open the eye
                _activeEye = Random.Range(0, 2);
                SwitchEye();
            }
        }
    }

    private void SwitchEye()
    {
        _timeEye = 0.0f;
        _isEyeOpen = !_isEyeOpen;
        _Eyes[_activeEye].GetComponent<Animator>().SetTrigger("Switch");
    }

    public void ChangeSpeedByRotation()
    {
        // Get Degres rotation
        zRotation = _playerFootPoint.eulerAngles.z <= 180f ? _playerFootPoint.eulerAngles.z : _playerFootPoint.eulerAngles.z - 360f;
        
        // Apply the basic speed the player still strait
        if (Mathf.Abs(zRotation) <= _walkMinZRotation)
        {
            _speedGame = _maxSpeedGame;
            return;
        }
        // Apply a slow/stop speed went the player is in rotation over the limit
        if (Mathf.Abs(zRotation) <= _walkLimitZRotation)
        {
            _speedGame = _maxSpeedGame * (1-((Mathf.Abs(zRotation) - _walkMinZRotation) / (_walkLimitZRotation - _walkMinZRotation)));
            return;
        }
        _speedGame = 0.0f;
    }

    public void MovePlatform(float direction = 1.0f)
    {
        // Check don't go before the start
        if (direction < 0) 
        {
            _currentDistanceForWin = Mathf.Abs(Vector2.Distance(_playerFootPosition, _platformPoint.position));
            if (_currentDistanceForWin >= totalDistance)
            {
                return;
            }
        }

        // Move the platform
        float step = direction * _speedGame * _dt;
        Vector2 newPos = Vector2.MoveTowards(_platformPoint.position, _playerFootPosition, step);
        _platformPoint.position = newPos;

        // Change the scale with the remaining distance
        float pourcent = (_platformPoint.position.y - startPosition.y) / (_playerFootPosition.y - startPosition.y);
        float newScale = initialScale + (endScale - initialScale) * pourcent;
        _platformPoint.localScale = new Vector2(newScale, newScale);

        _currentDistanceForWin = Mathf.Abs(Vector2.Distance(_playerFootPosition, _platformPoint.position));
    }

    public void AutoBascule()
    {
        // Auto rotate the player when he is not strait
        float distanceXFootHead = _playerFootPoint.transform.position.x - _playerHeatPoint.transform.position.x;
        _playerFootPoint.transform.Rotate(new Vector3(0.0f, 0.0f, distanceXFootHead * _autoBasculeStrengh * _dt));
    }

    public void ClickSideRotation() {
        // If you press the screen
        if (Input.GetMouseButton(0) && !_tools.IsPointerOverUIElement())
        {
            // click right => rotate left
            if (Screen.width / 2 < Input.mousePosition.x)
            {
                float step = _rotationSpeed * _dt;
                _playerFootPoint.transform.Rotate(new Vector3(0.0f, 0.0f, step));

            }
            // click left => rotate right
            else
            {
                float step = _rotationSpeed * _dt;
                _playerFootPoint.transform.Rotate(new Vector3(0.0f, 0.0f, -step));

            }
            return;
        }
    }

    public void CheckOverDegres()
    {
        // Check loose
        if (_loosingZRotation <= Mathf.Abs(zRotation) )
        {
            _isFalling = true;
            _timeEye = 0.0f;

            // Close the potential eye
            if (_isEyeOpen)
            {
                SwitchEye();
                _activeEye = -1;
            }
            _playerFootPoint.GetComponent<Animator>().SetBool("IsFalling", _isFalling);
        }
    }
    public void ApplyFalling()
    {
        _fallingTimeCurrent += _dt;

        // Make move the platform backward
        _speedGame = _maxSpeedGame;
        MovePlatform(-1f);

        // Reset the player on the wire
        if (_fallingTimeCurrent >= _fallingTimeDuration) 
        {
            _fallingTimeCurrent = 0.0f;
            _isFalling = false;
            _playerFootPoint.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            _playerFootPoint.GetComponent<Animator>().SetBool("IsFalling", _isFalling);
        }
    }

    public void CheckWin()
    {
        if(_currentDistanceForWin <= 0.01f) // 0.01f => distance enough to win
        {
            // Close the potential eye
            if (_isEyeOpen)
            {
                SwitchEye();
                _activeEye = -1;
            }

            isWin = true;
            _score.LauchScore();
        }
    }

    public void StartGame() 
    { 
        isStart = true;
    }
    public override void PauseMinigame()
    {
        isStart = false;
        _score.timer.stop = true;
    }

    public override void ResumeMinigame()
    {
        isStart = true;
        _score.timer.stop = false;
    }
}
