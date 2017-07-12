using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public string projectileName;

	public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
