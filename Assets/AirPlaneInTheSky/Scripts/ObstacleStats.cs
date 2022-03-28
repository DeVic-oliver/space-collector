using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStats : MonoBehaviour
{

    int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value > 0)
            {
                health = value;
            }
            else
            {
                health = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }
}
