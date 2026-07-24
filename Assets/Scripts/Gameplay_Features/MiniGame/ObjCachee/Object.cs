using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Objects : MonoBehaviour
{
    [SerializeField] Sprite _sprite;
    [SerializeField] string _text;
    [SerializeField] bool _canShowInfo = true;
    [SerializeField] Animator _animation;

    public Action<Objects> OnClick { get; set; }
    public Sprite Sprite { get => _sprite; }
    public string Text { get => _text; }

    public void PlayAnimation() => _animation.SetTrigger("PlayAnim");
    public void ClickedObject() { Debug.Log("feur"); OnClick.Invoke(this); }
}
