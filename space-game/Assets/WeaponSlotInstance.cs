using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotInstance : MonoBehaviour
{

    public WeaponType weaponType;


    public void Inititialize(WeaponType weapon)
    {
        weaponType = weapon;

        var components = GetComponentsInChildren<ComponentType>();

        foreach (ComponentType ct in components)
        {
            if (ct.componentName == "weaponImage")
            {
                Debug.Log("Loading sprite for: " + weapon.ToString());
                Sprite sp = Resources.Load<Sprite>(weapon.ToString());
                Image itemSlot = ct.gameObject.GetComponent<Image>();
                itemSlot.sprite = sp;
            }
        }

    }
}

