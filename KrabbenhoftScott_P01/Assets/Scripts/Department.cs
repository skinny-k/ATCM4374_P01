using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Department : MonoBehaviour
{
    [SerializeField] public float _drainSpeed = 0;

    public Slider Capital_Slider { get => _slider; }
    public float Capital { get; protected set; } = 0;
    public float DrainSpeed { get => _drainSpeed; }

    ResourceManager _manager;
    Slider _slider;
    float _maxResources = 99f;

    void OnEnable()
    {
        _slider = GetComponent<Slider>();
    }

    void UpdateSlider()
    {
        // _manager.UpdateSliders(Capital);
        _slider.value = Capital;
    }
    
    public float GiveCapital(float amount)
    {
        amount = Mathf.Clamp(amount, 0, _maxResources - Capital);
        Capital += amount;
        UpdateSlider();
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
}
