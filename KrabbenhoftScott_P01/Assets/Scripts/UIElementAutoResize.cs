using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIElementAutoResize : MonoBehaviour
{
    enum AnchorPoint { TopLeft, TopCenter, TopRight, MiddleLeft, MiddleCenter, MiddleRight, BottomLeft, BottomCenter, BottomRight }
    
    [Range(0.0f, 1.0f)][SerializeField] float _xProportion = 1f;
    [Range(0.0f, 1.0f)][SerializeField] float _yProportion = 1f;
    [SerializeField] AnchorPoint _anchor = AnchorPoint.MiddleCenter;

    RectTransform _rectTransform;
    
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        Debug.Log(_rectTransform.anchorMin + ", " + _rectTransform.anchorMax);
        
        // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width * _xProportion);
        // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height * _yProportion);
        
        // SetNewAnchors();
    }

    void SetNewAnchors()
    {
        float top = 0;
        float bottom = 0;
        float left = 0;
        float right = 0;
        
        switch (_anchor)
        {
            case AnchorPoint.TopLeft:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
            case AnchorPoint.TopCenter:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
            case AnchorPoint.TopRight:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
            case AnchorPoint.MiddleLeft:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
            case AnchorPoint.MiddleCenter:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
            case AnchorPoint.MiddleRight:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
            case AnchorPoint.BottomLeft:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
            case AnchorPoint.BottomCenter:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
            case AnchorPoint.BottomRight:
                bottom = Screen.height * _yProportion;
                right = Screen.width * _xProportion;
                break;
        }

        // rectTransform.anchoredPosition = new Vector2(x, y);
    }
}
