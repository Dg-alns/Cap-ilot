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
    // List different color associate to the button
    private readonly IReadOnlyDictionary<StateButton,Color> _colorMap = new Dictionary<StateButton, Color>()
    {
        {StateButton.None ,         new Color(1,1,1,0.7f) },                // White
        {StateButton.Selected ,     new Color(0.5f,1,0.5f,0.9f) },          // Green
        {StateButton.NoSelected ,   new Color(0.56f,0.56f,0.56f,0.65f) },   // Grey
        {StateButton.Error ,        new Color(1,0.19f,0.19f,0.8f) },        // Red
    }; 

    [SerializeField] private StateButton _stateButton;
    [SerializeField] private Image _button;

    public int position;

    void Start()
    {
        _button = GetComponent<Image>();
        SwapState(StateButton.None);
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
