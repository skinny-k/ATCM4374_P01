using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartmentDie : GameDie
{
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
