using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    int damage;
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

    [SerializeField] ParticleSystem hitVFX;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            damage = Random.Range(2, 7);
            
            audioSource.Play();
            
            int obstacleHealth = collision.gameObject.GetComponent<ObstacleStats>().Health - damage;
            
            collision.gameObject.GetComponent<ObstacleStats>().Health = obstacleHealth;
            
            Instantiate(hitVFX, transform.position, transform.rotation);
            
            Destroy(gameObject);
        }
    }
}
