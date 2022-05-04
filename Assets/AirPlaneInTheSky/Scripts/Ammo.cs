using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    int damage;

    float speed = 1200f;
    float timeToDestroy = 2f;
    float countToRelease;

    bool isDestroyed = false;

    public int Damage 
    {
        get { return damage; } 
        set 
        {
            if(value <= 0)
            {
                damage = 1;
            }
            else
            {
                damage = value;
            }
        }
    }

    AudioSource audioSource;

    CannonBarrel CannonBarrelScript;

    [SerializeField] ParticleSystem hitVFX;

    GameObject cannonBarrel;
    
    public GameObject CannonBarrel { get { return cannonBarrel; } set { cannonBarrel = value; } }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        CannonBarrelScript = cannonBarrel.GetComponent<CannonBarrel>();

        hitVFX = GameObject.Find("WFX_Explosion Small").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (isDestroyed == true)
        {
            if (countToRelease > 4)
            {
                ReturnToPool();
                countToRelease = 0;
            }
            else
            {
                countToRelease += Time.deltaTime;
            }
        }
        else
        {
            MoveFoward();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            hitVFX.Play();

            isDestroyed = true;

            gameObject.GetComponent<MeshRenderer>().enabled = false;

            damage = Random.Range(2, 7);
            
            audioSource.Play();
            
            int obstacleHealth = collision.gameObject.GetComponent<ObstacleStats>().Health - damage;
            
            collision.gameObject.GetComponent<ObstacleStats>().Health = obstacleHealth;
        }
    }

    void MoveFoward()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            CannonBarrelScript.pool.Release(gameObject);
            timeToDestroy = 2f;
        }
    }

    void ReturnToPool()
    {
        CannonBarrelScript.pool.Release(gameObject);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        isDestroyed = false;
    }
}
