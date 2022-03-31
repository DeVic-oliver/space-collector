using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool isCollected = false;

    ParticleItemHandle particleHandle;

    // Start is called before the first frame update
    void Start()
    {
        particleHandle = GetComponent<ParticleItemHandle>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           particleHandle.PlayParticle();
        }
    }
}
