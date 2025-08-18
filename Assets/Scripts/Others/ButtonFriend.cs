using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFriend : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image _otherButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePanel()
    {
       
        GetComponent<Image>().color = new Color(0.1058824f, 0.3215686f, 0.3490196f); ;
        GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

        _otherButton.color = Color.white;
        _otherButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.1058824f, 0.3215686f, 0.3490196f);
    }
}
