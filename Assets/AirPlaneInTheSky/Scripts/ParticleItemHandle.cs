using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleItemHandle : MonoBehaviour
{
    [SerializeField] GameObject particlesContainer;
    GameObject[] vfxs = new GameObject[2];

    // Start is called before the first frame update
    
    void Start()
    {
        GetContainerChilds();
        Instantiate(particlesContainer, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GetContainerChilds()
    {
        for(int i = 0; i < particlesContainer.transform.childCount; i++)
        {
            vfxs[i] = particlesContainer.transform.GetChild(i).gameObject;
        }

        foreach (GameObject vfx in vfxs)
        {
            vfx.transform.localScale = new Vector3(20, 20, 20);
            vfx.GetComponent<ParticleSystem>().Pause();
        }
    }

    public void PlayParticle()
    {
       foreach (GameObject vfx in vfxs)
       {
            vfx.GetComponent<ParticleSystem>().Play();
            Destroy(particlesContainer, 0.6f);
       }

    }
}
