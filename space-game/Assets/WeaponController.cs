using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    SingleRocket,
}

public enum Ammunition
{
    SmallRocket,
}

public class WeaponController : MonoBehaviour {

    // Weapons & ammunition
    public int startingAmmo = 10;
    public Weapon currentWeapon;
    private Dictionary<Weapon, Ammunition> ammoMapping;
    public Dictionary<Ammunition, int> playerAmmo;
    float cooldownTime = 1.0f;
    public float currentCooldown = 0.0f;

    // Projectile
    private Dictionary<Ammunition, GameObject> projectileMapping;

    public GameObject[] projectiles;

    void SetBasicWeapon()
    {
        currentWeapon = Weapon.SingleRocket;
    }

    void MapProjectile()
    {
        var values = Ammunition.GetValues(typeof(Ammunition));
        projectileMapping = new Dictionary<Ammunition, GameObject>();

        foreach(Ammunition ammo in values)
        {
            foreach(GameObject go in projectiles)
            {
                if (go.GetComponent<ProjectileController>().projectileName == ammo.ToString())
                {
                    projectileMapping.Add(ammo, go);
                }
            }
        }
    }

    void MapAmmo()
    {
        var values = Weapon.GetValues(typeof(Weapon));
        ammoMapping = new Dictionary<Weapon, Ammunition>();

        foreach (Weapon weapon in values)
        {
            switch (weapon)
            {
                case Weapon.SingleRocket:
                    {
                        ammoMapping.Add(Weapon.SingleRocket, Ammunition.SmallRocket);
                        break;
                    }
            }
        }
    }

    void SetupAmmo()
    {
        playerAmmo = new Dictionary<Ammunition, int>();
        foreach (Ammunition ammo in Ammunition.GetValues(typeof(Ammunition)))
        {
            playerAmmo[ammo] = 0;
            AddAmmo(ammo, 10);
        }
    }

    void AddAmmo(Ammunition ammo, int count)
    {
        playerAmmo[ammo] += count;
    }

    void RemoveAmmo(Weapon weapon, int count = 1)
    {
        Ammunition ammo = ammoMapping[weapon];
        playerAmmo[ammo] -= count;
    }

    public void UseWeapon()
    {
        if (currentWeapon != null)
        {
            FireWeapon(currentWeapon);
        }
    }

    bool FireWeapon(Weapon weapon, int ammoCount = 1)
    {
        bool hasAmmunition = false;
        bool canShoot = false;

        // Check if player has ammunition
        hasAmmunition = CheckForAmmunition(weapon, ammoCount);

        if (!hasAmmunition)
        {
            canShoot = false;
            return canShoot;
        }

        // Check cooldowns

        if (currentCooldown < cooldownTime)
        {
            canShoot = false;
            return canShoot;
        }

        // Step 1: Set cooldown to 0 and make it rise
        HandleCooldown();

        // Remove ammunition from player
        RemoveAmmo(currentWeapon);

        // Shoot projectile
        ShootProjectile(currentWeapon);

        return canShoot;
    }

    IEnumerator Cooldown()
    {
        while (currentCooldown < cooldownTime)
        {
            currentCooldown += Time.deltaTime;
            yield return null;
        }
    }

    bool CheckForAmmunition(Weapon weaopnType, int countRequired = 1)
    {
        Ammunition ammo = ammoMapping[weaopnType];
        return playerAmmo[ammo] >= countRequired ? true : false;
    }

    void ShootProjectile(Weapon weaponType)
    {
        // Just instantinate projectile, controller will handle rest.
        GameObject projectile = Instantiate(GetProjectile(weaponType));
        projectile.transform.position = GameObject.FindObjectOfType<BallController>().gameObject.transform.position;
    }

    GameObject GetProjectile(Weapon weapon)
    {        
        return projectileMapping[GetAmmunitionForWeapon(weapon)];
    }

    Ammunition GetAmmunitionForWeapon(Weapon weapon)
    {
        return ammoMapping[weapon];
    }

    void HandleCooldown()
    {
        currentCooldown = 0.0f;
        StartCoroutine("Cooldown");

    }

    public void Initialize()
    {
        SetBasicWeapon();
        MapAmmo();
        SetupAmmo();
        HandleCooldown();
        MapProjectile();
    }

}
