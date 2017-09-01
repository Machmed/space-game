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

[XmlRoot("Collection")]
public class WeaponContainer
{
    [XmlArray("Weapons")]
    [XmlArrayItem("WeaponDefinition")]
    public List<WeaponDefinition> Weapons = new List<WeaponDefinition>();
}

public class BonusDefinition
{
    [XmlAttribute("name")]
    public string BonusName;
    public string BonusType;

    public float Red;
    public float Green;
    public float Blue;

    [XmlArray("Functionalities")]
    [XmlArrayItem("Functionality")] 
    public Functionality[] Functionalities;
    

    public override string ToString()
    {
        return BonusName + " / " + BonusType + "Functionalities: " + Functionalities.Length;
    }
}

public class Functionality
{
    public string Method;
    public string Value;
}

[XmlRoot("Collection")]
public class BonusContainer
{
    [XmlArray("Bonuses")]
    [XmlArrayItem("BonusDefinition")]
    public List<BonusDefinition> Bonuses = new List<BonusDefinition>();
}

[XmlRoot("Collection")]
public class SoundContainer
{
    [XmlArray("Sounds")]
    [XmlArrayItem("SoundDefinition")]
    public List<SoundDefinition> Sounds = new List<SoundDefinition>();
}

public class SoundDefinition
{
    [XmlAttribute("name")]
    public string SoundName;
    public string Type;
    public string Path;

}
