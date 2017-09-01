using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    AmmunitionType ammoType;
    public PlayerController player;

    public void Start()
    {
        /*
        Collider2D ballCollider = GameObject.FindGameObjectWithTag("Ball").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), ballCollider);
         * */

        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), playerCollider);
    }

	public void Init(AmmunitionType type, float strength)
    {
        ammoType = type;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * strength * Time.deltaTime * Mathf.Clamp(player.gameObject.GetComponent<Rigidbody2D>().velocity.y, 1.0f, 50.0f));

        Vector2 velocity = new Vector2(0.0f, player.GetComponent<Rigidbody2D>().velocity.y + (strength * Time.deltaTime));

        GetComponent<Rigidbody2D>().velocity = velocity;

        Invoke("Deactivate", 3.0f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Collidable>())
        {
            Debug.Log("Projectile has hit collidable, deactivating.");
            Deactivate();
        }
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
