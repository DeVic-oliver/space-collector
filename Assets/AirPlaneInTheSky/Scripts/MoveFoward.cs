using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoward : MonoBehaviour
{
    float speed = 1200f;
    float timeToDestroy = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0)
        {
            Destroy(gameObject);
            timeToDestroy = 5f;
        }
    }
}
