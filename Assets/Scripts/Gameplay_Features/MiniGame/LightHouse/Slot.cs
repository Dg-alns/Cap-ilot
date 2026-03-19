using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] private LighthouseGameManager _manager;
    public int slot;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop");
        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragDrop>() && GetComponentsInChildren<RectTransform>().Length <= 1)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
            _manager.lighthouse.Add(eventData.pointerDrag.GetComponent<DragDrop>(),slot);
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(transform);
            _manager.Check();
        }
    }
}
