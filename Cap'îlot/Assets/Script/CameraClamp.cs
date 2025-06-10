using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer test;
    public GameObject target;
    void Start()
    {
        
        Debug.Log(GetComponent<Camera>().orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
      Mathf.Clamp(target.transform.position.x, 0, 2),
      Mathf.Clamp(target.transform.position.y, 0, 2),
      transform.position.z);
    }
}
