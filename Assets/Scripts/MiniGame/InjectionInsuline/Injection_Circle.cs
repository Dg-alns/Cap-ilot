using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injection_Circle : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * _speed;
        transform.localScale = new Vector3(transform.localScale.x - step, transform.localScale.y - step, transform.localScale.z);
        if (transform.localScale.x <= 0f)
        {
            Debug.Log("Raté Noob");
            Destroy(_parent);
        }
    }
}
