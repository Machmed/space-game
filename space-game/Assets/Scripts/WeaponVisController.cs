using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisController : MonoBehaviour {

    public Dictionary<WeaponType, WeaponSlotInstance> slots;

    void Start()
    {
        slots = new Dictionary<WeaponType, WeaponSlotInstance>();
    }

	public void AddWeapon(WeaponType weapon)
    {
        GameObject itemVis = (GameObject)Resources.Load("WeaponSlot");
        Debug.Log("Loading weapon slot. " + itemVis.ToString());
        itemVis = Instantiate(itemVis);
        itemVis.transform.SetParent(this.gameObject.transform);
        itemVis.GetComponent<WeaponSlotInstance>().Inititialize(weapon);
        slots.Add(weapon, itemVis.GetComponent<WeaponSlotInstance>());
    }

    public void RemoveWeapon(WeaponType weapon)
    {
        Debug.Log("Try to remove weapon: " + weapon.ToString());
        Debug.Log("Found weapons: ");

        foreach (var item in slots.Keys)
        {
            Debug.Log(item.ToString());
        }

        WeaponSlotInstance instance = slots[weapon];

        slots.Remove(weapon);
        Destroy(instance.gameObject);
    }
}
