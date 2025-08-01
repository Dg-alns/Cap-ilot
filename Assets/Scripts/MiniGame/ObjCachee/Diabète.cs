using System.Collections;
using UnityEngine;

public class Diabète : MonoBehaviour
{
    public Camera cam;
    public Timer timer;

    Animator animator;

    float HalfHeight;
    float HalfWidth;

    float minX;
    float maxX;

    float minY;
    float maxY;

    public float Basespeed = 5f;
    public float Currentspeed = 0f;

    Vector3 posDb;
    Vector3 destination;

    public bool clikme = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        HalfHeight = GetComponent<Renderer>().bounds.size.y / 2f;
        HalfWidth = GetComponent<Renderer>().bounds.size.x / 2f;

        posDb = transform.position;
        destination = posDb;

        timer.SetNSeconds(3);

        float height = cam.orthographicSize;
        float width = cam.aspect * height;

        float minXCam = cam.transform.position.x - width;
        float minYCam = cam.transform.position.y - height;

        float maxXCam = cam.transform.position.x + width;
        float maxYCam = cam.transform.position.y + height;

        minX = minXCam + HalfWidth;
        maxX = maxXCam - HalfWidth;

        minY = minYCam + HalfHeight;
        maxY = maxYCam - HalfHeight;
    }

    bool PointIsInMe(Vector3 point)
    {
        bool InY = point.y >= posDb.y - HalfHeight && point.y <= posDb.y + HalfHeight;
        bool InX = point.x >= posDb.x - HalfWidth && point.x <= posDb.x + HalfWidth;

        if( InY && InX)
            return true;


        return false;
    }

    Vector3 ChoosePosition()
    {
        Vector3 pos = new Vector3();
        pos.z = transform.position.z;

        do
        {
            pos.x = Random.Range(minX, maxX);
            pos.y = Random.Range(minY, maxY);
        }
        while (PointIsInMe(pos));

        return pos;
    }

    void NewPosition()
    {
        if (timer.ElapseNsecond())
        {
            destination = ChoosePosition();
            timer.RestartNSeconds();

        }
    }

    public void GoToPosition()
    {
        if (destination == transform.position)
        {
            NewPosition();
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, Currentspeed * Time.deltaTime);
    }

    private void Update()
    {
        GoToPosition();
    }
}
