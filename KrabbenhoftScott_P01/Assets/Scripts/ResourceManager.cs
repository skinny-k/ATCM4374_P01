using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] GameController _controller;
    [SerializeField] List<Department> _departments = new List<Department>();
    [SerializeField] float _maxResources = 99f;
    [SerializeField] float _initialResources = 10f;
    List<float> _resources = new List<float>();

    public float GiveCapitalToDepartment(float capital, int department)
    {
        capital = Mathf.Clamp(capital, 0, _maxResources - _resources[department - 1]);
        _resources[department - 1] += capital;
        return capital;
    }

    public void DecrementResources()
    {
        /*
        for (int i = 0; i < _departments.Count; i++)
        {
            _resources[i] = Mathf.Clamp(_resources[i] - _departments[i].DrainSpeed, 0, _maxResources);
            if (_resources[i] == 0)
            {
                _controller.GetComponent<GameSM>().ChangeState<LoseState>();
            }
        }
        */
    }

    public void SetInitial()
    {
        _resources.Clear();

        for (int i = 0; i < _departments.Count; i++)
        {
            _resources.Add(_initialResources);
        }
    }
}
