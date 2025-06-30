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
    public void DetectionBack()
    {
        gameObject.SetActive(false);
        timer.stop = false;        
    }

    public void AssociateInfo(Objects objects)
    {
        text.text = objects.str;
        if (objects.GetSprite() != null)
            img.sprite = objects.GetSprite();
    }
}
