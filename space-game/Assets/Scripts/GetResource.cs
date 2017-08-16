using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResource : MonoBehaviour {

    public static GameObject ball;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static GameObject GetResByTag(string tag)
    {
        if (tag == "Ball")
        {
            return ball;
        }

        return null;
    }
}
