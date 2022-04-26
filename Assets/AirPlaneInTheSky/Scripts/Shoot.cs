using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    int ammo;

    [SerializeField] GameObject spaceShip;
    [SerializeField] ParticleSystem shotFlash;
    
    public GameObject ammoType;

    // Start is called before the first frame update
    void Start()
    {
        ammo = spaceShip.GetComponent<SpaceShip>().Ammo;
    }

    // Update is called once per frame
    void Update()
    {
        ammo = spaceShip.GetComponent<SpaceShip>().Ammo;
        
        shotFlash.Stop();
        
        Fire();
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0)
            {
                shotFlash.Play();
                
                spaceShip.GetComponent<SpaceShip>().PlayShoot();
            
                Instantiate(ammoType, transform.position, transform.rotation);
                
                ammo--;
                
                spaceShip.GetComponent<SpaceShip>().Ammo = ammo;
            }
        }
    }
}


