using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStats : MonoBehaviour
{

    int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value > 0)
            {
                health = value;
            }
            else
            {
                health = 0;
            }
        }
    }

    bool isAudioPlaying = false;

    [SerializeField] ParticleSystem explosionVFX;

    AudioSource audioSource;

    MeshRenderer meshRenderer;
    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            collider.enabled = false;
            meshRenderer.enabled = false;
            if (!isAudioPlaying)
            {
                Instantiate(explosionVFX, transform.position, transform.rotation);
                audioSource.Play();
                isAudioPlaying = true;
            }
            
            Destroy(gameObject, 4f);
        }
    }
}
