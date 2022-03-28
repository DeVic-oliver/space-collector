using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
    //Just a simple rotation for any gameobject with this script attached
        transform.Rotate(0,1,0,Space.World);
    }
}
