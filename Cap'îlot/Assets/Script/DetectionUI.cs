using UnityEngine;
using UnityEngine.UI;

public class DetectionUI : MonoBehaviour
{
    public Image menu;
    public Image insuline;

    public GameObject diabete;


    public bool DetectionImg(Image obj)
    {
        Vector3 mouse = Input.mousePosition;

        RectTransform rect = obj.GetComponent<RectTransform>();
        float HalfHeight = (rect.rect.height * obj.transform.localScale.y) / 2f;
        float HalfWidth = (rect.rect.width * obj.transform.localScale.x) / 2f;

        bool InY = obj.transform.position.y - HalfHeight <= mouse.y && obj.transform.position.y + HalfHeight >= mouse.y;
        bool InX = obj.transform.position.x - HalfWidth <= mouse.x && obj.transform.position.x + HalfWidth >= mouse.x;

        return InY && InX;
    }

    public void DetectionMenu()
    {
        if (DetectionImg(menu))
        {
            Debug.Log("GO MENU");
        }
    }

    virtual public void DetectionInsuluine()
    {
        if (DetectionImg(insuline))
        {
            diabete.GetComponent<Diabete_Memorie>().ActiveInsuline();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectionMenu();
            DetectionInsuluine();
        }
    }

}
