using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject SpawnManager;

    private void Start()
    {
        SpawnManager = GameObject.Find("SpawnManager");    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpawnManager.GetComponent<SpawnManager>().itemPool.Release(gameObject);
        }
    }
}
