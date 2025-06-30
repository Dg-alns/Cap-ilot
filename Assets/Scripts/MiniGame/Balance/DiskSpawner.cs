using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
    public GameObject[] diskPrefabs; 
    public float spawnInterval = 4f;
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnDisk();
            timer = 0f;
            //if (spawnInterval > 0.5f)
                //spawnInterval -= 0.05f; 
        }
    }

    void SpawnDisk()
    {
        int i = Random.Range(0, diskPrefabs.Length);
        Instantiate(diskPrefabs[i], transform.position, Quaternion.identity);
    }
}

