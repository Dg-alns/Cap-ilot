using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
    // Start is called before the first frame update
    private Tools m_csTools;
    [SerializeField] private GameObject _UIArchives;
    void Start()
    {
        m_csTools = FindAnyObjectByType<Tools>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseOver()
    {
        if (!m_csTools.IsPointerOverUIElement())
        {
            if (Input.GetMouseButtonUp(0))
            {
                _UIArchives.SetActive(true);
            }
        }
    }
}
