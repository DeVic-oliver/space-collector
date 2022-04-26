using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //space spawn coordinates xyz
    float xSpawnRange = 1200;
    float zMinSpawnRange = 1800;
    float zMaxSpawnRange = 4200;
    float ySpawnRange = 600;

    float xObstacle = 1400;
    float yObstacle = 600;
    float zObstacle = 7000;

    int index;

    public GameObject target;
    public GameObject fuel;
    public GameObject ammo;
    public GameObject[] obstacle = new GameObject[5];

   

    // Start is called before the first frame update
    void Start()
    {
        RandomObstacle();
        StartCoroutine(GameObjectSpawnTimeHandle(target, -xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 2f));
        StartCoroutine(GameObjectSpawnTimeHandle(fuel, -xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 15f));
        StartCoroutine(GameObjectSpawnTimeHandle(ammo, -xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 40f));
        StartCoroutine(GameObjectSpawnTimeHandle(obstacle[index], -xObstacle, xObstacle, -yObstacle, yObstacle, zObstacle, zObstacle, 17f));
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
            RandomObstacle();
            RespawnGameObject(gameObject, xMinRange, xMaxRange, yMinRange, yMaxRange, zMinRange, zMaxRange);
        }
    }

    void RandomObstacle()
    {
        index = Random.Range(0, 5);
    }
}
