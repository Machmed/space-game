using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    public float health = 1000.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colision: " + other.collider.gameObject.name);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colision: " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<PlayerController>().GetHit(100);
            GetComponent<Animation>().Play();
            GameObject.FindObjectOfType<PlayerController>().Shake(this.transform.localScale);
        }

        if (other.gameObject.GetComponent<ProjectileController>())
        {
            float scale = Random.Range(0.7f, 1.3f);
            this.transform.localScale = new Vector2(scale, scale);

            health -= 100.0f;
            if (health <= 0.0f)
            {
                Deactivate();
            }
        }
    }

    public void Deactivate()
    {
        GameObject particle = Instantiate(Resources.Load<GameObject>("ExplosionEffect"), this.transform.position, Quaternion.identity);
        particle.GetComponent<FXPlayer>().SoundName = "explosion1";
        particle.GetComponent<FXPlayer>().PlaySound();

        this.health = 1000.0f;
        this.gameObject.SetActive(false);
    }
}
