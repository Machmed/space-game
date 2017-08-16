using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {

    public float time = 1.0f;

	// Use this for initialization
	void Start () {
        Invoke("DestroyEffect", time);
	}

    public void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
