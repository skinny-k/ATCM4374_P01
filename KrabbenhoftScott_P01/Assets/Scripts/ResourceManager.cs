using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] GameController _controller;
    [SerializeField] List<Department> _departments = new List<Department>();
    [SerializeField] float _maxResources = 99f;
    [SerializeField] float _initialResources = 10f;
    [SerializeField] float _capitalToCashOut = 100f;

    [Header("Buttons")]
    [SerializeField] List<Button> _incrementButtons = new List<Button>();
    [SerializeField] List<Button> _decrementButtons = new List<Button>();

    List<float> _resources = new List<float>();
    float _totalCapital = 0;
    float _liquidCapital = 0;

    public bool GiveCapitalToDepartment(float capital, int department)
    {
        if (department > 0)
        {
            _totalCapital += _departments[department - 1].GiveCapital(capital);
        }
        else if (department == 0 && capital > 0)
        {
            foreach (Department dept in _departments)
            {
                _totalCapital += dept.GiveCapital(capital);
            }
        }

        if (_totalCapital >= _capitalToCashOut)
        {
            _controller.GetComponent<GameSM>().ChangeState<WinState>();
            return false;
        }

        return true;
    }

    public void DrainCapital()
    {
        bool bankrupt = false;
        foreach (Department dept in _departments)
        {
            if (dept.DrainCapital() == 0)
            {
                bankrupt = true;
            }
        }
        if (bankrupt)
        {
            _controller.GetComponent<GameSM>().ChangeState<LoseState>();
        }
    }

    public void DecrementDepartmentCapital(int department)
    {
        if (_departments[department].DecrementCapital() == 0)
        {
            _controller.GetComponent<GameSM>().ChangeState<LoseState>();
        }
        else
        {
            _liquidCapital++;
            if (_liquidCapital > 0)
            {
                EnableIncrement();
            }
        }
    }

    public void IncrementDepartmentCapital(int department)
    {
        if (_liquidCapital > 0)
        {
            _departments[department].GiveCapital(1f);
            _liquidCapital--;
            if (_liquidCapital == 0)
            {
                DisableIncrement();
            }
        }
    }

    public void SetInitial()
    {
        foreach (Department dept in _departments)
        {
            _totalCapital += dept.SetCapital(_initialResources, _maxResources, this);
            dept.Capital_Slider.maxValue = _maxResources;
        }

        _liquidCapital = 0;
        DisableIncrement();

        // Debug.Log(_totalCapital);
    }

    public void DisableIncrement()
    {
        foreach (Button button in _incrementButtons)
        {
            button.interactable = false;
        }
    }

    public void DisableDecrement()
    {
        foreach (Button button in _decrementButtons)
        {
            button.interactable = false;
        }
    }

    public void EnableIncrement()
    {
        if (_liquidCapital > 0)
        {
            foreach (Button button in _incrementButtons)
            {
                button.interactable = true;
            }
        }
    }

    public void EnableDecrement()
    {
        foreach (Button button in _decrementButtons)
        {
            button.interactable = true;
        }
    }

    /*
    public void UpdateSliders(float amount)
    {
        Debug.Log(_maxSliderValue + ", " + (amount + _sliderUpperBuffer));
        if (_maxSliderValue <= Mathf.Min(amount + _sliderUpperBuffer, _maxResources))
        {
            _maxSliderValue = Mathf.Min(amount + _sliderUpperBuffer, _maxResources);
            foreach (Department dept in _departments)
            {
                dept.Capital_Slider.maxValue = _maxSliderValue;
            }
        }
    }
    */
}
