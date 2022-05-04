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

    float count = 0;

    public ObjectPool<GameObject> itemPool;

    [SerializeField] GameObject[] items;

    [SerializeField] GameObject[] obstacle;

    private void Awake()
    {
        itemPool = new ObjectPool<GameObject>(CreatedPoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 10);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void Update()
    {
        if (count > 3.75f) 
        {
            RespawnGameObject(-xSpawnRange, xSpawnRange, -ySpawnRange, ySpawnRange, zMinSpawnRange, zMaxSpawnRange, items);
            count = 0;
        }
        else
        {
            count += Time.deltaTime;
        }
    }

    void RespawnGameObject(float xMinRange, float xMaxRange, float yMinRange, float yMaxRange, float zMinRange, float zMaxRange, GameObject[] gameObjects = default(GameObject[]))
    {
   
        GameObject item = itemPool.Get();

        float xPos = Random.Range(xMinRange, xMaxRange);
        float yPos = Random.Range(yMinRange, yMaxRange);
        float zPos = Random.Range(zMinRange, zMaxRange);

        item.transform.position = RandomPosition(xPos, yPos, zPos);
     
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

    void OnTakeFromPool(GameObject item)
    {
        item.SetActive(true);
    }

    void OnReturnedToPool(GameObject item)
    {
        item.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject item)
    {
        Destroy(item);
    }

    Vector3 RandomPosition(float x, float y, float z) 
    {
        Vector3 respawnPos = new Vector3(x, y, z);
        return respawnPos;
    }
}
