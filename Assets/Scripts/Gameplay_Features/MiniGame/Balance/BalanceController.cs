using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalanceController : MonoBehaviour
{
    public float tiltAmount = 15f;
    public float speed = 1f;


    public bool pause = false; 

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (pause)
            return;

        if (Input.GetMouseButton(0)) 
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (touchPosition.x > 0)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -tiltAmount), speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, -tiltAmount);
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, tiltAmount), speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, tiltAmount); 
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 3);
        }
    }
}
