using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    float fuelUsage = 2f;

    [SerializeField] float turboConsume = 3f;
    [SerializeField] SpaceShipController spaceShipController;

    public GameObject ammoType;

    public float fuelTank = 100f;
    public int ammoCapacity = 300;
    public int ammo = 100;

    public bool isAlive { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        spaceShipController = GetComponent<SpaceShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float DecreaseFuel()
    {
        if (spaceShipController.isTurboActive)
        {
            float fuelAux = fuelUsage;
            fuelUsage *= turboConsume;
            fuelTank -= fuelUsage;
            fuelUsage = fuelAux;
        }
        else
        {
            fuelTank -= fuelUsage;
        }

        return fuelTank;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            float fuelTotal = fuelTank + 25f;
            if (fuelTotal >= 100)
            {
                fuelTank = 100f;
            }
            else
            {
                fuelTank += 25f;
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("AmmoSupply"))
        {
            int ammoTotal = ammo + 50;
            if(ammoTotal >= ammoCapacity)
            {
                ammo = ammoCapacity;
            }
            else
            {
                ammo = ammoTotal;
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            isAlive = false;
        }
    }
}
