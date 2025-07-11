using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Accueil_BGMove : MonoBehaviour
{
    RectTransform _rectTransform;
    Animator _animator;

    float speed;
    float showTime = 12.0f;
    float currentTime = 0;

    bool isMoving = false;
    bool isFading = false; 

    // Start is called before the first frame update
    void Awake()
    {

        _rectTransform = GetComponent<RectTransform>();
        Debug.Log(name + " : " + _rectTransform.position);

        _animator = GetComponent<Animator>();   
        float w = _rectTransform.rect.width;
        speed = (w - Screen.width) / showTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
            return;

        currentTime += Time.deltaTime;
        Move();

        if (currentTime > showTime)
            isMoving = false;

        if (currentTime < showTime - 2)
            return;

        if (isFading)
            return;

        Fade();
    }

    public void RestartMove()
    {
        currentTime = 0;
        _rectTransform.position = new Vector3(0,Screen.height * _rectTransform.anchorMin.y,0);
        isMoving = true;
        Show();
    }
    public void Show()
    {
        _animator.SetTrigger("GoShow");
        isFading = false;
        SetSameTriggerAnimatorOnChild("GoShow");
    }
    public void Fade()
    {
        _animator.SetTrigger("GoFade");
        isFading = true;
        SetSameTriggerAnimatorOnChild("GoFade");
    }
    void SetSameTriggerAnimatorOnChild(string trigger)
    {
        if (transform.childCount == 0)
            return;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<Animator>().SetTrigger(trigger);
    }

    public void Move()
    {
        Vector3 newPos = _rectTransform.position;
        newPos.x -= speed * Time.deltaTime;

        _rectTransform.position = newPos;
    }
}
