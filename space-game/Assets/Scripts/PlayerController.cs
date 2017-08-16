using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

    [Range(0.0f, 100.0f)]
    public float platformSpeed = 5.0f;

    public int HP = 1000;
    public int maxHP = 1000;

    public GameObject holder;
    public GameObject ball;
    public bool isShooting = true;
    public bool startedShooting = false;
    private Vector2 startingPosition;
    private Vector2 endingPosition;

    public float maxForce = 100.0f;

    // Effect
    public GameObject forceEffect;



	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(8.0f, 0.0f, 0.0f), platformSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-8.0f, 0.0f, 0.0f), platformSpeed * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(0) && isShooting)
        {
            startedShooting = true;
            startingPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            // Get point where to instatinate effect
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0.0f;
            // Instatntinate effect in calculated position
            GameObject effect =  Instantiate(forceEffect, Camera.main.transform);
            effect.transform.position = position;
        }


        if (startedShooting && Input.GetMouseButtonUp(0))
        {
            endingPosition = Input.mousePosition;
            float strngth = Vector2.Distance(startingPosition, endingPosition);
            Debug.Log("Strength:" + strngth);
            isShooting = false;
            float forceFactor = Vector2.Distance(new Vector2(0.0f, 0.0f), new Vector2(Screen.width, Screen.height));
            float force = maxForce * strngth / forceFactor;
            ball.GetComponent<BallController>().AddForce(endingPosition, force);
            startedShooting = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.FindObjectOfType<CameraController>().Restart();
            Initialize();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            foreach (Weapon weapon in GetComponents<Weapon>())
            {
                weapon.Use();
            }
        }

	}    

    public void Shake(Vector3 scale)
    {
        float strength = scale.magnitude / 3.0f;
        float duration = scale.normalized.magnitude / 2.0f;
        GameObject.FindObjectOfType<CameraShake>().InitShake(strength, duration);
    }

    public void Initialize()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ball.transform.position = holder.transform.position;
        ball.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        ball.transform.SetParent(holder.transform);
        isShooting = true;
        startedShooting = false;
        startingPosition = Vector2.zero;
        endingPosition = Vector2.zero;
        ball.GetComponent<BallController>().ResetForce();
        HP = 1000;
    }
}
