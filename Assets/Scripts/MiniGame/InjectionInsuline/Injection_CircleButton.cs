using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injection_CircleButton : MonoBehaviour
{
    [SerializeField] private float _scaleTarget = 2.0f;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _parent;


    //private void OnMouseDown()
    //{
    //    Debug.Log("lalalal");
    //    ClickCircle();
    //}

    public void ClickCircle()
    {

        float stopScale = _circle.GetComponent<RectTransform>().localScale.x;

        float average = stopScale - _scaleTarget;
        
        if (Mathf.Abs(average) < 0.5f)
        {
            Debug.Log("Parfait : " + (stopScale - _scaleTarget));
            Destroy(_parent);
            return;
        }
        
        if (Mathf.Abs(average) <= 3.0f)
        {
            Debug.Log("Bien : " + (stopScale - _scaleTarget));

            Destroy(_parent);
            return;
        }
        
        if (Mathf.Abs(average) > 3.0f)
        {
            Debug.Log("RATE : " + (stopScale - _scaleTarget));
            Destroy(_parent);
            return;
        }

    }
}
