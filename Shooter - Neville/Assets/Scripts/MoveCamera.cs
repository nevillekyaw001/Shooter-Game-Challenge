using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPos;

    void Update()
    {
        //To Locate Camera to Player Object
        transform.position = cameraPos.position;

    }
}
