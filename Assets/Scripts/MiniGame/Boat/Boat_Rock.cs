using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Rock : MonoBehaviour
{
    private float _heightCam;
    private float _widthCam;

    private float _heightSprite;

    [SerializeField] private float _speed = 1.0f;

    private void Start()
    {
        Camera cam = Camera.main;
        _heightCam = 2f * cam.orthographicSize;
        _widthCam = _heightCam * cam.aspect;

        _heightSprite = GetComponent<SpriteRenderer>().bounds.size.y;
        float _widthSprite = GetComponent<SpriteRenderer>().bounds.size.x;

        transform.position = new Vector2(Random.Range(_widthCam / 2 - _widthSprite / 2, -_widthCam / 2 + _widthSprite / 2), transform.position.y);
    }
    // Update is called once per frame
    void Update()
    {
        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(transform.position.x, -_heightCam/2 - _heightSprite/2),step);

        float distance = Vector2.Distance(transform.position, new Vector2(transform.position.x, -_heightCam / 2 - _heightSprite / 2));

        if (distance < 0.01f) { 
            Destroy(gameObject);
        }
    }
}
