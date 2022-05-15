using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeDuration;
    public int damage;

    public float lifeTimer;
    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }

    void OnEnable()
    {
        lifeTimer = lifeDuration;
    }

    void Update() 
    {
        //Make the bullet move.
        transform.position += transform.forward * speed * Time.deltaTime;

        //Check if the bullet should be destroyed.
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
