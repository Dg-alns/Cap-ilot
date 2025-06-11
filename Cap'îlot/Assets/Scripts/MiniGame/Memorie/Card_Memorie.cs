using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Card_Memorie : MonoBehaviour
{
    public string symbol = "";
    private Animator animator;

    private bool isActive;
    public Minigame_Memorie mMemorie;

    private float speedSwitch;
    public bool switching = false;
    private Vector2 positionTarget;
    private float distanceEnough;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isActive = false;

        speedSwitch = 0.0f;
        distanceEnough = 0.0f;

        GenerateRandomPosition();
    }

    private void Update()
    {
        // Move Card when it's the chosen one
        if (switching)
        {
            float step = Time.deltaTime * speedSwitch;
            transform.position = Vector2.MoveTowards(transform.position, positionTarget, step);
            float distance = Vector2.Distance(transform.position, positionTarget);

            // Check the Distance to stop it
            if (distance < distanceEnough) {
                switching = false;
            }
        }
    }
    private void GenerateRandomPosition()
    {
        // Take a random number in a list
        int r = Random.Range(0, mMemorie.slots.Count);
        int associateNum = mMemorie.slots[r];

        // Remove the number to the list
        mMemorie.slots.RemoveAt(r);

        // Associate number with Col and Row
        int numcol = associateNum % 4;
        int numrow = associateNum / 4;

        // Assign Position
        Transform t = gameObject.GetComponent<Transform>();
        t.localPosition = new Vector2(numrow - 1.5f, numcol);
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

    public void SwitchCard(Vector2 _positionTarget)
    {
        Debug.Log(gameObject.name + " are switching");
        positionTarget = _positionTarget;
        switching = true;
        speedSwitch = Vector2.Distance(transform.position, _positionTarget) / 1.5f;
    }
}