using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenspaceAnchor : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] float _verticalAnchor = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] float _horizontalAnchor = 0.5f;

    [SerializeField] Camera anchorCamera;

    void Start()
    {
        Vector3 worldPos = anchorCamera.ScreenToWorldPoint(new Vector3(Screen.width * _horizontalAnchor, Screen.height * _verticalAnchor, 10));
        worldPos.z = transform.position.z;

        transform.position = worldPos;
    }
}
