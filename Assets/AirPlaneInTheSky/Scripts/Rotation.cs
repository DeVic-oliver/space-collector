using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 eulers;
    int opt;
    private void Start()
    {
        opt = Random.Range(0, 7);
        CheckGameObject();
       
    }
    // Update is called once per frame
    void Update()
    {
    //Just a simple rotation for any gameobject with this script attached
        transform.Rotate(eulers, Space.World);
    }

    void CheckGameObject()
    {
        if (gameObject.CompareTag("Obstacle"))
        {
            switch (opt)
            {
                case 0:
                    eulers = new Vector3(1, 0, 0);
                    break;
                case 1:
                    eulers = new Vector3(0, 1, 0);
                    break;
                case 2:
                    eulers = new Vector3(0, 0, 1);
                    break;
                case 3:
                    eulers = new Vector3(1, 1, 0);
                    break;
                case 4:
                    eulers = new Vector3(1, 0, 1);
                    break;
                case 5:
                    eulers = new Vector3(0, 1, 1);
                    break;
                case 6:
                    eulers = new Vector3(1, 1, 1);
                    break;
            }
        }
        else
        {
            eulers = new Vector3(0, 1, 0);
        }
    }
}
