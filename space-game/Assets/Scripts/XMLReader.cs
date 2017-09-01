using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class XMLReader : MonoBehaviour {

    WeaponContainer container;
    BonusContainer bonusContainer;
    public SoundContainer sounds;

	// Use this for initialization
	void Start () {
        var serializer = new XmlSerializer(typeof(WeaponContainer));
        string path = Application.dataPath + "/Resources/Definitions/Definitions.xml";
        var stream = new FileStream(path, FileMode.Open);

        container = serializer.Deserialize(stream) as WeaponContainer;

        Debug.Log("Loaded " + container.Weapons.Count.ToString() + " weapon definitions.");

        foreach (WeaponDefinition wd in container.Weapons)
        {
            Debug.Log(wd.ToString());
        }

        stream.Close();


        serializer = new XmlSerializer(typeof(BonusContainer));
        path = Application.dataPath + "/Resources/Definitions/Definitions.xml";
        stream = new FileStream(path, FileMode.Open);

        bonusContainer = serializer.Deserialize(stream) as BonusContainer;

        Debug.Log("Loaded " + bonusContainer.Bonuses.Count.ToString() + " bonuses.");

        foreach (BonusDefinition wd in bonusContainer.Bonuses)
        {
            Debug.Log(wd.ToString());
        }

        stream.Close();

        serializer = new XmlSerializer(typeof(SoundContainer));
        path = Application.dataPath + "/Resources/Definitions/Definitions.xml";
        stream = new FileStream(path, FileMode.Open);

        sounds = serializer.Deserialize(stream) as SoundContainer;

        Debug.Log("Loaded " + sounds.Sounds.Count.ToString() + " sounds");

        SoundDefinition sd = sounds.Sounds[1];

        Debug.Log("S: Name: " + sd.SoundName + " path: " + sd.Path + " type: " + sd.Type);

        stream.Close();

        FindObjectOfType<SoundController>().Init();
	}
	
	public WeaponDefinition GetWeaponDefinition(WeaponType weapon)
    {
        return (from u in container.Weapons where u.WeaponType == weapon.ToString() select u).FirstOrDefault();
    }

    public BonusDefinition GetRandomDefinition()
    {
        int max = bonusContainer.Bonuses.Count;

        int index = Random.Range(0, max);

        return bonusContainer.Bonuses[index];
    }

}
