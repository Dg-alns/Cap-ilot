using UnityEngine;
using UnityEngine.UI;

public class DetectionUI : MonoBehaviour
{
    public Image menu;

    public GameObject diabete;

    protected Tools _tools;


    private void Awake()
    {

        _tools = FindAnyObjectByType<Tools>();
    }

    public void DetectionMenu()
    {
        Debug.Log("GO MENU");
        
    }

}
