using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartmentDie : GameDie
{
    [SerializeField] List<Department> _departmentIndices = new List<Department>();

    public enum Department { Accounting, Legal, IT, HR, Facilities, Management }

    public List<Department> DepartmentIndices { get => _departmentIndices; }
    public static DepartmentDie Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
