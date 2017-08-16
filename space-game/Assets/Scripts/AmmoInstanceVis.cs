using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoInstanceVis : MonoBehaviour {

    public AmmunitionType ammoType;
    private Text value;

    public void Init(string ammoName, int ammoCount)
    {
        SetText("ammoName", ammoName);
        SetText("ammoValue", ammoCount.ToString());
        SetupValue();
    }

    public void UpdateValue(int count)
    {
        value.text = count.ToString();
    }

    private void SetupValue()
    {
        ComponentType[] components = gameObject.GetComponentsInChildren<ComponentType>();

        foreach (var item in components)
        {
            if (item.componentName == "ammoValue")
            {
                value = item.gameObject.GetComponent<Text>();
            }
        }
    }

    private void SetText(string holder, string name)
    {
        ComponentType[] components = gameObject.GetComponentsInChildren<ComponentType>();

        foreach (var item in components)
        {
            if (item.componentName == holder)
            {
                item.gameObject.GetComponent<Text>().text = name;
            }
        }
    }
}
