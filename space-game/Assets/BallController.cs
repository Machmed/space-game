using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddForce(Vector3 targetPosition, float force)
    {
        Vector2 direction = gameObject.transform.position - Camera.main.ScreenToWorldPoint(targetPosition);
        this.gameObject.GetComponentInChildren<Rigidbody2D>().AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    public void ResetForce()
    {
        this.gameObject.GetComponentInChildren<Rigidbody2D>().velocity = Vector3.zero;
    }
}
