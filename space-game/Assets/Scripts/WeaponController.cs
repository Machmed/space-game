using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Dictionary<WeaponType, Weapon> currentWeapons;

    public void Start()
    {
        currentWeapons = new Dictionary<WeaponType, Weapon>();
    }

	public bool AddWeapon(WeaponType weapon)
    {
        Debug.Log("Add weapon of type: " + weapon.ToString());
        Weapon weaponInstance;
        bool hasWeapon = currentWeapons.TryGetValue(weapon, out weaponInstance);

        if (!hasWeapon)
        {
            // Get definition
            WeaponDefinition wd = GameObject.FindObjectOfType<XMLReader>().GetWeaponDefinition(weapon);
            if (wd != null)
            {
                weaponInstance = this.gameObject.AddComponent<Weapon>();
                weaponInstance.weaponType = weapon;
                weaponInstance.weaponName = wd.WeaponName;

                var values = AmmunitionType.GetValues(typeof(AmmunitionType));

                foreach (AmmunitionType ammo in values)
                {
                    if (ammo.ToString() == wd.AmmoType)
                    {
                        weaponInstance.ammoType = ammo;
                    }
                }
                weaponInstance.oneShotAmmoNeed = wd.OneShotAmmoNeed;
                weaponInstance.force = wd.Force;
                weaponInstance.cooldownTime = wd.CooldownTime;
                currentWeapons.Add(weapon, weaponInstance);

                return true;
            }
        }

        return false;

    }

    public void RemoveWeapon(WeaponType weapon)
    {
        Debug.Log("Remove weapon of type: " + weapon.ToString());
        Weapon weaponInstance;
        bool hasWeapon = currentWeapons.TryGetValue(weapon, out weaponInstance);

        if (hasWeapon)
        {
            Debug.Log("Found weapon of type to remove: " + weapon.ToString());
            weaponInstance.Die();
            currentWeapons.Remove(weapon);
            Destroy(weaponInstance);
        }
    }
}
