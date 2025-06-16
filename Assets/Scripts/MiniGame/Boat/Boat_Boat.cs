using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private float _width;
    private float _widthResolution;

    private float _widthSprite;

    [SerializeField] private float _speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        _width = height * cam.aspect;

        _widthResolution = Screen.width;

        _widthSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_widthResolution / 2 < Input.mousePosition.x){
                float step = _speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(_width/2 - _widthSprite/2, transform.position.y),step);
            }
            else{
                float step = _speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-_width / 2 + _widthSprite/2, transform.position.y), step);
            }
        }
    }
}
