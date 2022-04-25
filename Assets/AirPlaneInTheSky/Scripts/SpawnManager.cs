using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //space spawn coordinates xyz
    float xSpawnRange = 2000;
    float zMinSpawnRange = 1000;
    float zMaxSpawnRange = 5000;
    float ySpawnRange = 800;

    float xObstacle = 2000;
    float yObstacle = 600;
    float zObstacle = 7000;

    public GameObject target;
    public GameObject fuel;
    public GameObject ammo;
    public GameObject obstacle;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameObjectSpawnTimeHandle(target, -xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 2f));
        StartCoroutine(GameObjectSpawnTimeHandle(fuel, -xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 15f));
        StartCoroutine(GameObjectSpawnTimeHandle(ammo, -xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 40f));
        StartCoroutine(GameObjectSpawnTimeHandle(obstacle, -xObstacle, xObstacle, -yObstacle, yObstacle, zObstacle, zObstacle, 17f));

    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void RespawnGameObject(GameObject gameObject, float xMinRange, float xMaxRange, float yMinRange, float yMaxRange, float zMinRange, float zMaxRange)
    {
        float xPos = Random.Range(xMinRange, xMaxRange);
        float yPos = Random.Range(yMinRange, yMaxRange);
        float zPos = Random.Range(zMinRange, zMaxRange);

        Vector3 respawnPos = new Vector3(xPos, yPos, zPos);
        Instantiate(gameObject, respawnPos, gameObject.transform.rotation);
    }

    IEnumerator GameObjectSpawnTimeHandle(GameObject gameObject, float xMinRange, float xMaxRange, float yMinRange, float yMaxRange, float zMinRange, float zMaxRange, float repeatAfter)
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatAfter);
            RespawnGameObject(gameObject, xMinRange, xMaxRange, yMinRange, yMaxRange, zMinRange, zMaxRange);
        }
    }

}
