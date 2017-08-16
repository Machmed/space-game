using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour {

    private Dictionary<AmmunitionType, int> ammunition;
    private AmmoVisManager ammoManager;
    private Dictionary<AmmunitionType, GameObject> projectiles;

	// Use this for initialization
	void Start () {
        ammunition = new Dictionary<AmmunitionType, int>();
        projectiles = new Dictionary<AmmunitionType, GameObject>();
        var values = AmmunitionType.GetValues(typeof(AmmunitionType));

        foreach (AmmunitionType value in values)
        {
            ammunition.Add(value, 1000);
            GameObject projectileGO = (GameObject)Resources.Load(value.ToString());
            projectiles.Add(value, projectileGO);
            Debug.Log("Loaded: " + value.ToString() + " path: " + value.ToString());
            Debug.Log(" object name: " + projectileGO.name);

        }

        ammoManager = GameObject.FindObjectOfType<AmmoVisManager>();
        ammoManager.InitAmmoCount(ammunition);

	}

    public GameObject GetProjectlie(AmmunitionType type)
    {
        return projectiles[type];
    }

    public bool RemoveAmmo(AmmunitionType type, int? count = null)
    {
        if (count == null) count = 1;

        if (ammunition[type] >= count)
        {
            ammunition[type] = ammunition[type] - count.GetValueOrDefault();
            return true;
        }
        else
        {
            return false;
        }

    }
	
	// Update is called once per frame
	void Update () {

        ammoManager.UpdateAmmoCount(ammunition);
	}
}
