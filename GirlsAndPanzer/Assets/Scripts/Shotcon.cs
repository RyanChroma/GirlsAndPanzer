using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotcon : MonoBehaviour
{
    private Vector3 shotTraj;

    void Start()
    {
        shotTraj = transform.forward * 5 + transform.up * 5;
        GetComponent<Rigidbody>().velocity = shotTraj;
    }

    void Update()
    {
        
    }
}
