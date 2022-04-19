using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera playerCamera;

    [Header("Gameplay")]
    public int initialAmmo = 200;
    public int ammo;
    public GameObject bulletPrefab;

    void Start()
    {
        ammo = initialAmmo;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(ammo > 0)
            {
                ammo--;

                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet();
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }
    }
}