using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSwitcher : MonoBehaviour
{
    public Transform redBox;
    public Transform blueBox;

    private Vector3 redOriginalPos;
    private Vector3 blueOriginalPos;

    private bool isSwitched = false;

    void Start()
    {
        redOriginalPos = redBox.position;
        blueOriginalPos = blueBox.position;
        StartCoroutine(SwitchBoxesRoutine());
    }

    System.Collections.IEnumerator SwitchBoxesRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(8f, 15f);
            yield return new WaitForSeconds(waitTime);
            SwitchBoxes();
        }
    }

    void SwitchBoxes()
    {
        Vector3 temp = redBox.position;
        redBox.position = blueBox.position;
        blueBox.position = temp;

        isSwitched = !isSwitched;

        redBox.tag = isSwitched ? "BlueBox" : "RedBox";
        blueBox.tag = isSwitched ? "RedBox" : "BlueBox";
    }
}
