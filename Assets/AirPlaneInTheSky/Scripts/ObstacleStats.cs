using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStats : MonoBehaviour
{

    int health = 100;
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

    float countToRelease = 0;

    float speed = 100;

    bool isAudioPlaying = false;
    [SerializeField] bool isDestroyed;

    AudioSource audioSource;
    Collider m_collider;
    GameObject[] childs;
    ParticleSystem explosionVFX;
    
    GameObject SpawnManager;


    private void Awake()
    {
        SpawnManager = GameObject.Find("SpawnManager");
    }

    // Start is called before the first frame update
    void Start()
    {
        isDestroyed = false;

        audioSource = GetComponent<AudioSource>();
        
        m_collider = GetComponent<Collider>();

        childs = new GameObject[gameObject.transform.childCount - 1];
        
        for (int i = 0; i < gameObject.transform.childCount; i++){

            if (gameObject.transform.GetChild(i).gameObject.CompareTag("ObstacleChild"))
            {

                childs[i] = gameObject.transform.GetChild(i).gameObject;
            }else if (gameObject.transform.GetChild(i).gameObject.CompareTag("FxTemporaire"))
            {
                explosionVFX = gameObject.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
            }
        }
    }

    private void Update()
    {
        CheckHealth();

        Movimentation();

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
    }

    void Movimentation()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    void CheckHealth()
    {
        if (health <= 0 && isDestroyed == false)
        {
            isDestroyed = true;
            m_collider.enabled = false;
            
            foreach (GameObject child in childs)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }
            
            if (!isAudioPlaying)
            {
                explosionVFX.Play();
                audioSource.Play();
                isAudioPlaying = true;
            }
        }
    }

    void ReturnToPool()
    {
        SpawnManager.GetComponent<SpawnManager>().obstaclePool.Release(gameObject);
        isDestroyed = false;
        health = 100;
        m_collider.enabled = true;
        foreach (GameObject child in childs)
        {
            child.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
