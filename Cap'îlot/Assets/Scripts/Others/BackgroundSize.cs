using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSize : MonoBehaviour
{
    void Awake()
    {
        ResizeBackground();
    }

    void ResizeBackground()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;

        Vector3 newScale = transform.localScale;
        newScale.x = worldScreenWidth / width;
        newScale.y = worldScreenHeight / height;
        transform.localScale = newScale;
    }

}

