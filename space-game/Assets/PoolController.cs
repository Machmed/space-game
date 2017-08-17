using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolController : MonoBehaviour {

    public int projectilesCount = 200;
    Dictionary<AmmunitionType, List<GameObject>> projectiles;

	// Use this for initialization
	void Start () {
	    // Create pool for projectiles

        projectiles = new Dictionary<AmmunitionType, List<GameObject>>();
 	
        var values = AmmunitionType.GetValues(typeof(AmmunitionType));

        foreach (AmmunitionType value in values)
        {
            // for each ammo type create list of bullets

            List<GameObject> projectilesList = new List<GameObject>();

            for (int i = 0; i < projectilesCount; i++)
            {
                GameObject template = Resources.Load<GameObject>(value.ToString());
                GameObject instance = Instantiate(template);
                instance.transform.SetParent(this.transform);
                instance.SetActive(false);
                projectilesList.Add(instance);
                
            }

            projectiles.Add(value, projectilesList);
        }
	}

    public GameObject GetProjectile(AmmunitionType type)
    {
        GameObject go = (from u in projectiles[type] where u.activeInHierarchy == false select u).FirstOrDefault();
        return go;
    }

    public GameObject GetProjectileAndActivate(AmmunitionType type)
    {
        GameObject go = GetProjectile(type);
        go.SetActive(true);
        go.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        go.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
        go.transform.rotation = Quaternion.identity;
        
        return go;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
