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
    Collider m_collider;
    GameObject[] childs;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        audioSource = GetComponent<AudioSource>();
        m_collider = GetComponent<Collider>();

         childs = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++){
            childs[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if(health <= 0)
        {
            m_collider.enabled = false;
            foreach(GameObject child in childs)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }
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
