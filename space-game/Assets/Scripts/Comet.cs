using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour {

    public void Deactivate()
    {
        Invoke("DeactivateLater", 8.0f);
    }

    void DeactivateLater()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();

        rbody.velocity = Vector3.zero;
        rbody.angularVelocity = 0.0f;

        this.transform.rotation = Quaternion.identity;

        this.gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colision: " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<PlayerController>().GetHit(20);
            GameObject.FindObjectOfType<PlayerController>().Shake(this.transform.localScale * 0.2f);
        }
    }
}
