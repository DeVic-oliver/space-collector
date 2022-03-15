using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    float fireRate = 13.75f;
    float auxFireRate = 13.75f;

    int ammo;

    [SerializeField] GameObject spaceShip;

    public GameObject ammoType;

    // Start is called before the first frame update
    void Start()
    {
        ammo = spaceShip.GetComponent<SpaceShip>().ammo;
    }

    // Update is called once per frame
    void Update()
    {
        ammo = spaceShip.GetComponent<SpaceShip>().ammo;

        if (Input.GetButton("Fire1"))
        {
            if (!(ammo == 0) && ammo <= spaceShip.GetComponent<SpaceShip>().ammo)
            {
                fireRate--;
                if (fireRate <= 0)
                {
                    Instantiate(ammoType, transform.position, transform.rotation);
                    ammo--;
                    spaceShip.GetComponent<SpaceShip>().ammo = ammo;
                    fireRate = auxFireRate;
                }
            }
        }
    }


}
