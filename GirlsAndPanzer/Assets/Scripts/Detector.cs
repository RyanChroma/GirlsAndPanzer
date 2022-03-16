using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    //Variables.
    public Root3 myRoot;

    //Use this for initialization.
    void Start()
    {
        myRoot = gameObject.GetComponentInParent<Root3>();
    }

    //Detecting objects.
    public void OnTriggerEnter(Collider other)
    {
        print("Detecting");

        if(other.tag != "Terrain" && other.tag != "obstacle" && other.tag != "Untagged")
        {
            myRoot.detected.Add(other.GetComponent<Root3>());
        }
    }

    //Removing objects that leave the detection range.
    public void OnTriggerExit(Collider other)
    {
        myRoot.detected.Remove(other.GetComponent<Root3>());
    }

    void Update()
    {
        //Clear enemy list.
        if(myRoot.enemies.Count > 0)
        {
            myRoot.enemies.Clear();
        }

        //Cleaning the detection list.
        foreach(Root3 detectedobject in myRoot.detected)
        {
            if(detectedobject == null)
            {
                myRoot.detected.Remove(detectedobject);
            }
            else
            {
                if(detectedobject.tag == "Enemy")
                {
                    myRoot.enemies.Add(detectedobject);
                }
            }

            //Check for enemies.
            /*if(detectedobject.tag == "Enemy")
            {
                myRoot.enemies.Add(detectedobject);
            }*/
        }

        //Going into combat.
        if(myRoot.currentState != Root3.STATE.Moving)
        {
            if(myRoot.enemies.Count > 0)
            {
                myRoot.ChangeState(Root3.STATE.Combat);
            }
            else
            {
                if(myRoot.currentState == Root3.STATE.Combat)
                {
                    myRoot.ChangeState(Root3.STATE.Idle);
                }
            }
        }
    }
}