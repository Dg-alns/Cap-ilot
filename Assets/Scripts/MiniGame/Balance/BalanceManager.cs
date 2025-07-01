using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBar : MonoBehaviour
{
    public float maxAngle = 15f;      
    public float rotationSpeed = 5f;  

    void Update()
    {
        int redCount = GameObject.FindGameObjectsWithTag("Red").Length;
        int blueCount = GameObject.FindGameObjectsWithTag("Blue").Length;

        int difference = blueCount - redCount;

        float targetAngle = Mathf.Clamp(difference * 3f, -maxAngle, maxAngle);

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
