using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMemorie : MonoBehaviour
{
    public ParticleSystem[] mParticleSystem;

    private void Start()
    {
        mParticleSystem = GetComponentsInChildren<ParticleSystem>();
        Debug.Log(mParticleSystem.Length);
    }

    public void PlayParticle(List<GameObject> gameObject)
    {
        for(int i = 0; i< mParticleSystem.Length;  i++)
        {
            Debug.Log(gameObject[i].GetComponent<Transform>().position);
            mParticleSystem[i].gameObject.GetComponent<Transform>().position = gameObject[i].GetComponent<Transform>().position;
            mParticleSystem[i].Play();
        }
    }
}
