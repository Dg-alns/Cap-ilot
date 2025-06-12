using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TouchManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _dialogueBox;

    public Vector2 position;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _touchPositionAction = _playerInput.actions.FindAction("TouchPosition");
        _touchPressAction = _playerInput.actions.FindAction("TouchPress");
    }

    private void OnEnable()
    {
        _touchPositionAction.performed += TouchPosition;
        _touchPressAction.performed += TouchPress;
    }

    private void OnDisable()
    {
        _touchPressAction.performed -= TouchPress;
        _touchPositionAction.performed -= TouchPosition;
    }

    private void TouchPress(InputAction.CallbackContext context)
    {
        if (_dialogueBox.GetComponentInChildren<DialogueBox>().dialogStarted)
        {
            _dialogueBox.GetComponentInChildren<DialogueBox>().GoToNextDialogueLine();
        }
    }

    private void TouchPosition(InputAction.CallbackContext context) {
       position = context.ReadValue<Vector2>();
       _player.GetComponent<Movement>().Move(Camera.main.ScreenToWorldPoint(position));
       //Debug.Log(Camera.main.ScreenToWorldPoint(position));
    }
}
