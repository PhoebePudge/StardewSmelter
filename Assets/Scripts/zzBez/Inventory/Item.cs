using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    // Objects ID, we'll use this for specific items such as, it'll be used in our weaponManager to identify weapons we're trying to equip
    public int ID;
    // Type: weapon, potion ect
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;
    public bool playersWeapon;

    [HideInInspector]
    public bool equipped;
    
    [HideInInspector]
    public GameObject weapon;

    [HideInInspector]
    public GameObject weaponManager;

    public void Start()
    {
        weaponManager = GameObject.FindWithTag("WeaponManager");
        // If it's not the players weapon
        if (!playersWeapon)
        {
            int allWeapons = weaponManager.transform.childCount;
            for (int i = 0; i < allWeapons; i++)
            {
                if (weaponManager.transform.GetChild(i).gameObject.GetComponent <Item>().ID == ID)
                {
                    weapon = weaponManager.transform.GetChild(i).gameObject;
                    print(weapon.name);
                }
            }
        }
    }

    public void Update()
    {
        if (equipped)
        {


            if (Input.GetKeyDown(KeyCode.G))
                equipped = false;

            if (equipped == false)
                this.gameObject.SetActive(false);           
        }
    }
    public void ItemUsage()
    {
        if (type == "Weapon")
        {
            weapon.SetActive(true);
            weapon.GetComponent<Item>().equipped = true;
        }
    }
}
