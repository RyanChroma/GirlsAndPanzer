using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zonecon : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.tag = "Active";
    }
}
