using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public Vector3 currentVelocity = Vector3.zero;
    public float smoothTime = 0.1f;
    public float yOffset = 10.0f;
    Vector3 lastPlayerPosition;

    public float playerSpeed;
    public float playerMaxSpeed = 25.0f;

    [Range(-1.0f, -15.0f)]
    public float minCameraRange = -13.0f;

    [Range(-2.0f, -20.0f)]
    public float maxCameraRange = -9.0f;

    [Range(0.0f, 2.0f)]
    public float cameraZoomStep = 0.3f;
    
    [Range(0.0f, 20.0f)]
    public float speedRange = 0.3f;

    [Range(0.0f, 1.0f)]
    public float speedFactor = 0.3f;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void LateUpdate()
    {
        float zPos = this.transform.position.z;       

        // Player max speed
        playerMaxSpeed = 50.0f;

        playerSpeed = player.GetComponent<Rigidbody2D>().velocity.y;

        speedFactor = playerSpeed / playerMaxSpeed;

        speedFactor = Mathf.Clamp(speedFactor, 0.5f, 0.9f);

        if (playerSpeed > speedRange)
        {
            zPos -= cameraZoomStep * speedFactor;
            //zPos -= cameraZoomStep;
        }
        else
        {
            zPos += cameraZoomStep * speedFactor;
            //zPos += cameraZoomStep;
        }

        zPos = Mathf.Clamp(zPos, minCameraRange, maxCameraRange);

        Vector3 xyPosition = Vector3.SmoothDamp(this.transform.position, new Vector3(0.0f, player.transform.position.y + yOffset, 0.0f),
            ref currentVelocity, smoothTime);

        xyPosition = xyPosition + new Vector3(0.0f, 0.0f, zPos);

        this.transform.position = xyPosition;

    }

    public void Restart()
    {
        this.gameObject.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
    }
}
