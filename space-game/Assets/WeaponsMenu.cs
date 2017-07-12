using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour {

    // Animation controller
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Open()
    {
        anim.SetBool("isOpen", true);
    }

    public void Close()
    {
        anim.SetBool("isOpen", false);
    }
}
