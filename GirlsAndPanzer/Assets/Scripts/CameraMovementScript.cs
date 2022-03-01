using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public float speed = 0.1f;
    public float zoomSpeed = 30.0f;
    //public float rotateSpeed = 0.1f;

    float maxHeight = 60f;
    float minHeight = 10f;

    Vector2 p1;
    Vector2 p2;

    void Start()
    {
        
    }

    void Update()
    {
        /*if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.1f;
            zoomSpeed = 60f;
        }
        else
        {
            speed = 0.05f;
            zoomSpeed = 30.0f;
        }*/

        float hsp = Mathf.Log(transform.position.y) * speed * Input.GetAxis("Horizontal");
        float vsp = Mathf.Log(transform.position.y) * speed *Input.GetAxis("Vertical");
        float scrollSP = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        if((transform.position.y >= maxHeight) && (scrollSP > 0))
        {
            scrollSP = 0;
        }
        else if((transform.position.y <= minHeight) && (scrollSP > 0))
        {
            scrollSP = 0;
        }

        if((transform.position.y + scrollSP) > maxHeight)
        {
            scrollSP = maxHeight - transform.position.y;
        }
        else if((transform.position.y + scrollSP) < minHeight)
        {
            scrollSP = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrollSP, 0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vsp;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        getCameraRotation();
    }

    void getCameraRotation()
    {
        if(Input.GetMouseButtonDown(2)) //Check if the middle mouse button is pressed.
        {
            p1 = Input.mousePosition;
        }

        if(Input.GetMouseButton(2)) //Check if the middle mouse button is beng held down.
        {
            p2 = Input.mousePosition;

            /*float dx = (p1-p2).x * rotateSpeed;
            float dy = (p2-p1).y * rotateSpeed;*/

            //transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0)); //Y rotation.

            p1 = p2;
        }
    }
}