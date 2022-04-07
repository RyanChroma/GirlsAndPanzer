using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public CharacterController playerHeight;
    public float normalHeight, crouchHeight;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            playerHeight.height = crouchHeight;
        }

        if(Input.GetKeyUp(KeyCode.C))
        {
            playerHeight.height = normalHeight;
        }
    }
}