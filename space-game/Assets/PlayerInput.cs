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
        if (Input.GetMouseButton(1))
        {
            weaponsMenu.Open();
        }
        else
        {
            weaponsMenu.Close();
        }
	}
}
