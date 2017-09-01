using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolController : MonoBehaviour {

    public int projectilesCount = 200;
    public int cometCount = 200;
    public int bonusesCount = 10;
    Dictionary<AmmunitionType, List<GameObject>> projectiles;
    List<GameObject> comet;
    List<GameObject> bonuses;

    public PlayerController pc;

	// Use this for initialization
	void Start () {
        pc = FindObjectOfType<PlayerController>();

	    // Create pool for projectiles
        CreateProjectilesPool();

        //Create comet pool

        CreateCometPool();

        //Create bonuses pool
        CreateBonusesPool();
        
	}

    private void CreateCometPool()
    {
        comet = new List<GameObject>();

        GameObject template = Resources.Load("Comet") as GameObject;

        for (int i = 0; i < cometCount; i++)
        {
            GameObject go = Instantiate(template);

            comet.Add(go);

            go.SetActive(false);
            go.transform.SetParent(this.transform);

        }
    }

    private void CreateBonusesPool()
    {
        bonuses = new List<GameObject>();

        GameObject template = Resources.Load("Bonus") as GameObject;

        for (int i = 0; i < bonusesCount; i++)
        {
            GameObject go = Instantiate(template);

            bonuses.Add(go);

            go.SetActive(false);
            go.transform.SetParent(this.transform);

        }
    }

    private void CreateProjectilesPool()
    {
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

                instance.GetComponent<ProjectileController>().player = pc; 

            }

            projectiles.Add(value, projectilesList);
        }
    }

    public GameObject GetProjectile(AmmunitionType type)
    {
        GameObject go = (from u in projectiles[type] where u.activeInHierarchy == false select u).FirstOrDefault();
        return go;
    }

    public GameObject GetComet()
    {
        GameObject go = (from u in comet where u.activeInHierarchy == false select u).FirstOrDefault();
        return go;
    }

    public GameObject GetBonus()
    {
        GameObject go = (from u in bonuses where u.activeInHierarchy == false select u).FirstOrDefault();
        return go;
    }

    public List<GameObject> GetComets(int count)
    {
        // List<GameObject> go = (from u in comet where u.activeInHierarchy == false select u).Take(count).ToList();\

        List<GameObject> go = comet.Select(s => s).Where(s => s.activeInHierarchy == false).Take(count).ToList();

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
