using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class XMLReader : MonoBehaviour {

    WeaponContainer container;

	// Use this for initialization
	void Start () {
        var serializer = new XmlSerializer(typeof(WeaponContainer));
        string path = Application.dataPath + "/Resources/Definitions/Definitions.xml";
        var stream = new FileStream(path, FileMode.Open);
        Debug.Log(stream.Length);
        container = serializer.Deserialize(stream) as WeaponContainer;

        Debug.Log("Loaded " + container.Weapons.Count.ToString() + " weapon definitions.");

        foreach (WeaponDefinition wd in container.Weapons)
        {
            Debug.Log(wd.ToString());
        }

        stream.Close();
	}
	
	public WeaponDefinition GetWeaponDefinition(WeaponType weapon)
    {
        return (from u in container.Weapons where u.WeaponType == weapon.ToString() select u).FirstOrDefault();
    }

}
