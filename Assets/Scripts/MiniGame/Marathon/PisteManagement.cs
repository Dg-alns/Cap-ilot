using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisteManagement : MonoBehaviour
{
    public GameObject RefPiste;
    public GameObject RefRavitoLeft;
    public GameObject RefRavitoRight;


    public Camera cam;

    public GameObject PisteCreated;
    GameObject LastPisteCreated;

    public List<GameObject> CurrentPist;

    public int nbPisteBlock = 10;

    public float baseSpeed;
    public float currentSpeed;


    GameObject InitNewPist()
    {
        GameObject newPist = Instantiate(RefPiste);

        newPist.transform.position = PisteCreated.transform.position;
        newPist.transform.localScale = PisteCreated.transform.localScale;
        newPist.transform.rotation = PisteCreated.transform.rotation;


        newPist.transform.position = new Vector3(newPist.transform.position.x, newPist.transform.position.y + newPist.transform.position.y * 2 , newPist.transform.position.z);
        newPist.GetComponent<Piste>().speed = currentSpeed;
        CurrentPist.Add(newPist);
        return newPist;
    }

    GameObject InitNewRavito()
    {
        int nb = Random.Range(0, 2);
        GameObject newPist = null;

        switch (nb)
        {
            case 0:
                newPist = Instantiate(RefRavitoLeft);
                break;
            case 1:
                newPist = Instantiate(RefRavitoRight);
                break;
        }

        newPist.transform.position = PisteCreated.transform.position;
        newPist.transform.localScale = PisteCreated.transform.localScale;
        newPist.transform.rotation = PisteCreated.transform.rotation;


        newPist.transform.position = new Vector3(newPist.transform.position.x, newPist.transform.position.y + newPist.transform.position.y * 2 , newPist.transform.position.z);

        newPist.GetComponent<Piste>().speed = currentSpeed;

        CurrentPist.Add(newPist);
        return newPist;
    }

    GameObject CreateNewBlock()
    {
        int nb = Random.Range(1, 10);

        if (nb <= 7)
        {
            nbPisteBlock--;
            return InitNewPist();
        }

        return InitNewRavito();
    }

    void DestroyPiste()
    {

        if (LastPisteCreated.transform.position == LastPisteCreated.GetComponent<Piste>().destination)
        {
            CurrentPist.Remove(LastPisteCreated);
            Destroy(LastPisteCreated);
            LastPisteCreated = CurrentPist[0];
        }
    }



    void CreatePiste()
    {
        if (PisteCreated.transform.position.y <= cam.orthographicSize)
        {
            PisteCreated = CreateNewBlock();
        }
    }

    public void UpdateSpeed(float speed)
    {
        foreach (GameObject item in CurrentPist)
        {
            item.GetComponent<Piste>().speed = speed;
        }
    }


    private void Awake()
    {
        LastPisteCreated = CurrentPist[0];
        baseSpeed = PisteCreated.GetComponent<Piste>().speed;
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        DestroyPiste();

        if (nbPisteBlock > 0)
            CreatePiste();
    }

}