using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boat_SimpleRock : MonoBehaviour
{
    protected float _heightCam;
    protected float _widthCam;

    public SpriteRenderer rockSprite;
    protected float _heightSprite;
    protected float _widthSprite;

    [SerializeField] private float _speed = 1.0f;
     
    public bool isPause = false;

    private void Awake()
    {
        Camera cam = Camera.main;
        _heightCam = 2f * cam.orthographicSize;
        _widthCam = _heightCam * cam.aspect;

        _heightSprite = rockSprite.bounds.size.y;
        _widthSprite = rockSprite.bounds.size.x;

        transform.position = new Vector2(Random.Range(_widthCam / 2 - _widthSprite / 2, -_widthCam / 2 + _widthSprite / 2), transform.position.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (isPause)
            return;

        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(transform.position.x, -_heightCam/2 - _heightSprite/2),step);

        float distance = Vector2.Distance(transform.position, new Vector2(transform.position.x, -_heightCam / 2 - _heightSprite / 2));

        if (distance < 0.01f) { 
            Destroy(gameObject);
        }
    }
}
