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

    public GameObject target;
    public GameObject fuel;
    public GameObject ammo;
    public GameObject[] obstacle = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameObjectSpawnTimeHandle(-xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 2f, target));
        StartCoroutine(GameObjectSpawnTimeHandle(-xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 15f, fuel));
        StartCoroutine(GameObjectSpawnTimeHandle(-xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, 40f, ammo));
        StartCoroutine(GameObjectSpawnTimeHandle(-xObstacle, xObstacle, -yObstacle, yObstacle, zObstacle, zObstacle, 17f, null, obstacle));
    }

    // Update is called once per frame
    void Update()
    {

        
    }
   
    void RespawnGameObject(float xMinRange, float xMaxRange, float yMinRange, float yMaxRange, float zMinRange, float zMaxRange, GameObject gameObject = default, GameObject[] gameObjects = default)
    {
        float xPos = Random.Range(xMinRange, xMaxRange);
        float yPos = Random.Range(yMinRange, yMaxRange);
        float zPos = Random.Range(zMinRange, zMaxRange);

        Vector3 respawnPos = new Vector3(xPos, yPos, zPos);
        
        if (!(gameObjects == null))
        {
            int index = RandomObstacle();
            Instantiate(gameObjects[index], respawnPos, gameObjects[index].transform.rotation);
        }
        else
        {
            Instantiate(gameObject, respawnPos, gameObject.transform.rotation);
        }
    }

    IEnumerator GameObjectSpawnTimeHandle(float xMinRange, float xMaxRange, float yMinRange, float yMaxRange, float zMinRange, float zMaxRange, float repeatAfter, GameObject gameObject = default(GameObject), GameObject[] gameObjects = default(GameObject[]))
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatAfter);
            RespawnGameObject(xMinRange, xMaxRange, yMinRange, yMaxRange, zMinRange, zMaxRange, gameObject, gameObjects);
        }
    }

    int RandomObstacle()
    {
        return Random.Range(0, 5);
    }
}
