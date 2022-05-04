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

                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet();
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }
    }

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
        Debug.Log(hit.collider.name);
		if(hit.collider.GetComponent<AmmoCrate>() != null)
		{   //Collide with ammo crates.
            AmmoCrate ammoCrate = hit.collider.GetComponent<AmmoCrate>().gameObject.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;
            Destroy(ammoCrate.gameObject);
        }

        else if(hit.collider.GetComponent<Enemy>() != null)
		{
            if(isHurt == false)
			{
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                health -= enemy.damage;

                isHurt = true;

                //Perform the knockback effect.
                Vector3 hurtDirection = (transform.position - enemy.transform.position).normalized;
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