using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TirBut_Ball : MonoBehaviour
{
    private readonly IReadOnlyDictionary<int, Vector2> _shootPosition = new Dictionary<int, Vector2>()
    {
        {0, new Vector2 (-1.66f, 2.4f)},{1, new Vector2 (0, 2.4f)},{2, new Vector2 (1.66f, 2.4f)},
        {3, new Vector2 (-1.66f, 1.28f)},{4, new Vector2 (0, 1.28f)},{5, new Vector2 (1.66f, 1.28f)},
    };

    [SerializeField] private bool _shooting;

    private Vector2 _standingPos;

    private Vector2 _standingScale;
    private Vector2 _saveScale;
    private Vector2 _scoreScale;

    [SerializeField] private Vector2 _targetPos;

    [SerializeField] private float speed = 1.0f;

    private float _time;

    GameObject _ButtonInterface;

    [SerializeField] TirBut_Score _Score;



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
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _targetPos, step);

            float coef1 = Vector2.Distance(_standingPos, _targetPos);
            float coef2 = Vector2.Distance(transform.position, _targetPos);

            float coef = coef2 / coef1;

            transform.localScale = coef * (_standingScale - _scoreScale) + _scoreScale;
            Debug.Log(transform.localScale);
            if (coef2 < 0.01f)
            {
                _time += Time.deltaTime ;
                if(_time >= 1.5f)
                {
                    _time = 0 ;
                    transform.localScale = _standingScale;
                    transform.localPosition = _standingPos;
                    _shooting = false;
                    _ButtonInterface.SetActive(true);
                    _Score.AddScore(true);
                }
            }
        }
    }

    public void Shoot(int indexPosition)
    {
        _targetPos = _shootPosition[indexPosition];
        _shooting = true;
    }
    public bool IsShooting() { return _shooting; }
}
