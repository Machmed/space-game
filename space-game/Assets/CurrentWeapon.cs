using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour {

    public Weapon currentWeapon;

    private Image weaponSlot;

    void Start()
    {
        weaponSlot = GetComponentInChildren<Image>();
    }

    // TODO Change it to map automaticly
    public Sprite[] weaponsVisuals;

	public void SwitchWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        ChangeVisuals();
    }

    private void ChangeVisuals()
    {
        foreach (Sprite go in weaponsVisuals)
        {
            if (currentWeapon.ToString() == go.name)
            {
                weaponSlot.sprite = Instantiate(go);
            }
        }
    }
}
