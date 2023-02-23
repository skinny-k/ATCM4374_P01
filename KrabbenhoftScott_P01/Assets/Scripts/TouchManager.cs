using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchManager : MonoBehaviour
{
    [SerializeField] GameObject _debugInput;
    
    public static event Action<Vector2> OnFingerMove;
    public static event Action<Vector2> OnFingerDown;
    public static event Action OnFingerUp;

    void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        
        Touch.onFingerMove += FingerMove;
        Touch.onFingerDown += FingerDown;
        Touch.onFingerUp += FingerUp;
    }

    void OnDisable()
    {
        Touch.onFingerMove -= FingerMove;
        Touch.onFingerDown -= FingerDown;
        Touch.onFingerUp -= FingerUp;

        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
    }

    void FingerMove(Finger finger)
    {
        OnFingerMove?.Invoke(finger.screenPosition);
        
        UpdateDebug(finger, Color.green);
    }

    void FingerDown(Finger finger)
    {
        OnFingerDown?.Invoke(finger.screenPosition);

        UpdateDebug(finger, Color.white);
    }

    void FingerUp(Finger finger)
    {
        OnFingerUp?.Invoke();

        UpdateDebug(finger, Color.white);
    }

    void UpdateDebug(Finger finger, Color color)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(finger.screenPosition.x, finger.screenPosition.y, 18.5f));
        worldPos.z = _debugInput.transform.position.z;

        SpriteRenderer sprite = _debugInput.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = color;
        }
        _debugInput.transform.position = worldPos;
    }
}
