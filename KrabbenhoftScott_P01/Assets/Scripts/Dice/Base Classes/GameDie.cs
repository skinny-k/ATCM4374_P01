using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class GameDie : MonoBehaviour
{
    [SerializeField] protected List<DieFace> _faces = new List<DieFace>();
    [SerializeField] protected float _minCastForce = 2f;
    [SerializeField] protected float _maxCastForce = 20f;
    [SerializeField] protected float _minCastTorque = 0.25f;
    [SerializeField] protected float _maxCastTorque = 3f;

    protected Rigidbody _rb;
    protected bool _rolling = false;

    public event Action<int> OnLand;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    protected virtual void FixedUpdate()
    {
        if (_rolling && _rb.velocity == Vector3.zero)
        {
            _rolling = false;
            OnLand?.Invoke(GetResultOfRoll());
        }
    }
    
    public virtual void Roll()
    {
        if (!_rolling)
        {
            Vector3 forceToApply = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1f);
            forceToApply = forceToApply.normalized * Random.Range(_minCastForce, _maxCastForce);
            Vector3 torqueToApply = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            torqueToApply = torqueToApply.normalized * Random.Range(_minCastTorque, _maxCastTorque);

            _rb.velocity = new Vector3(0, 0, -0.1f);
            _rb.AddForce(forceToApply);
            _rb.AddTorque(torqueToApply);
            _rolling = true;
        }
    }

    public virtual int GetResultOfRoll()
    {
        DieFace resultFace = _faces[0];
        foreach (DieFace face in _faces)
        {
            if (face.transform.position.z == transform.position.z - 0.51f)
            {
                resultFace = face;
                break;
            }
        }

        return resultFace.Number;
    }
}
