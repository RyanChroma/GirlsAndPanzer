using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera playerCamera;

    [Header("Gameplay")]
    public int initialHealth;
    public int health;
    public float knockbackForce;
    public float hurtDuration;
    public int Health { get { return health; } }

    public int initialAmmo;
    public int ammo;
    public int Ammo { get { return ammo; } }
    public GameObject bulletPrefab;
    public bool isHurt;

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

                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true);
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }
    }

	void OntriggerEnter(Collider otherCollider)
	{
        Debug.Log(otherCollider.name);
		if(otherCollider.GetComponent<AmmoCrate>() != null)
		{   //Collect ammo crates.
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>().gameObject.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;
            Destroy(ammoCrate.gameObject);
        }

        if (isHurt == false)
        {
            GameObject hazard = null;

            if(otherCollider.GetComponent<Enemy>() != null)
			{
                Enemy enemy = otherCollider.GetComponent<Enemy>();
                hazard = enemy.gameObject;
                health -= enemy.damage;
            }

            else if(otherCollider.GetComponent<Bullet>() != null)
			{
                Bullet bullet = otherCollider.GetComponent<Bullet>();
                if(bullet.ShotByPlayer == false)
				{
                    hazard = bullet.gameObject;
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
				}
			}

            if(hazard != null)
			{
                isHurt = true;

                //Perform the knockback effect.
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutine());
            }
		}
	}

    IEnumerator HurtRoutine()
	{
        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
	}
}