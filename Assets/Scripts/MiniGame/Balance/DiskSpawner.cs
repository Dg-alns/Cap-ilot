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
        int i = Random.Range(0, 2);
        if (i == 0)
            Instantiate(diskPrefabs[0], transform.position, Quaternion.identity);
        else
        {
            /*faire la liste des ingredients random à avoir pour réussir le niveau, et faire des randoms
            *de cette liste en supprimant les elements séléctonner de sorte à avoir tous les ingredients
            de la liste qui sorte dans un ordre random en premier avant d'avoir un réel aléatoire et laisser
            la chance nous sauver ou non de nos echecs sur les ingredient deja passé*/
            int j = Random.Range(1, diskPrefabs.Length);
            Instantiate(diskPrefabs[j], transform.position, Quaternion.identity);
        }
       
        
    }
}

