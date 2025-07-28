using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoatMer : MonoBehaviour
{
    [Header("Mer Value")]
    [SerializeField] private float _speed = 1.0f;

    private float _yMerOutGamePosition = -24.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, _yMerOutGamePosition - 1), _speed * Time.deltaTime);

        if (transform.position.y <= _yMerOutGamePosition)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (_yMerOutGamePosition * 2));
        }
    }

    public void ChangeForWinSpeed()
    {
        float winSpeedBoat = 2.0f;
        _speed -= winSpeedBoat;

    }
}
