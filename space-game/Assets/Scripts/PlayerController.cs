using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

    [Range(0.0f, 100.0f)]
    public float playerSpeed = 5.0f;

    [Range(0.0f, 100.0f)]
    public float basePlayerSpeed = 16.0f;


    public int HP = 1000;
    public int maxHP = 1000;

    public GameObject holder;
    public GameObject ball;
    public bool isShooting = true;
    public bool startedShooting = false;
    private Vector2 startingPosition;
    private Vector2 endingPosition;

    private bool canPlay = true;

    public float maxForce = 100.0f;

    // Effect
    public GameObject forceEffect;

    Vector2 target;

    // UI
    public UIController ui;

    public int score;



	// Use this for initialization
	void Start () {
        Initialize();
	}

    public void GetHit(int force)
    {
        UpdateHP(-1 * force);

        Instantiate(forceEffect, this.transform.position, Quaternion.identity);

    }

    public void UpdateHP(int value)
    {
        Debug.Log("Update hp for: " + value);
        if (value < 0)
        {
            RemoveHealth(value);           
        }
        else if (value > 0)
        {
            AddHealth(value);
        }
    }

    private void AddHealth(int value)
    {
        if (HP < maxHP + value)
            HP += value;
    }

    private void RemoveHealth(int value)
    {
        if (HP + value >= 0)
            HP += value;
        else
            HP = 0;
    }

    void UpdateScore()
    {
        score = (int)this.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {

        if (canPlay)
        {

            CheckHP();
            UpdateScore();

            /*
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
            */

            // Controlls

            if (Input.GetKey(KeyCode.Space))
            {
                foreach (Weapon weapon in GetComponents<Weapon>())
                {
                    weapon.Use();
                }
            }

            if (Input.GetMouseButton(0))
            {
                //first we get the ray represented by the mouse click. because its a perspective camera, 
                //the direction will change depending on where you click
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //the ray origin is the position in world space on the 'front' plane of the camera. however
                //for your game we need to go forwards to the plane of your objects. I am going to assume
                //that is z==0 for now, so...
                float z_plane_of_2d_game = 0;
                Vector3 pos_at_z_0 = ray.origin + ray.direction * (z_plane_of_2d_game - ray.origin.z) / ray.direction.z;

                Vector2 point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);

                Vector2 playerPosition = (Vector2)this.transform.position;

                float distance = Vector3.Distance(point, playerPosition);


                if (distance > 0.5f)
                {
                    target = point;
                }

            }

            // Testing

            if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKey(KeyCode.Q) == false)
            {
                GetComponent<WeaponController>().AddWeapon(WeaponType.Minigun);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.Q) == false)
            {
                GetComponent<WeaponController>().AddWeapon(WeaponType.RocketLauncher);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKey(KeyCode.Q))
            {
                GetComponent<WeaponController>().RemoveWeapon(WeaponType.Minigun);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.Q))
            {
                GetComponent<WeaponController>().RemoveWeapon(WeaponType.RocketLauncher);
            }


            CheckWaypoint();
            GoToTarget();

        }
       
	}

    public void CheckHP()
    {
        if (HP <= 0)
        {
            ui.GameOver();

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            canPlay = false;
        }
    }

    public void CheckWaypoint()
    {
        Vector2 playerPosition = (Vector2)this.transform.position;

        float distance = Vector3.Distance(playerPosition, target);

        if (distance < 0.5f)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, basePlayerSpeed);
            target = Vector2.zero;
        }
    }
    
    public void GoToTarget()
    {
        if (target != Vector2.zero)
        {
        Vector2 playerPosition = (Vector2)this.transform.position;

        float distance = Vector3.Distance(playerPosition, target);

        Vector2 dir = target - playerPosition;

        dir = dir.normalized * distance * playerSpeed;

        dir.y = Mathf.Clamp(dir.y, basePlayerSpeed, dir.y);  

        this.GetComponent<Rigidbody2D>().velocity = dir;
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
        //ball.transform.position = holder.transform.position;
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        isShooting = true;
        startedShooting = false;
        startingPosition = Vector2.zero;
        endingPosition = Vector2.zero;
        HP = 1000;

        ui = FindObjectOfType<UIController>();

        //this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * maxForce, ForceMode2D.Force);

    }
}
