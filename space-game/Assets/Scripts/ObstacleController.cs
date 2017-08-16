using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

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
        if (other.gameObject.tag == "Ball")
        {
            GameObject.FindObjectOfType<PlayerController>().HP -= 100;
            GetComponent<Animation>().Play();
            GameObject.FindObjectOfType<PlayerController>().Shake(this.transform.localScale);
        }
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
