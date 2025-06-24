using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionEndurance : MonoBehaviour
{
    Image endurance;

    public bool HaveNoneEndurance = false;
    bool recupEndurance = false;
    public bool tackeRavito = false;
    public bool restartEndurance = false;

    float BaseEndurance;

    float TargetTime = 1;
    float CurrentTime;

    void Start()
    {
        endurance = GetComponent<Image>();
        BaseEndurance = endurance.fillAmount;
    }


    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= TargetTime)
        {
            CurrentTime = 0; 
            recupEndurance = true;
            StartCoroutine(RecupEndurance());
        }
        


        if(HaveNoneEndurance)
        {
            endurance.fillAmount += 1.0f / 5f * Time.deltaTime;

            if (endurance.fillAmount >= 1)
            {
                endurance.fillAmount = 1;
                HaveNoneEndurance = false;
                restartEndurance = true;
            }
        }
    }

    public void UseEndurance() {
        CurrentTime = 0;
        endurance.fillAmount -= (BaseEndurance * 0.05f);

        if(endurance.fillAmount <= 0)
        {
            endurance.fillAmount = 0;
            HaveNoneEndurance = true;
        }
    }

    IEnumerator RecupEndurance() 
    {
        if (recupEndurance == false)
            yield return null;

        endurance.fillAmount += (BaseEndurance * 0.02f);

        if (endurance.fillAmount > 1)
        {
            endurance.fillAmount = 1;
            yield return null;
        }
    }

    public void Ravitallement()
    {
        tackeRavito = true;
        endurance.fillAmount += (BaseEndurance * 0.5f);

        if (endurance.fillAmount >= 1)
        {
            endurance.fillAmount = 1;
            HaveNoneEndurance = false;
        }
    }
}
