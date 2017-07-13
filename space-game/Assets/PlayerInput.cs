using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    WeaponsMenu weaponsMenu;

	// Use this for initialization
	void Start () {
        weaponsMenu = FindObjectOfType<WeaponsMenu>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            weaponsMenu.Open();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            weaponsMenu.Close();
        }
	}
}
