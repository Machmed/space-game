using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public string weaponName;
    public WeaponType weaponType;
    public AmmunitionType ammoType;
    public int oneShotAmmoNeed;
    public float cooldownTime = 0.5f;
    public float currentCooldown = 0.0f;
    private PlayerResources res;
    public float force = 100.0f;
	
    public void Start()
    {
        res = GameObject.FindObjectOfType<PlayerResources>();
        StartCoroutine("Cooldown");
    }

	public void Use()
    {
        if (currentCooldown >= cooldownTime)
        {
            if (res.RemoveAmmo(ammoType, oneShotAmmoNeed) == true)
            {
                Debug.Log("Shoot: " + oneShotAmmoNeed.ToString() + " projectiles.");
                currentCooldown = 0.0f;
                StartCoroutine("Cooldown");
                CreateProjectiles(res.GetProjectlie(ammoType));
            }
        }
    }

    private void CreateProjectiles(GameObject template)
    {
        for (int i = 0; i < oneShotAmmoNeed; i++)
        {
            GameObject projectile = Instantiate(template);
            projectile.transform.position = this.gameObject.transform.position;
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.Init(ammoType, force);
        }
    }

    public IEnumerator Cooldown()
    {
        while(currentCooldown < cooldownTime)
        {
            currentCooldown += Time.deltaTime;
            yield return null;
        }
    }

    
}

public enum WeaponType
{
    RocketLauncher,
    Minigun
}

public enum AmmunitionType
{
    MiniRocket,
    Bullet9mm
}
