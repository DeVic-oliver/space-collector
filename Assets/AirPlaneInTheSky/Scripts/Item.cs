using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ParticleSystem vfxCollect_1;
    [SerializeField] ParticleSystem vfxCollect_2;

    // Start is called before the first frame update
    void Awake()
    {
        vfxCollect_1.Stop();
        vfxCollect_2.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        vfxCollect_1.Stop();
        vfxCollect_2.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vfxCollect_1.Play();
            vfxCollect_2.Play();
            Destroy(gameObject);
        }
    }
}
