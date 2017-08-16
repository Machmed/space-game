using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject ball;
    public Vector3 currentVelocity = Vector3.zero;
    public float smoothTime = 0.1f;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(0.0f, ball.transform.position.y, 0.0f),
            ref currentVelocity, smoothTime);

        //if (ball.transform.position.y > 0)
        //{
        //    this.gameObject.transform.position = new Vector3(0.0f, ball.transform.position.y, this.transform.position.z);
        //}
	}

    public void Restart()
    {
        this.gameObject.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
    }
}
