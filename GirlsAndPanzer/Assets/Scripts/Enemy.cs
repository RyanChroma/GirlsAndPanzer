using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
	public static Action onEnemyDeath;

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
					onEnemyDeath?.Invoke();
					Cursor.lockState = CursorLockMode.None;
				}
			}
		}
	}
}