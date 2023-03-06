using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class GameDie : MonoBehaviour
{
    [SerializeField] protected float _minCastForce = 2f;
    [SerializeField] protected float _maxCastForce = 20f;
    [SerializeField] protected float _minCastTorque = 0.25f;
    [SerializeField] protected float _maxCastTorque = 3f;
    [SerializeField] protected float _bounceForce = 5f;
    [SerializeField] protected AudioClip _impactSFX;
    [Range(0.0f, 1.0f)]
    [SerializeField] protected float _sfxVolume = 1f;

    protected List<DieFace> _faces = new List<DieFace>();
    protected Rigidbody _rb;
    protected bool _rolling = false;

    public event Action<int> OnLand;

    Vector3 _pausedVelocity;
    Vector3 _pausedAngularVelocity;

    protected virtual void Start()
    {
        foreach (Transform child in transform)
        {
            DieFace face = child.GetComponent<DieFace>();
            if (face != null)
            {
                _faces.Add(face);
            }
        }
        
        _rb = GetComponent<Rigidbody>();
        _rb.transform.up = _faces[Random.Range(0, _faces.Count)].transform.forward;
    }
    
    protected virtual void FixedUpdate()
    {
        if (_rolling && _rb.velocity == Vector3.zero && !_rb.isKinematic)
        {
            _rolling = false;
            OnLand?.Invoke(GetResultOfRoll());
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        AudioHelper.PlayClip2D(_impactSFX, _sfxVolume, true);
        
        if (collision.collider.gameObject.GetComponent<GameDie>() != null)
        {
            _rb.AddForce(collision.impulse * _bounceForce);
        }
    }
    
    public virtual void Roll()
    {
        if (!_rolling)
        {
            _rb.useGravity = true;
            
            Vector3 forceToApply = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
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
        DieFace resultFace = null;
        foreach (DieFace face in _faces)
        {
            if (face.transform.forward == Vector3.forward)
            {
                resultFace = face;
                break;
            }
        }

        if (resultFace != null)
        {
            return resultFace.Number;
        }
        else
        {
            return 0;
        }
    }

    public virtual void Freeze()
    {
        _pausedVelocity = _rb.velocity;
        _pausedAngularVelocity = _rb.angularVelocity;

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.isKinematic = true;
    }

    public virtual void Unfreeze()
    {
        _rb.velocity = _pausedVelocity;
        _rb.angularVelocity = _pausedAngularVelocity;
        _rb.isKinematic = false;
    }
}
