using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;

	void OnTriggerEnter(Collider otherCollider)
	{
		if(otherCollider.GetComponent<Bullet>() != null)
		{
			Bullet bullet = otherCollider.GetComponent<Bullet>();

			if(bullet.ShotByPlayer == true)
			{
				health -= bullet.damage;
				bullet.gameObject.SetActive(false);

				if (health <= 0)
				{
					Destroy(gameObject);
				}
			}
		}
	}
}
