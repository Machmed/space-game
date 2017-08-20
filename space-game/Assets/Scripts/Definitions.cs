using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

public class Definitions {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public class WeaponDefinition
{
    [XmlAttribute("name")]
    public string WeaponName;
    public string WeaponType;
    public string AmmoType;
    public int OneShotAmmoNeed;
    public float CooldownTime;
    public float Force;

    public override string ToString()
    {
        return WeaponName + " / " + WeaponType.ToString() + " / " + AmmoType.ToString() + " / " + OneShotAmmoNeed.ToString() + " / " + CooldownTime.ToString() + " / " + Force.ToString();
    }
}

[XmlRoot("WeaponsCollection")]
public class WeaponContainer
{
    [XmlArray("Weapons")]
    [XmlArrayItem("WeaponDefinition")]
    public List<WeaponDefinition> Weapons = new List<WeaponDefinition>();
}

