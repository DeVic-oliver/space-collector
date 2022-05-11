using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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

    float countItem = 0;
    float countObstacle = 0;

    [SerializeField] GameObject[] items;
    [SerializeField] GameObject[] obstacles;
    [SerializeField] float itemRespawnTime = 3.75f;
    [SerializeField] float obstacleRespawnTime = 7.25f;

    public ObjectPool<GameObject> itemPool;
    public ObjectPool<GameObject> obstaclePool;


    private void Awake()
    {
        itemPool = new ObjectPool<GameObject>(CreatedPoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 10);
        obstaclePool = new ObjectPool<GameObject>(CreatedPoolObstacle, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 10);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void Update()
    {
        if (!GameManager.isGamePaused)
        {
            if (countItem > itemRespawnTime)
            {
                RespawnGameObject(-xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, items, "Item");
                countItem = 0;
            }
            else
            {
                countItem += Time.deltaTime;
            }
            if (countObstacle > obstacleRespawnTime)
            {
                RespawnGameObject(-xObstacle, xObstacle, -yObstacle, yObstacle, zObstacle, zObstacle, obstacles, "Obstacle");
                countObstacle = 0;
            }
            else
            {
                countObstacle += Time.deltaTime;
            }
        }
    }

    void RespawnGameObject(float xMinRange, float xMaxRange, float yMinRange, float yMaxRange, float zMinRange, float zMaxRange, GameObject[] gameObjects, string type)
    {
        if(type == "Item") { 
           GameObject item = itemPool.Get();
           item.transform.position = RandomPosition(xMinRange, xMaxRange, yMinRange, yMaxRange, zMinRange, zMaxRange);
        }
        else if(type == "Obstacle")
        {
            GameObject obstacle = obstaclePool.Get();
            obstacle.transform.position = RandomPosition(xMinRange, xMaxRange, yMinRange, yMaxRange, zMinRange, zMaxRange);
        }
    }

    int RandomIndex(GameObject[] gameObjectArray)
    {
        return Random.Range(0, gameObjectArray.Length);
    }

    GameObject CreatedPoolItem()
    {
        int index = RandomIndex(items);
        GameObject itemInstance = Instantiate(items[index], Vector3.zero, items[index].transform.rotation);
        itemInstance.SetActive(false);
        return itemInstance;
    }
    GameObject CreatedPoolObstacle()
    {
        int index = RandomIndex(obstacles);
        GameObject obstacleInstance = Instantiate(obstacles[index], Vector3.zero, obstacles[index].transform.rotation);
        obstacleInstance.SetActive(false);
        return obstacleInstance;
    }
    void OnTakeFromPool(GameObject gameObj)
    {
        gameObj.SetActive(true);
    }

    void OnReturnedToPool(GameObject gameObj)
    {
        gameObj.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject gameObj)
    {
        Destroy(gameObj);
    }

    Vector3 RandomPosition(float xMinRange, float xMaxRange, float yMinRange, float yMaxRange, float zMinRange, float zMaxRange) 
    {
        float x = Random.Range(xMinRange, xMaxRange);
        float y = Random.Range(yMinRange, yMaxRange);
        float z = Random.Range(zMinRange, zMaxRange);
        
        Vector3 respawnPos = new Vector3(x, y, z);
        return respawnPos;
    }
}
