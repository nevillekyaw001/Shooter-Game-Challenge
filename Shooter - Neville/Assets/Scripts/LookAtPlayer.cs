using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Transform mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(mainCamera); //Making Enemies healthbar to look at Player
    }
}
