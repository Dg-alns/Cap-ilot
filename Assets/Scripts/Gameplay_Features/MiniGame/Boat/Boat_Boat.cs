using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AnimationBoatState
{
    Idle,
    Left,
    Right,
    Win,
    Lose,
}
public class Boat_Boat : MonoBehaviour
{
    [Header("MiniGame")]

    public Boat_Game boat_Game;

    [Header("Camera and Size value")]
    private float _widthCam;
    private float _heightCam;
    private float _widthResolution;
    private float _widthSprite;

    [Header("Animation")]
    private Animator _animator;

    [Header("Boat Data")]
    [SerializeField] private float _speed = 1;
    private float _timeInvincibility = 1.0f;
    private float _currentInvincibility = -1.0f;
    private float _winAnimationTime = 2.0f;
    private float _currentWinAnimationTime = 0.0f;

    [Header("ProgressBar")]
    [SerializeField] private Boat_ProgressBar _progressBar;

    [Header("NextScene")]
    [SerializeField] private LoadNexScene _loadNexScene;
    //[SerializeField] private NextSceneDestination _destination;

    private Tools _tools;
    // Start is called before the first frame update
    void Start()
    {
        _tools = FindAnyObjectByType<Tools>();
        Camera cam = Camera.main;
        _heightCam = 2f * cam.orthographicSize;
        _widthCam = _heightCam * cam.aspect;

        _widthResolution = Screen.width;

        _widthSprite = GetComponent<SpriteRenderer>().bounds.size.x;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!boat_Game.IsGameStart()) return;

        // If you win => do nothing
        if (_animator.GetInteger("direction") == (int)AnimationBoatState.Win)
        {
            float winSpeed = _speed * 2;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), winSpeed * Time.deltaTime);
            return;
        };

        // If you are invincible
        if (_currentInvincibility >= 0.0f) 
        { 
            _currentInvincibility += Time.deltaTime;
            if(_currentInvincibility >= _timeInvincibility)
            {
                _currentInvincibility = -1.0f;
            }
            return;
        }

        // If you press the screen
        if (Input.GetMouseButton(0) && !_tools.IsPointerOverUIElement())
        {
            Debug.Log(_tools.IsPointerOverUIElement());
            // Move right
            if (_widthResolution / 2 < Input.mousePosition.x){
                float step = _speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(_widthCam / 2 - _widthSprite / 4, transform.position.y),step);

                _animator.SetInteger("direction", (int)AnimationBoatState.Right);
            }
            // Move left
            else{
                float step = _speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-_widthCam / 2 + _widthSprite / 4, transform.position.y), step);

                _animator.SetInteger("direction", (int)AnimationBoatState.Left);
            }
            return;
        }
        _animator.SetInteger("direction", (int)AnimationBoatState.Idle);
    }

    public void SetAnime(AnimationBoatState animationBoatState)
    {
        _animator.SetInteger("direction", (int)animationBoatState);
    }

    public void Winning()
    {
        _currentWinAnimationTime += Time.deltaTime;
        if(_currentWinAnimationTime > _winAnimationTime)
        {
            _loadNexScene.LoadIle();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boat_Rock")
        {
            if (_currentInvincibility != -1.0f) return;

            _progressBar.AddTime(-5.0f);
            _animator.SetInteger("direction", (int)AnimationBoatState.Lose);

            _currentInvincibility = 0.0f;
        }
    }
}
