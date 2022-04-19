using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance {get {return instance;} }
    public GameObject bulletPrefab;
    public int bulletAmount;
    public List<GameObject> bullets;
    void Awake()
    {
        instance = this;
        //Preload bullets.
        bullets = new List<GameObject>(bulletAmount);

        for(int i = 0; i < 20; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            bullets.Add(prefabInstance);
        }
    }

    public GameObject GetBullet()
    {
        foreach(GameObject bullet in bullets)
        {
            if(!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.SetActive(false);
        bullets.Add(prefabInstance);

        return prefabInstance;
    }
}