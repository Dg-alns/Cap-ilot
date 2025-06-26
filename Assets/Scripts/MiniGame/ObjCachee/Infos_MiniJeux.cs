using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Infos_MiniJeux : MonoBehaviour
{
    public Timer timer;

    public TextMeshProUGUI text;
    public Image img;

    public Image Back;

    private void Start()
    {
        gameObject.SetActive(false);
    }


    bool DetectionImg(Image obj)
    {
        Vector3 mouse = Input.mousePosition;

        RectTransform rect = obj.GetComponent<RectTransform>();
        float HalfHeight = (rect.rect.height * obj.transform.localScale.y) / 2f;
        float HalfWidth = (rect.rect.width * obj.transform.localScale.x) / 2f;

        bool InY = obj.transform.position.y - HalfHeight <= mouse.y && obj.transform.position.y + HalfHeight >= mouse.y;
        bool InX = obj.transform.position.x - HalfWidth <= mouse.x && obj.transform.position.x + HalfWidth >= mouse.x;

        return InY && InX;
    }
    void DetectionBack()
    {
        if (DetectionImg(Back))
        {
            gameObject.SetActive(false);


            timer.stop = false;
        }
    }

    public void AssociateInfo(Objects objects)
    {
        text.text = objects.str;
        if (objects.GetSprite() != null)
            img.sprite = objects.GetSprite();
    }

    void Update()
    {
        if(gameObject.activeSelf ==false)
            { return; }

        if (Input.GetMouseButtonDown(0))
        {
            DetectionBack();
        }
    }
}
