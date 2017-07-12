using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour {

    public float effectRange = 5.0f;
    public float force = 50.0f;

	// Use this for initialization
	void Start () {
		// Get reference to ball
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        float distance = Vector3.Distance(this.gameObject.transform.position, ball.transform.position);

        if (distance < effectRange)
        {
            // Calculate force based on distance. <linear for now>
            float force_factor = distance / effectRange;

            // Calculate direction

            int direction = 1; // Right

            if (this.transform.position.x < ball.transform.position.x)
            {
                direction = 1; // Right
            }
            else
            {
                direction = -1; // Left
            }

            // Apply force to ball.
            ball.GetComponent<Rigidbody2D>().AddForce(Vector3.right * direction * force * force_factor, ForceMode2D.Impulse);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
