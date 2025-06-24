using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TirBut_ButtonManager : MonoBehaviour
{
    [SerializeField] private List<TirBut_Button> _buttons;
    [SerializeField] private TirBut_Button _activeButton;


    void Start()
    {
        TirBut_Button[] tmpButton = GetComponentsInChildren<TirBut_Button>();
        _buttons = new List<TirBut_Button>(tmpButton);
        _activeButton = null;
    }

    void Update()
    {
        
    }

    public void SetActiveButton(TirBut_Button button)
    {
        if (button.GetStateButton() == StateButton.Selected)
        {
            DisableButton();
            return;
        }
        foreach(TirBut_Button b  in _buttons)
        {
            b.SwapState(StateButton.NoSelected);
        }
        _activeButton = button;
        _activeButton.ActiveButton();
    }
    public void DisableButton()
    {
        foreach (TirBut_Button b in _buttons)
        {
            b.SwapState(StateButton.None);
        }
        _activeButton = null;
    }
    public void SetErrorButtons()
    {
        foreach (TirBut_Button b in _buttons)
        {
            b.SwapState(StateButton.Error);
        }
    }
    public TirBut_Button GetActiveButton()
    {
        return _activeButton;
    }
}
