using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string symbol = "";
    private Animator animator;

    private bool isActive;
    public Memorie mMemorie;


    private void Start()
    {
        animator = GetComponent<Animator>();
        isActive = false;

        GenerateRandomPosition();
    }

    private void GenerateRandomPosition()
    {
        // Take a random number in a list
        int r = Random.Range(0, mMemorie.nbSlotsAvailable);
        int associateNum = mMemorie.slots[r];

        // Remove the number to the list
        mMemorie.nbSlotsAvailable -= 1;
        mMemorie.slots.RemoveAt(r);

        // Associate number with Col and Row
        int numcol = associateNum % 4;
        int numrow = associateNum / 4;

        // Assign Position
        Transform t = gameObject.GetComponent<Transform>();
        t.localPosition = new Vector3(numrow - 1.5f, numcol, 0);
    }
    void OnMouseUp()
    {
        if (!isActive)
        {
            mMemorie.AddShowingList(this.gameObject);
        }
    }
    public void ReverseCard()
    {
        isActive = !isActive;
        animator.SetBool("IsClicked", isActive);
    }
}
