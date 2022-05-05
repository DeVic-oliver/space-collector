using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    int ammo;

    CannonBarrel barrelLeftScript, barrelRightScript;

    [SerializeField] GameObject spaceShip;
    
    [SerializeField] GameObject barrelLeft, barrelRight;
    
    [SerializeField] ParticleSystem shotFlash;
    
    [SerializeField] GameObject ammoType;

    public GameObject AmmoType { get { return ammoType; } }

    // Start is called before the first frame update
    void Start()
    {
        ammo = spaceShip.GetComponent<SpaceShip>().Ammo;

        barrelLeftScript = barrelLeft.GetComponent<CannonBarrel>();

        barrelRightScript = barrelRight.GetComponent<CannonBarrel>();

    }

    // Update is called once per frame
    void Update()
    {
        ammo = spaceShip.GetComponent<SpaceShip>().Ammo;
        
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

                barrelLeftScript.GetBullet();
                
                barrelRightScript.GetBullet();

                ammo--;
                
                spaceShip.GetComponent<SpaceShip>().Ammo = ammo;
            }
        }
    }
}


