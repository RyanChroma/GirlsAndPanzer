using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Contains NavMesh classes.

public class SoldierMovement : MonoBehaviour
{
    public bool moving;
    public NavMeshAgent agent;
    public Transform pointer;

    void Update()
    {
        if(moving)
        {
            agent.SetDestination(pointer.position);
            agent.isStopped = false;
        }
    }
}
