using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public SpriteRenderer test;
    public GameObject target;

    void Update()
    {
        float cameraHeight = Camera.main.orthographicSize;

        float cameraWidth = cameraHeight * Camera.main.aspect +0.1f;
        transform.position = new Vector3(
        Mathf.Clamp(target.transform.position.x, test.bounds.min.x + cameraWidth, test.bounds.max.x - cameraWidth),
        Mathf.Clamp(target.transform.position.y, test.bounds.min.y + cameraHeight, test.bounds.max.y - cameraHeight),
        transform.position.z);
    }
}
