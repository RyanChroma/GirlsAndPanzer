using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInput : MonoBehaviour
{
    public Transform SoldierPointer;
    public SoldierMovement soldierMovement;
    public float minMovRange;
    public SpriteRenderer cursor1;

    void Update()
    {
        //Get input.
        if(Input.GetButton("Fire1"))
        {
            //Cast ray to get position of cursor on Terrain.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                if(hit.collider.tag == "Terrain")
                {
                    //Debugging Ray.
                    Debug.DrawLine(ray.origin, hit.point);

                    //Move the Pointer.
                    SoldierPointer.position = new Vector3(hit.point.x, SoldierPointer.position.y, hit.point.z);

                    //Only moves if the distance betweeen the pointer and the soldier is big enough.
                    if(Vector3.Distance(SoldierPointer.position, soldierMovement.transform.position) > minMovRange)
                    {
                        soldierMovement.moving = true;
                        cursor1.enabled = true;
                    }

                    soldierMovement.moving = true;
                    cursor1.enabled = true;
                }
            }
        }
    }

}