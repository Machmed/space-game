using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    PlayerController player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		// Setup UI of player specs
        SetupUI();
        
	}

    public void SetupUI()
    {
        // HP
        Image hpimg = ((GameObject)GameObject.FindGameObjectWithTag("UIHPIMG")).GetComponent<Image>();
        float hpPercent = (float)((float)player.HP / (float)player.maxHP);
        hpimg.fillAmount = hpPercent;

        // HP text
        Text hpText = GameObject.FindGameObjectWithTag("UIHPPERC").GetComponent<Text>();
        hpText.text = ((int)(hpPercent * 100)).ToString() + " %";
    }
}
