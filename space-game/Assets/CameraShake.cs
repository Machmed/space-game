using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float duration = 2.0f;
    public float strength = 2.0f;
    private bool shouldResetPosition = false;
    private Vector3 velocity = Vector3.zero;
    
    void Update()
    {
        if (shouldResetPosition)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.position,  new Vector3(0.0f, 0.0f, -1.0f), ref velocity, 0.0f);
        }

    }

    public void InitShake(float _strength, float _duration)
    {
        strength = _strength;
        duration = _duration;
        StartCoroutine("Shake");
        Debug.Log("Shake");
    }

    IEnumerator Shake()
    {
        shouldResetPosition = false;

        Debug.Log("Shake" + " start");
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            this.transform.localPosition += new Vector3(Mathf.Sin(Time.realtimeSinceStartup * strength * 300.0f) * strength, 0, 0);
            yield return null;
        }

        shouldResetPosition = true;
        
    }
}
