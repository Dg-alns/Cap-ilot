using System.Collections.Generic;
using UnityEngine;


public enum TypeBLock
{
    Piste,
    Ravito,
    Arrivée
}

public class PisteManagement : MonoBehaviour
{
    [Header ("Prefab Block")]
    public GameObject RefPiste;
    public GameObject RefRavitoLeft;
    public GameObject RefRavitoRight;
    public GameObject RefArrivée;

    public GameObject player;
    public Score score;


    public Camera cam;

    GameObject PisteCreated;
    GameObject LastPisteCreated;

    public List<GameObject> CurrentPist;

    public int nbPisteBlock;

    public float baseSpeed;
    public float currentSpeed;   
    
    public bool Finish = false;

    public List<TypeBLock> patternPiste = new List<TypeBLock>();
    int idx = 0;
    int NbBlockDontShow;

    public bool isPause = false;

    private void Awake()
    {
        LastPisteCreated = CurrentPist[0];
        PisteCreated = CurrentPist[1];
        baseSpeed = PisteCreated.GetComponent<Piste>().speed;
        currentSpeed = baseSpeed;

        CreationPattern();
    }

    private void Update()
    {
        if (isPause)
            return;

        if (Finish)
        {
            if (player.transform.position != PisteCreated.GetComponent<Arrivée>().GetFinalPos())
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, PisteCreated.GetComponent<Arrivée>().GetFinalPos(), 2 * Time.deltaTime);
            }
            else
            {
                score.LauchScore();
            }
            

            return;
        }
        
        if(PisteCreated.name.Contains("Arrivée"))
        {
            if (PisteCreated.GetComponent<Arrivée>().ToucheTheLine())
            {
                StopMovementPist();
                Finish = true;
                return;
            }
        }

        if (NbBlockDontShow != 0)
        {
            CreatePiste();

            if (PisteCreated != null && PisteCreated != CurrentPist[1])
            {
                if (PisteCreated.GetComponent<Piste>().destination == PisteCreated.transform.position)
                {
                    PisteCreated.GetComponent<Piste>().StartDestination();
                    idx++;
                    NbBlockDontShow--;
                }


                DestroyPiste();
            }
        }


        
    }

    GameObject InitNewPiste(GameObject reference)
    {
        GameObject newPist = Instantiate(reference);

        newPist.transform.position = PisteCreated.transform.position;
        newPist.transform.localScale = PisteCreated.transform.localScale;
        newPist.transform.rotation = PisteCreated.transform.rotation;


        newPist.transform.position = new Vector3(newPist.transform.position.x, newPist.transform.position.y + newPist.transform.position.y * 2, newPist.transform.position.z);
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


        newPist.transform.position = new Vector3(newPist.transform.position.x, newPist.transform.position.y + newPist.transform.position.y * 2, newPist.transform.position.z);

        newPist.GetComponent<Piste>().speed = currentSpeed;

        CurrentPist.Add(newPist);
        return newPist;
    }

    GameObject CreateNewBlock()
    {
        switch (patternPiste[idx])
        {
            case TypeBLock.Piste:
                return InitNewPiste(RefPiste);

            case TypeBLock.Ravito:
                return InitNewRavito();

            case TypeBLock.Arrivée:
                return InitNewPiste(RefArrivée);
        }

        return null;
    }

    void DestroyPiste()
    {
        if (LastPisteCreated == null)
            return;           

        if (LastPisteCreated.transform.position == LastPisteCreated.GetComponent<Piste>().destination)
        {
            CurrentPist.Remove(LastPisteCreated);
            Destroy(LastPisteCreated);

            if (CurrentPist.Count > 0)
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

    public void StopMovementPist()
    {
        foreach (GameObject item in CurrentPist)
        {
            item.GetComponent<Piste>().RestDestination();
        }
    }

    public void StartMovementPist()
    {
        foreach (GameObject item in CurrentPist)
        {
            item.GetComponent<Piste>().StartDestination();
        }
    }


    void CreationPattern()
    {
        int i = 0;
        while (i < nbPisteBlock)
        {
            int rangePist = Random.Range(2, 5);

            if (nbPisteBlock - i <= 6)
            {
                for (int j = 0; j < rangePist; j++)
                {
                    patternPiste.Add(TypeBLock.Piste);
                    i++;
                }
                
                patternPiste.Add(TypeBLock.Ravito);

                for (int k = 0; k < nbPisteBlock - i; k++)
                {
                    patternPiste.Add(TypeBLock.Piste);
                }
                break;
            }

            while (rangePist > 0)
            {
                patternPiste.Add(TypeBLock.Piste);
                i++;
                rangePist--;
            }

            patternPiste.Add(TypeBLock.Ravito);

        }

        patternPiste.Add(TypeBLock.Arrivée);

        NbBlockDontShow = patternPiste.Count;
    }
    public void Pause()
    {
        isPause = true;
        foreach (GameObject piste in CurrentPist)
        {
            piste.GetComponent<Piste>().isPause = true;
        }
    }
    public void Resume()
    {
        isPause = false;
        foreach (GameObject piste in CurrentPist)
        {
            piste.GetComponent<Piste>().isPause = false;
        }
    }
}