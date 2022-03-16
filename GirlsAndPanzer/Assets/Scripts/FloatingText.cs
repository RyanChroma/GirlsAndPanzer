using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public string dano;
    public TextMesh myTextmesh;

    void Start()
    {
        myTextmesh.text = dano;
        myTextmesh.transform.eulerAngles = Camera.main.transform.eulerAngles;
    }
}
