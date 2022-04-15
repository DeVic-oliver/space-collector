using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    GameManager gameManager;

    int fuelUsage = 2;

    [SerializeField] int turboConsume = 3;
    [SerializeField] SpaceShipController spaceShipController;
    [SerializeField] ParticleSystem explosionVFX;

    public GameObject ammoType;

    int fuelTank = 100;
    public int FuelTank 
    {
        get { return fuelTank; }
        set
        {
            if(value < 0)
            {
                Debug.Log("ERROR");
            }
            else
            {
                fuelTank = value;
            }
        }
    }


    int ammo = 100;
    public int Ammo
    {
        get { return ammo; }
        set
        {
            if(value < 0)
            {
                Debug.Log("ERROR");
            }
            else
            {
                ammo = value;
            }
        }
    }
    
    [SerializeField] int ammoCapacity = 300;
    public int AmmoCapacity
    {
        get { return ammoCapacity; }
    }

    public bool isAlive { get; set; }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        spaceShipController = GetComponent<SpaceShipController>();
    }

    public float DecreaseFuel()
    {
        if (spaceShipController.isTurboActive)
        {
            int fuelAux = fuelUsage;
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
        if (other.gameObject.CompareTag("Target"))
        {
            gameManager.AddScore();
        }

        if (other.gameObject.CompareTag("Fuel"))
        {
            gameManager.AddFuel();
        }

        if (other.gameObject.CompareTag("AmmoSupply"))
        {
            gameManager.AddAmmo();
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(explosionVFX,transform.position, transform.rotation);
            isAlive = false;
        }
    }
}
