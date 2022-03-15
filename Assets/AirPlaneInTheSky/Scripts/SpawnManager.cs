using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameObject[] targets;

    float xSpawnRange = 390;
    float zMinSpawnRange = 1200;
    float zMaxSpawnRange = 1900;
    float yMinSpawnRange = 7;
    float yMaxSpawnRange = 150;

    [SerializeField] int maxTargetsActive = 10;
    [SerializeField] int maxFuelActive = 3;
    [SerializeField] float spawnRate = 10;

    public GameObject target;
    public GameObject fuel;
    public GameObject ammo;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Respawn", 3f, 1f);

        InvokeRepeating("RespawnFuel", 5f, 10f);

        InvokeRepeating("RespawnAmmo", 15f, 20f);

    }

    // Update is called once per frame
    void Update()
    {
   
    }

    void Respawn()
    {
        float xPos = Random.Range(-xSpawnRange, xSpawnRange);
        float yPos = Random.Range(yMinSpawnRange, yMaxSpawnRange);
        float zPos = Random.Range(zMinSpawnRange, zMaxSpawnRange);

        Vector3 respawnPos = new Vector3(xPos, yPos, zPos);
        Instantiate(target, respawnPos, transform.rotation);
    }

    void RespawnFuel()
    {
        float xPos = Random.Range(-xSpawnRange, xSpawnRange);
        float yPos = Random.Range(yMinSpawnRange, yMaxSpawnRange);
        float zPos = Random.Range(zMinSpawnRange, zMaxSpawnRange);

        Vector3 respawnPos = new Vector3(xPos, yPos, zPos);
        Instantiate(fuel, respawnPos, fuel.transform.rotation);
    }

    void RespawnAmmo()
    {
        float xPos = Random.Range(-xSpawnRange, xSpawnRange);
        float yPos = Random.Range(yMinSpawnRange, yMaxSpawnRange);
        float zPos = Random.Range(zMinSpawnRange, zMaxSpawnRange);

        Vector3 respawnPos = new Vector3(xPos, yPos, zPos);
        Instantiate(ammo, respawnPos, ammo.transform.rotation);
    }

    IEnumerator RespawnCoolDown()
    {
        yield return new WaitForSeconds(spawnRate);
    }
}
