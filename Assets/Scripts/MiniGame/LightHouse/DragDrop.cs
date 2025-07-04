using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int floor;

    private RectTransform _rt;
    [SerializeField] private Canvas _canva;
    private CanvasGroup _cg;

    [SerializeField] private LighthouseGameManager _manager;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _cg = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _cg.blocksRaycasts = false;
        _rt.SetParent(_canva.transform);

        foreach (var item in _manager.lighthouse.Keys.ToList())
        {
            if(item == this)
            {
                _manager.lighthouse.Remove(item);
            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        _rt.anchoredPosition += eventData.delta/ _canva.scaleFactor;


        switch (floor)
        {

            case 0:
                _rt.sizeDelta = new Vector2(165.1635f, 138.6663f);
                break;
            case 1:
                _rt.sizeDelta = new Vector2(151.5106f, 178.0784f);
                break;
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _cg.blocksRaycasts = true;

        foreach (var item in _manager.lighthouse.Keys.ToList())
        {
            if (item == this)
            {
                return;
            }
        }
        _rt.SetParent(_canva.GetComponentInChildren<HorizontalLayoutGroup>().transform);
    }

   public void OnPointerDown(PointerEventData eventData)
    {
    }
}
