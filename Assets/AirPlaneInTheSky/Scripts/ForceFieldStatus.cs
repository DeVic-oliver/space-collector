using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForceFieldStatus : MonoBehaviour
{
    float health;

    public float Health
    {
        get { return health; }
        set
        {
            CheckHealthValue(value);
        }
    }
    float healthAux;

    bool isForceFieldUp = true;

    [SerializeField] Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForceFieldStatus();
         
    }

    void CheckHealthValue(float value)
    {
        if(value < 0)
        {
            return;
        }
        else
        {
            health = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.GetComponent<ObstacleStats>().Health = 0;
            if(health <= 0)
            {
                GameManager.isGameActive = false;
            }
            else
            {
                healthAux = health;
                health -= 25;
            }
        }
    }

    void CheckForceFieldStatus()
    {
        if(health <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        if (healthAux > health)
        {
            healthBar.value -= 0.01f;
            healthAux--;
        }
    }
}
