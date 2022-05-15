using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public float shootingInterval;
    public float shootingDistance;

    public Player player;
    public float shootingTimer;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        shootingTimer = Random.Range(0, shootingInterval);
    }

    void Update()
    {
        shootingTimer -= Time.deltaTime;
        if(shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)
		{
            shootingTimer = shootingInterval;

            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = (player.transform.position - transform.position).normalized;
		}
    }
}