using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPlayer : MonoBehaviour {

    public string SoundName;

    public void PlaySound()
    {
        Sound sound = FindObjectOfType<SoundController>().PlaySoundByName(SoundName, false);
        AudioSource a_source = GetComponent<AudioSource>();
        a_source.clip = sound.clip;
        a_source.Play();
    }

}
