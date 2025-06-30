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
    private float _widthCam;
    private float _heightCam;

    private float _widthResolution;

    private float _widthSprite;

    private Animator _animator;

    private float _timeInvincibility = 1.0f;
    private float _currentInvincibility = -1.0f;

    [SerializeField] private float _speed = 1;
    [SerializeField] private NextSceneDestination _destination;
    [SerializeField] private Boat_ProgressBar _progressBar;

    // Start is called before the first frame update
    void Start()
    {
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
        // If you win => do nothing
        if (_animator.GetInteger("direction") == (int)AnimationBoatState.Win) return;

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
        if (Input.GetMouseButton(0))
        {
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

    public void WinningForward()
    {
        float step = _speed * Time.deltaTime * 2;
        Vector2 target = new Vector2(transform.position.x, _heightCam / 1.8f);

        transform.position = Vector2.MoveTowards(transform.position, target, step);
        if(Vector2.Distance(transform.position, target) < 0.01f)
        {
            _destination.LoadNewIle();
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
