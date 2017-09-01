using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    public BonusDefinition definition;

    public PlayerController player;

    public string value;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (player == null)
                player = other.gameObject.GetComponent<PlayerController>();

            foreach (var functionality in definition.Functionalities)
            {
                value = functionality.Value;
                Invoke(functionality.Method, 0.0f);
            }

            Deactivate();
        }

    }

    public void Init(BonusDefinition _definition, PlayerController player)
    {
        Invoke("Deactivate", UnityEngine.Random.Range(5.0f, 10.0f));

        Debug.Log("Init: " + _definition.ToString());

        definition = _definition;

        float posX = player.transform.position.x - UnityEngine.Random.Range(-5.0f, 5.0f);

        float posY = player.transform.position.y + 10.0f - UnityEngine.Random.Range(-2.0f, 2.0f);

        this.transform.position = new Vector2(posX, posY);

        if (this.transform.position.x < player.transform.position.x)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, UnityEngine.Random.Range(-0.5f, 0.5f));
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-2.0f, UnityEngine.Random.Range(-0.5f, 0.5f));
        }

        // Change Color

        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

        sr.color = new Color(_definition.Red, _definition.Green, _definition.Blue);
    }

	public void Activate()
    {
        foreach (Functionality funct in definition.Functionalities)
        {
            value = funct.Value;
            Invoke(funct.Method, 0.0f);
        }
    }

    public void Deactivate()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        this.gameObject.SetActive(false);
    }

    // Functionalities

    void AddHealth()
    {
        Debug.Log("AddHealth invoked with: " + value + " value");

        player.UpdateHP(Int32.Parse(value));
    }

    void AddAmmo()
    {

        AmmunitionType ammoType = AmmunitionType.Bullet9mm;

        foreach (AmmunitionType type in AmmunitionType.GetValues(typeof(AmmunitionType)))
        {
            if (type.ToString() == value)
            {
                ammoType = type;
            }
        }

        player.gameObject.GetComponent<PlayerResources>().AddAmmo(ammoType, UnityEngine.Random.Range(1, 100));
    }
}
