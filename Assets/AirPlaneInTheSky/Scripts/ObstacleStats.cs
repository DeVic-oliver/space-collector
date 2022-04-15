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

    [SerializeField] ParticleSystem explosionVFX;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
