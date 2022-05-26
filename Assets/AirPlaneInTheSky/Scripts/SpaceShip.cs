using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    int fuelUsage = 2;
    int fuelTank = 100;
    int limitHitCount = 0;

    bool isDestroyedAudioPlaying = false;

    bool isPlayerTrespassing = false;
    public bool IsPlayerTrespassing { 
        get { return isPlayerTrespassing; }
        set { isPlayerTrespassing = value; } 
    }

    GameManager gameManager;
    AudioSource spaceShipAudioSource;

    [SerializeField] int turboConsume = 3;
    [SerializeField] int ammoCapacity = 300;
    [SerializeField] GameObject targetVFX;
    [SerializeField] GameObject ammoVFX;
    [SerializeField] GameObject fuelVFX;
    [SerializeField] SpaceShipController spaceShipController;
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] ParticleSystem timeVFX;
    [SerializeField] AudioClip turboSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip spaceShipDestroyedSound;

    public GameObject ammoType;

    public int FuelTank 
    {
        get { return fuelTank; }
        set
        {
            if(value < 0)
            {
                fuelTank = 0;
            }
            else
            {
                fuelTank = value;
            }
        }
    }


    int ammo = 300;
    public int Ammo
    {
        get { return ammo; }
        set
        {
            if(value < 0)
            {
                ammo = 0;
            }
            else
            {
                ammo = value;
            }
        }
    }
    
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
        
        spaceShipAudioSource = GetComponent<AudioSource>();

        explosionVFX = GameObject.Find("WFX_Nuke").GetComponent<ParticleSystem>();
    }


    public float DecreaseFuel()
    {
        if (spaceShipController.isTurboActive)
        {
            
            int fuelAux = fuelUsage;
            
            fuelUsage *= turboConsume;
         
            FuelTank -= fuelUsage;
            
            fuelUsage = fuelAux;
        }
        else
        {
            FuelTank -= fuelUsage;
        }

        return FuelTank;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            gameManager.AddScore();

            PlayEffectsOfChild(targetVFX);
        }

        if (other.gameObject.CompareTag("Fuel"))
        {
            gameManager.AddFuel();

            PlayEffectsOfChild(fuelVFX);
        }

        if (other.gameObject.CompareTag("AmmoSupply"))
        {
            gameManager.AddAmmo();

            PlayEffectsOfChild(ammoVFX);
        }

        if (other.gameObject.CompareTag("TimerSupply"))
        {
            gameManager.AddTime();

            timeVFX.Play();
        }

        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("ForceField"))
        {
            if (!isDestroyedAudioPlaying)
            {
                spaceShipAudioSource.PlayOneShot(spaceShipDestroyedSound);

                isDestroyedAudioPlaying = true;
            }

            explosionVFX.Play();
            
            isAlive = false;
        }
    }

    void PlayEffectsOfChild(GameObject parent)
    {
        int childsCount = parent.transform.childCount;

        for(int i = 0; i < childsCount; i++)
        {
            parent.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }
    }

    public void PlaySound()
    {
        spaceShipAudioSource.PlayOneShot(turboSound, .3f); 
    }

    public void PlayShoot()
    {
        spaceShipAudioSource.PlayOneShot(shootSound, .3f);
    }
}
