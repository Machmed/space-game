using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Linq;

public class WeaponsMenu : MonoBehaviour {

    // Animation controller
    Animator anim;

    // Varriables
    bool isOpen = false;

    // Player
    WeaponController playerWeapons;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        playerWeapons = FindObjectOfType<WeaponController>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfSwitchToWeapon();
	}

    public void Open()
    {
        Debug.Log("Weapons Menu Open");
        anim.SetBool("isOpen", true);
        isOpen = true;
    }

    public void Close()
    {

        Debug.Log("Weapons Menu Close");
        anim.SetBool("isOpen", false);
        isOpen = false;
    }

    private void CheckIfSwitchToWeapon()
    {
        List<RaycastResult> result = RaycastMouse();

        Debug.Log(result.Count);

        if (result != null)
        {
            RaycastResult slot = result.Select(n => n).Where(s => s.gameObject.GetComponent<WeaponSlot>() != null).FirstOrDefault();

            if (slot.gameObject != null && slot.gameObject.GetComponent<WeaponSlot>())
            {
                playerWeapons.SwitchWeapon(slot.gameObject.GetComponent<WeaponSlot>().weapon);
            }               
        }
    }

    private List<RaycastResult> RaycastMouse()
    {

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };

        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        Debug.Log(results.Count);

        return results;
    }
}
