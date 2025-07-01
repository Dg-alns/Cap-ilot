using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSwitcher : MonoBehaviour
{
    public Transform redBox;
    public Transform blueBox;
    public Transform delete;

    private Vector3 redOriginalPos;
    private Vector3 blueOriginalPos;
    private Vector3 deleteOriginalPos;

    private bool isSwitched = false;

    void Start()
    {
        redOriginalPos = redBox.position;
        blueOriginalPos = blueBox.position;
        deleteOriginalPos = delete.position;
        StartCoroutine(SwitchBoxesRoutine());
    }

    System.Collections.IEnumerator SwitchBoxesRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(12f, 16f);
            yield return new WaitForSeconds(waitTime);
            SwitchBoxes();
        }
    }

    void SwitchBoxes()
    {
        List<Vector3> positions = new List<Vector3> { redOriginalPos, blueOriginalPos, deleteOriginalPos };

        // On mélange ces positions
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 temp = positions[i];
            int randIndex = Random.Range(i, positions.Count);
            positions[i] = positions[randIndex];
            positions[randIndex] = temp;
        }

        redBox.position = positions[0];
        blueBox.position = positions[1];
        delete.position = positions[2];

        Debug.Log("Boxes switched !");
    }

}
