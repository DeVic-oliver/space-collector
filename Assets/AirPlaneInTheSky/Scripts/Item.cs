using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool isCollected = false;

    [SerializeField] GameObject particlesContainer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(particlesContainer, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
