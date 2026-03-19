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
    [SerializeField] private GameObject _futurParent;
    private CanvasGroup _cg;

    [SerializeField] private GameObject _slots;

    [SerializeField] private LighthouseGameManager _manager;

    private List<RectTransform> _slotsList = new List<RectTransform>();

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _cg = GetComponent<CanvasGroup>();
        for (int i = 0; i < _slots.GetComponentsInChildren<RectTransform>().Length; i++) {
            _slotsList.Add(_slots.GetComponentsInChildren<RectTransform>()[i]);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _cg.blocksRaycasts = false;
        _rt.SetParent(_futurParent.transform);
        //_rt.SetAsLastSibling();

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

        _rt.sizeDelta = _slotsList[_slotsList.Count-floor-1].sizeDelta;        
        /*{

            case 0:
                _rt.sizeDelta = new Vector2(165.1635f, 138.6663f);
                break;
            case 1:
                _rt.sizeDelta = new Vector2(151.5106f, 178.0784f);
                break;
        }*/

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
        _rt.sizeDelta = new Vector2(100,100);           

        //_rt.SetAsLastSibling();
    }

   public void OnPointerDown(PointerEventData eventData)
    {
    }
}
