using UnityEngine;

public class Piste : MonoBehaviour
{
    public float speed = 2f;
    public Vector3 destination;

    private void Start()
    {
        destination = new Vector3(transform.position.x, -15, 0);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

    }
}
