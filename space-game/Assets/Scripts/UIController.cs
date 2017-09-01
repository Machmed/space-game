using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour {

    PlayerController player;

    public GameObject gameOverUI;
    public GameObject scoreValue;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerController>();

        // (from u in projectiles[type] where u.activeInHierarchy == false select u).FirstOrDefault();
        scoreValue = (from u in FindObjectsOfType<ComponentType>() where u.componentName == "ScoreUI" select u).FirstOrDefault().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		// Setup UI of player specs
        SetupUI();
        
	}

    public void GameOver()
    {
        gameOverUI.SetActive(true);
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

        //scoreValue

        scoreValue.GetComponent<Text>().text = player.score.ToString();
    }
}
