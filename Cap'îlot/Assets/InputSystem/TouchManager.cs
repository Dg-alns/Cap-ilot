using UnityEngine.InputSystem;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    [SerializeField] private GameObject _player;

    private bool _isTouching = false;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _touchPositionAction = _playerInput.actions.FindAction("TouchPosition");
        _touchPressAction = _playerInput.actions.FindAction("TouchPress");
    }

    private void OnEnable()
    {
        _touchPressAction.performed += OnTouchStarted;
        _touchPressAction.canceled += OnTouchEnded;
    }

    private void OnDisable()
    {
        _touchPressAction.performed -= OnTouchStarted;
        _touchPressAction.canceled -= OnTouchEnded;
    }

    private void Update()
    {
        if (_isTouching)
        {
            Vector2 position = _touchPositionAction.ReadValue<Vector2>();
            _player.GetComponent<Movement>().Move(Camera.main.ScreenToWorldPoint(position));
        }
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        _isTouching = true;
    }

    private void OnTouchEnded(InputAction.CallbackContext context)
    {
        _isTouching = false;
    }
}
