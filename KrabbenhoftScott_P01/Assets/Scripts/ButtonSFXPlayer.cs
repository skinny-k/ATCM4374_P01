using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFXPlayer : MonoBehaviour
{
    [SerializeField] AudioClip _sfx;
    [Range(0.0f, 1.0f)]
    [SerializeField] float _volume = 1f;
    
    public void Play()
    {
        AudioHelper.PlayClip2D(_sfx, _volume);
    }
}
