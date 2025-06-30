using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceController : MonoBehaviour
{
    public float tiltAmount = 15f;

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (touchPosition.x > 0)
                transform.rotation = Quaternion.Euler(0, 0, -tiltAmount);
            else
                transform.rotation = Quaternion.Euler(0, 0, tiltAmount); 
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 3);
        }
    }
}
