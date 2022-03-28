using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //space spawn coordinates xyz
    float xSpawnRange = 390;
    float zMinSpawnRange = 1200;
    float zMaxSpawnRange = 1900;
    float yMinSpawnRange = 7;
    float yMaxSpawnRange = 150;

    public GameObject target;
    public GameObject fuel;
    public GameObject ammo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameObjectSpawnTimeHandle(target, 2f));
        StartCoroutine(GameObjectSpawnTimeHandle(fuel, 15f));
        StartCoroutine(GameObjectSpawnTimeHandle(ammo, 40f));
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void RespawnGameObject(GameObject gameObject)
    {
        float xPos = Random.Range(-xSpawnRange, xSpawnRange);
        float yPos = Random.Range(yMinSpawnRange, yMaxSpawnRange);
        float zPos = Random.Range(zMinSpawnRange, zMaxSpawnRange);

        Vector3 respawnPos = new Vector3(xPos, yPos, zPos);
        Instantiate(gameObject, respawnPos, gameObject.transform.rotation);
    }

    IEnumerator GameObjectSpawnTimeHandle(GameObject gameObject, float repeatAfter)
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatAfter);
            RespawnGameObject(gameObject);
        }
    }
    
}
