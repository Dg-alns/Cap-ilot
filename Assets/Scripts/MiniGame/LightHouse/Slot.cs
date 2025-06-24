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
        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragDrop>())
        {
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = GetComponent<RectTransform>().localPosition;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
            _manager.lighthouse.Add(eventData.pointerDrag.GetComponent<DragDrop>(),slot);
            _manager.Check();
        }
    }
}
