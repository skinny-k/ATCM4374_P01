using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] CapitalDie _capitalDie;
    [SerializeField] DepartmentDie _departmentDie;

    public CapitalDie Capital_Die { get => _capitalDie; }
    public DepartmentDie Department_Die { get => _departmentDie; }

    public void RollDice()
    {
        _capitalDie.Roll();
        _departmentDie.Roll();
    }
}
