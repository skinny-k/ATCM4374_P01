using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    private PlayerInput _playerInput;

    private InputAction _touchLongAction;
    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _touchLongAction = _playerInput.actions["Touch Press Long"];
        _touchPositionAction = _playerInput.actions["Touch Position"];
        _touchPressAction = _playerInput.actions["Touch Press"];
    }

    void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        
        _touchPressAction.started += TouchPressed;
        _touchLongAction.performed += TouchLongPressed;
        EnhancedTouch.Touch.onFingerMove += FingerMove;
        EnhancedTouch.Touch.onFingerUp += FingerUp;
    }

    void OnDisable()
    {
        _touchPressAction.started -= TouchPressed;
        _touchLongAction.performed -= TouchLongPressed;
        EnhancedTouch.Touch.onFingerMove -= FingerMove;
        EnhancedTouch.Touch.onFingerUp -= FingerUp;

        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(_touchPositionAction.ReadValue<Vector2>());
        worldPos.z = _player.transform.position.z;

        SpriteRenderer sprite = _player.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }

        _player.transform.position = worldPos;
    }

    private void TouchLongPressed(InputAction.CallbackContext context)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(_touchPositionAction.ReadValue<Vector2>());
        worldPos.z = _player.transform.position.z;

        SpriteRenderer sprite = _player.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.red;
        }
        
        _player.transform.position = worldPos;
    }

    private void FingerMove(EnhancedTouch.Finger finger)
    {
        _touchLongAction.Disable();
            
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(_touchPositionAction.ReadValue<Vector2>());
        worldPos.z = _player.transform.position.z;

        SpriteRenderer sprite = _player.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.green;
        }
        _player.transform.position = worldPos;
    }

    private void FingerUp(EnhancedTouch.Finger finger)
    {
        _touchLongAction.Enable();
    }
}
