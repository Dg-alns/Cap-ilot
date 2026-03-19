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

    public void SetActiveButton(TirBut_Button button)
    {
        // If the button is already Selected then put all of them in disable
        if (button.GetStateButton() == StateButton.Selected)
        {
            SetDisableButton();
            return;
        }

        // Put every buttons to NoSelected then the button select to Selected  
        foreach(TirBut_Button b  in _buttons)
        {
            b.SwapState(StateButton.NoSelected);
        }
        _activeButton = button;
        _activeButton.ActiveButton();
    }

    // Change state buttons to None (Grey Color)
    public void SetDisableButton()
    {
        foreach (TirBut_Button b in _buttons)
        {
            b.SwapState(StateButton.None);
        }
        _activeButton = null;
    }

    // Change state buttons to Error (Red Color)
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
