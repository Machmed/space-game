using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BackGroundController : MonoBehaviour {

    // stars
    public GameObject star;
    public List<GameObject> stars;
    public int maxStarsCount = 50;
    public float xRange = 10.0f;
    public float yRange = 5.0f;
    public int StarsCount = 10;

    // planets

    public GameObject planet;
    public List<GameObject> planets;
    public float maxScale = 0.5f;
    public float minScale = 0.3f;

    public int maxPlanetCount = 10;
    public int planetCount = 2;

	// Use this for initialization
	void Start () {

        stars = new List<GameObject>();

        for (int i = 0; i < maxStarsCount; i++)
        {
            GameObject _star = Instantiate(star) as GameObject;
            _star.SetActive(false);
            _star.transform.parent = this.transform;
            stars.Add(_star);
        }

        for (int i = 0; i < maxPlanetCount; i++)
        {
            GameObject _planet = Instantiate(planet) as GameObject;
            _planet.SetActive(false);
            _planet.transform.parent = this.transform;
            planets.Add(_planet);
        }

        //ActivateStars(StarsCount, -yRange, yRange);
        ActivateObjects(StarsCount, -yRange, yRange, stars);
        ActivateObjects(planetCount, -yRange, yRange, planets);
	}

    public void ActivateStars(int count, float minY, float maxY)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _unactiveStar = GetStar();
            _unactiveStar.transform.position = new Vector3(Random.Range(-xRange, xRange), Random.Range(minY, maxY), 0.0f);
            _unactiveStar.SetActive(true);
            float scale = Random.Range(0.7f, 1.5f);
            _unactiveStar.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void ActivateObjects(int count, float minY, float maxY, List<GameObject> objects)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _unactiveObject = GetObject(objects);
            _unactiveObject.transform.position = new Vector3(Random.Range(-xRange, xRange), Random.Range(minY, maxY), 0.0f);
            _unactiveObject.SetActive(true);
            float scale = Random.Range(minScale, maxScale);
            _unactiveObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public GameObject GetObject(List<GameObject> type)
    {
        return type.First(p => !p.activeInHierarchy);
    }

    public GameObject GetStar()
    {
        foreach (GameObject item in stars)
        {
            if (!item.activeInHierarchy)
            {
                return item;
            }
        }

        return null;
    }

    public int GetActiveStarsCount()
    {
        return stars.Where(p => p.activeInHierarchy).Count();
    }

    public int GetActiveObjectsCount(List<GameObject> objects)
    {
        return objects.Where(p => p.activeInHierarchy).Count();
    }
	
	// Update is called once per frame
	void Update () {
		// Each frame 
        // First get all active star which y position is less than camera min y range

        foreach (GameObject star in stars)
        {
            if (star.activeInHierarchy && star.transform.position.y < Camera.main.transform.position.y - yRange - 2.0f)
            {
                // deactivate star
                star.SetActive(false);
            }
        }

        foreach(GameObject planet in planets)
        {
            if (planet.activeInHierarchy && planet.transform.position.y < Camera.main.transform.position.y - yRange - 2.0f)
            {
                // deactivate star
                planet.SetActive(false);
            }
        }

        ActivateObjects(StarsCount - GetActiveObjectsCount(stars), Camera.main.transform.position.y + yRange, Camera.main.transform.position.y + (yRange * 3), stars);
        ActivateObjects(planetCount - GetActiveObjectsCount(planets), Camera.main.transform.position.y + yRange, Camera.main.transform.position.y + (yRange * 3), planets);
	}
}
