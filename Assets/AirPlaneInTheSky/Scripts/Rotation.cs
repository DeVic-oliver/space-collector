using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 eulers;

    int opt;

    float noObstacleSpeed = 15f;
    float obstacleRotationSpeed = 10f;
    private void Start()
    {
        opt = Random.Range(0, 7);
        CheckGameObject();
       
    }
    // Update is called once per frame
    void Update()
    {
    //Just a simple rotation for any gameobject with this script attached
        transform.Rotate(eulers * Time.deltaTime, Space.World);
    }

    void CheckGameObject()
    {
        if (gameObject.CompareTag("Obstacle"))
        {
            switch (opt)
            {
                case 0:
                    eulers = Vector3.Normalize(new Vector3(0.5f, 0, 0)) * obstacleRotationSpeed;
                    break;
                case 1:
                    eulers = Vector3.Normalize(new Vector3(0, 0.5f, 0)) * obstacleRotationSpeed;
                    break;
                case 2:
                    eulers = Vector3.Normalize(new Vector3(0, 0, 0.5f)) * obstacleRotationSpeed;
                    break;
                case 3:
                    eulers = Vector3.Normalize(new Vector3(0.5f, 0.5f, 0)) * obstacleRotationSpeed;
                    break;
                case 4:
                    eulers = Vector3.Normalize(new Vector3(0.5f, 0, 0.5f)) * obstacleRotationSpeed;
                    break;
                case 5:
                    eulers = Vector3.Normalize(new Vector3(0, 0.5f, 0.5f)) * obstacleRotationSpeed;
                    break;
                case 6:
                    eulers = Vector3.Normalize(new Vector3(0.5f, 0.5f, 0.5f)) * obstacleRotationSpeed;
                    break;
            }
        }
        else
        {
            eulers = new Vector3(0, 1, 0) * noObstacleSpeed;
        }
    }
}
