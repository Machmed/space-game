using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoVisManager : MonoBehaviour {

    public GameObject AmmoInstancePrefab; // TODO load this from resources
    private Dictionary<AmmunitionType, AmmoInstanceVis> ammoVis;

	public void InitAmmoCount(Dictionary<AmmunitionType, int> values)
    {
        ammoVis = new Dictionary<AmmunitionType, AmmoInstanceVis>();

        foreach (AmmunitionType key in values.Keys)
        {
            GameObject instance = Instantiate(AmmoInstancePrefab);
            instance.transform.SetParent(this.transform);
            instance.transform.localPosition = Vector2.zero;
            AmmoInstanceVis vis = instance.GetComponent<AmmoInstanceVis>();
            vis.Init(key.ToString(), values[key]);
            vis.ammoType = key;
            ammoVis.Add(key, vis);
        }
    }

    public void UpdateAmmoCount(Dictionary<AmmunitionType, int> values)
    {
        foreach (AmmunitionType key in values.Keys)
        {
            AmmoInstanceVis vis = ammoVis[key];
            vis.UpdateValue(values[key]);
        }
    }
}
