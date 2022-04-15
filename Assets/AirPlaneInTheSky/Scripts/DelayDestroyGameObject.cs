using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroyGameObject : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
