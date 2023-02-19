using System;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    [SerializeField] GameObject _debugInput;
    [SerializeField] GameDie _testDie;
    
    PlayerInput _playerInput;

    InputAction _touchLongAction;
    InputAction _touchPressAction;
    InputAction _touchPosition;

    public static event Action<Vector2> OnTouchPress;
    public static event Action<Vector2> OnTouchLongPress;
    public static event Action<Vector2> OnFingerMove;
    public static event Action OnFingerUp;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _touchLongAction = _playerInput.actions["Touch Press Long"];
        _touchPressAction = _playerInput.actions["Touch Press"];
        _touchPosition = _playerInput.actions["Touch Position"];
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

    void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 touchPos = _touchPosition.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);
        OnTouchPress?.Invoke(touchPos);
        // Ray pickup = Camera.main.ScreenPointToRay(touchPos);

        // UpdateDebug(worldPos, Color.white);
    }

    void TouchLongPressed(InputAction.CallbackContext context)
    {
        Vector2 touchPos = _touchPosition.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);
        OnTouchLongPress?.Invoke(touchPos);
        
        // UpdateDebug(worldPos, Color.red);
    }

    void FingerMove(EnhancedTouch.Finger finger)
    {
        _touchLongAction.Disable();
            
        Vector2 touchPos = _touchPosition.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);
        OnFingerMove?.Invoke(touchPos);
        
        // UpdateDebug(worldPos, Color.green);
    }

    void FingerUp(EnhancedTouch.Finger finger)
    {
        _touchLongAction.Enable();

        OnFingerUp?.Invoke();
    }

    void UpdateDebug(Vector3 worldPos, Color color)
    {
        worldPos.z = _debugInput.transform.position.z;

        SpriteRenderer sprite = _debugInput.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = color;
        }
        _debugInput.transform.position = worldPos;
    }
}
