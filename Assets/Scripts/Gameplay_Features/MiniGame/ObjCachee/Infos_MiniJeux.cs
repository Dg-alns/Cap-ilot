using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Infos_MiniJeux : MonoBehaviour
{
    [SerializeField] GameObject _base;
    [SerializeField] Timer _timer;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Image _img;
    [SerializeField] Animator _panelAnimation;

    Action _disAppearEvent = null;

    public GameObject Base { get => _base; }


    private void Start()
    {
        _base.SetActive(false);
    }

    void OnEnable()
    {
        _panelAnimation.SetTrigger("Appear");
    }

    public void Disable()
    {
        _panelAnimation.SetTrigger("Disappear");
        _disAppearEvent = DetectionBack;
    }

    public void EndDisable() => _disAppearEvent?.Invoke();

    public void DetectionBack()
    {
        _base.SetActive(false);
        _timer.stop = false;
        _disAppearEvent = null;
    }

    public void AssociateInfo(Objects objects)
    {
        _text.text = objects.Text;
        if (objects.Sprite != null)
            _img.sprite = objects.Sprite;
    }
}
