using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TirBut_Ball : MonoBehaviour
{
    // List of shoot position 
    private readonly IReadOnlyDictionary<int, Vector2> _shootPosition = new Dictionary<int, Vector2>()
    {                                                                                                   // ---------------------------------------- //
        {0, new Vector2 (-1.66f, 2.40f)},{1, new Vector2 (0, 2.40f)},{2, new Vector2 (1.66f, 2.40f)},   // TOP_LEFT     | TOP_MID    | TOP_RIGHT    //
        {3, new Vector2 (-1.66f, 1.28f)},{4, new Vector2 (0, 1.28f)},{5, new Vector2 (1.66f, 1.28f)},   // BOTTOM_LEFT  | BOTTOM_MID | BOTTOM_RIGHT //
    };                                                                                                  // ---------------------------------------- //

    [SerializeField] private bool _shooting;

    // Default position and scale
    private Vector2 _standingPos;
    private Vector2 _standingScale;

    // Minimum scale to be saved
    private Vector2 _saveScale;

    // Scale for score
    private Vector2 _scoreScale;

    private Vector2 _targetPos;

    private float speed = 10.0f;
    private float speedDeviation = 20.0f;

    private bool _deviation = false;

    private float _time;

    GameObject _ButtonInterface;

    [SerializeField] TirBut_Score _Score;

    [SerializeField] TirBut_Diabete _Diabete;



    // Start is called before the first frame update
    void Start()
    {
        _shooting =      false;

        _standingPos =   new Vector2 (0, -6.4f);
        transform.position = _standingPos;

        _standingScale = new Vector2(2.2f, 2.2f);
        transform.localScale = _standingScale;
        _saveScale =     new Vector2(0.18f, 0.18f);
        _scoreScale =    new Vector2(0.08f, 0.08f);

        //_targetPos =     Vector2.zero;
        _targetPos = new Vector2(-1.66f, 2.4f);
        _ButtonInterface = GameObject.Find("ButtonInterface");
    }

    // Update is called once per frame
    void Update()
    {
        if (_shooting) 
        { 
            // Get a step to move on the target position
            float step = _deviation ? speedDeviation * Time.deltaTime : speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _targetPos, step);

            // Coef to calcul the scale of the ball
            float coef1 = Vector2.Distance(_standingPos, _targetPos);
            float coef2 = Vector2.Distance(transform.position, _targetPos);

            float coef = coef2 / coef1;
            transform.localScale = _deviation ? transform.localScale : coef * (_standingScale - _scoreScale) + _scoreScale;

            // if you are close enough to score wait 1.5sec to Add score
            if (coef2 < 0.01f)
            {
                _time += Time.deltaTime ;
                if(_time >= 1.5f)
                {
                    _Score.AddScore(!_deviation);
                    ResetValue();
                }
            }
        }
    }

    private void ResetValue()
    {
        // Reset a time
        _time = 0;

        // Replace the ball at the default place
        transform.localScale = _standingScale;
        transform.localPosition = _standingPos;

        _shooting = false;
        _deviation = false;

        _Diabete.ResetAnimation();

        // if it's the end, don't replace the buttons
        if (_Score.end == true )
            return;
        _ButtonInterface.SetActive(true);
    }

    public void Shoot(int indexPosition)
    {
        // Set the target to a goal position
        _targetPos = _shootPosition[indexPosition];
        _shooting = true;
    }
    public bool IsShooting() { return _shooting; }

    public bool CheckSavable()
    {
        // If the scale are corresponding with the saveScale then the ball is in a savePosition
        if (transform.localScale.x <= _saveScale.x)
        {
            _deviation = true;
            _targetPos = new Vector2(Random.Range(-5.0f,5.0f),10.0f);
            return true;
        }
        return false;
    }
}
