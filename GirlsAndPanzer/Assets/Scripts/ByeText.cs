using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByeText : MonoBehaviour
{
    public GameObject myBase;
    
    public void DestroyMe()
    {
        Destroy(myBase);
        Destroy(gameObject);
    }
}
