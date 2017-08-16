using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon {

	public void Initialize()
    {
        weaponType = WeaponType.RocketLauncher;
        weaponName = "Rocket Launcher";
        ammoType = AmmunitionType.MiniRocket;
    }
}
