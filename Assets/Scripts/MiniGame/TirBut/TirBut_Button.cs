using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum StateButton
{
    None,
    Selected,
    NoSelected,
    Error,

}
public class TirBut_Button : MonoBehaviour
{
    private readonly IReadOnlyDictionary<StateButton,Color> _colorMap = new Dictionary<StateButton, Color>()
    {
        {StateButton.None , new Color(1,1,1,0.7f) },
        {StateButton.Selected , new Color(0.5f,1,0.5f,0.9f) },
        {StateButton.NoSelected , new Color(0.56f,0.56f,0.56f,0.65f) },
        {StateButton.Error , new Color(1,0.19f,0.19f,0.8f) },
    }; 

    [SerializeField] private StateButton _stateButton;
    [SerializeField] private Image _button;

    public int position;

    void Start()
    {
        _button = GetComponent<Image>();
        SwapState(StateButton.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapState(StateButton newState)
    {
        _stateButton = newState;

        _button.color = _colorMap[newState];
    }

    public void ActiveButton()
    {
        SwapState(StateButton.Selected);
    }

    public StateButton GetStateButton()
    {
        return _stateButton;
    }
}
