using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundController : MonoBehaviour {

    private Dictionary<string, Sound> sounds;

	// Use this for initialization
	void Start () {
		

	}

    public void Init()
    {
        // Populate sounds

        sounds = new Dictionary<string, Sound>();
        XMLReader xr = FindObjectOfType<XMLReader>();

        foreach (SoundDefinition s_definition in xr.sounds.Sounds)
        {
            Sound sound = new Sound();

            sound.Init(s_definition);

            sounds.Add(s_definition.SoundName, sound);
        }
    }
	
	public Sound PlaySoundByName(string name, bool playLocal)
    {
        Sound sound;

        sounds.TryGetValue(name, out sound);

        if (playLocal == true && sound != null)
        {
            PlaySound(sound);
        }
        else if (sound == null)
        {
            Debug.Log("Sound: " + name + " not found in SoundController.PlaySoundByName");
        }

        return sound;

    }

    public void PlaySound(Sound sound)
    {
        Debug.Log("Playing " + sound.definition.SoundName);
        AudioSource source = GetComponent<AudioSource>();

        source.clip = sound.clip;

        source.Play();
    }
}
