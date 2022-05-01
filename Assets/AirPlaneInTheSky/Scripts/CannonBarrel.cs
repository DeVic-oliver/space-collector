using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CannonBarrel : MonoBehaviour
{
    GameObject bullet;

    Cannon cannonScript;

    public ObjectPool<GameObject> pool;

    [SerializeField] GameObject cannon;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(CreatedPoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 100, 300);
    }

    private void Start()
    {
        cannonScript = cannon.GetComponent<Cannon>();

        bullet = cannonScript.AmmoType;
    }

    private void Update()
    {
    }

    GameObject CreatedPoolItem()
    {
        GameObject instance = Instantiate(bullet, Vector3.zero, bullet.transform.rotation);
        instance.SetActive(false);
        return instance;
    }

    void OnTakeFromPool(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    void OnReturnedToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject bullet)
    {
        Destroy(bullet);
    }

    public void GetBullet()
    { 
        GameObject bullet = pool.Get();
        bullet.GetComponent<Ammo>().CannonBarrel = gameObject;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        Debug.Log(pool.CountActive);
        Debug.Log(pool.CountInactive);
        Debug.Log(pool.CountAll);
    }
}
