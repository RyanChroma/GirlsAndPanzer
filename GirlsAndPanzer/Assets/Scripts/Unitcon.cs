using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unitcon : MonoBehaviour
{
    public Transform shotObj;
    public GameObject attackZone;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(-2, 0, 0);
    }

    void Update()
    {
        if(attackZone.tag == "Active")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            StartCoroutine(fireSeq());
            attackZone.tag = "Bombarding";
        }
    }

    IEnumerator fireSeq()
    {
        yield return new WaitForSeconds(1);
        Instantiate(shotObj, new Vector3(transform.position.x, shotObj.position.y, transform.position.z), transform.rotation);
        StartCoroutine(fireSeq());
    }
}
