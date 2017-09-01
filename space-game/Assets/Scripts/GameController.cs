using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int maxCometCount = 50;
    private PlayerController player;

    public float CMinYOffset = 0.0f;
    public float CMaxYOffset = 4.0f;

    [Range(0.0f, 1.0f)]
    public float bonusChance = 0.1f;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C))
        {
            MakeItRain();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayTestSound();
        }

        float chance = Random.Range(0.0f, 1.0f);

        if (chance > 1.0f - bonusChance)
        {
            GiveBonus();
        }
	}

    private void PlayTestSound()
    {
        SoundController sc = FindObjectOfType<SoundController>();

        sc.PlaySoundByName("explosion1", true);
    }

    public void MakeItRain()
    {
        int rainCount = Random.Range((int)(maxCometCount * 0.5f), maxCometCount);

        List<GameObject> comets = this.GetComponent<PoolController>().GetComets(rainCount);

        Debug.Log("Comets: wanted " + rainCount + " got: " + comets.Count);

        int dir = Random.Range(0, 1);

        if (dir == 0) dir = -1;
        else dir = 1;

        foreach(GameObject go in comets)
        {
            // Activate
            go.SetActive(true);
            // Set position

            float playerYPos = player.transform.position.y;
            float yOffset = Random.Range(CMinYOffset, CMaxYOffset);

            float xOffset = yOffset * 0.5f;

            go.transform.position = player.transform.position + new Vector3(-1 * player.transform.position.x - xOffset, yOffset);            

            go.GetComponent<Rigidbody2D>().AddForce(go.transform.right * 1.0f * Random.Range(500.0f, 750.0f), ForceMode2D.Force);
            go.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 100.0f);

            go.transform.Rotate(Vector3.forward, Random.Range(-30.0f, 0.0f));

            go.GetComponent<Comet>().Deactivate();
        }

    }

    public void GiveBonus()
    {

        GameObject bonus = this.GetComponent<PoolController>().GetBonus();

        if (bonus != null)
        {

            bonus.SetActive(true);

            BonusDefinition def = FindObjectOfType<XMLReader>().GetRandomDefinition();

            bonus.GetComponent<Bonus>().Init(def, player);
        }

    }
}
