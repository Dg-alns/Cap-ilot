using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public  class Tools : MonoBehaviour
{
    private int m_iUILayer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_iUILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private bool IsPointerOverUIElement(List<RaycastResult> lRaycastResults)
    {
        foreach (RaycastResult rsResult in lRaycastResults)
        {
            if (rsResult.gameObject.layer == m_iUILayer)
                return true;
        }
        return false;
    }

    private static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> lRaycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, lRaycastResults);
        return lRaycastResults;
    }
}
