using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalDie : GameDie
{
    public static CapitalDie Instance;

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
