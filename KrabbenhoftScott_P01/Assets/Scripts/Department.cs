using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Department : MonoBehaviour
{
    [SerializeField] float _drainSpeed = 0;
    [Range(0.0f, 1.0f)]
    [SerializeField] float _shakeThreshold = 0.2f;
    [SerializeField] float _shakeAmount = 0.5f;
    [SerializeField] ParticleSystem _particlePrefab;
    [SerializeField] Camera _projectionCamera;

    public Slider Capital_Slider { get => _slider; }
    public float Capital { get; protected set; } = 0;
    public float DrainSpeed { get => _drainSpeed; }

    ParticleSystem _particles = null;
    ResourceManager _manager;
    RectTransform _rect;
    Slider _slider;
    float _maxResources = 99f;

    void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (_slider.value <= _slider.maxValue * _shakeThreshold)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-_shakeAmount, _shakeAmount)));
        }
    }

    void UpdateSlider()
    {
        _slider.value = Capital;
        if (_slider.value > _slider.maxValue * _shakeThreshold)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
    
    public float GiveCapital(float amount)
    {
        amount = Mathf.Clamp(amount, 0, _maxResources - Capital);
        Capital += amount;
        UpdateSlider();
        PlayBurst(amount);
        return amount;
    }

    public float DrainCapital()
    {
        Capital = Mathf.Clamp(Capital - DrainSpeed * Time.deltaTime, 0, Mathf.Infinity);
        UpdateSlider();
        return Capital;
    }

    public float DecrementCapital()
    {
        Capital = Mathf.Clamp(Capital - 1f, 0, Mathf.Infinity);
        UpdateSlider();
        return Capital;
    }

    public float SetCapital(float amount, float max, ResourceManager manager = null)
    {
        Capital = amount;
        _maxResources = max;
        _manager = manager;
        return amount;
    }

    void PlayBurst(float amount)
    {
        if (_particles == null)
        {
            RectTransform rect = GetComponent<RectTransform>();
            Vector3 anchor = new Vector3((rect.anchorMax.x + rect.anchorMin.x) / 2, (rect.anchorMax.y + rect.anchorMin.y) / 2, 0);
            Vector3 screenDimensions = new Vector3(Screen.width, Screen.height, 0);
            Vector3 screenPos = Vector3.Scale(anchor, screenDimensions);
            screenPos.z = -_projectionCamera.transform.position.z;

            _particles = Instantiate(_particlePrefab, _projectionCamera.ScreenToWorldPoint(screenPos), Quaternion.identity);
        }
        
        _particles.Stop();
        ParticleSystem.MainModule mainModule = _particles.main;
        mainModule.duration = (0.1f * amount) + 0.05f;
        _particles.Play();
    }
}
