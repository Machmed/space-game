using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound {

    public SoundDefinition definition;

    public AudioClip clip;

    public void Init(SoundDefinition _definition)
    {
        definition = _definition;
        clip = Resources.Load<AudioClip>(definition.Path);
    }
}
