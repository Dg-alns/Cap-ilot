using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funambule : MonoBehaviour
{

    [SerializeField] private Transform _playerPoint;
    [SerializeField] private Transform _platformPoint;

    Vector2 startPosition;
    float initialScale = 1.0f;
    float endScale = 2.1f;

    private float _speedGame;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = _platformPoint.transform.position;
        _speedGame = (Mathf.Abs(_playerPoint.position.y) + Mathf.Abs(_platformPoint.position.y)) / 30.0f;
        Debug.Log("Speed : " + _speedGame);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    public void MovePlatform()
    {
        float step = _speedGame * Time.deltaTime;
        Vector2 newPos = Vector2.MoveTowards(_platformPoint.position, _playerPoint.position, step);
        _platformPoint.position = newPos;

        float pourcent = (_platformPoint.position.y - startPosition.y) / (_playerPoint.position.y - startPosition.y);

        float newScale = initialScale + (endScale - initialScale) * pourcent;

        _platformPoint.localScale = new Vector2(newScale, newScale);
        Debug.Log(pourcent + "%");
    }
}
