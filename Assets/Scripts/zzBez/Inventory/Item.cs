using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attribute {
    Weapon,
    Armor,
    Damage,
    Defence,
    Health,
    Object,
    Metal,
    None
}

public class Item : MonoBehaviour {

    // Objects ID, we'll use this for specific items such as in our weaponManager to identify weapons we're trying to equip
    public int ID;

    public string description;
    public Sprite icon;
    public bool pickedUp;
    public bool playersWeapon; 
    public Attribute itemAttribute;

    [HideInInspector] public bool equipped; 
    [HideInInspector] public GameObject weapon; 

    // Define our weaponManager gameobject which will handle our potions... Wait no, our weapons
    [HideInInspector] public GameObject weaponManager;

    public void Start() {
        // Devine our weaponManager
        weaponManager = GameObject.FindWithTag("WeaponManager");
        // If it's not the players weapon
        if (!playersWeapon) { 
            // Make allWeapons equal to our weaponsManager child count
            int allWeapons = weaponManager.transform.childCount;
            // Go through allWeapons inside our manager
            for (int i = 0; i < allWeapons; i++) {  
                // Check if it's the same ID as this gameobject
                if (weaponManager.transform.GetChild(i).gameObject.GetComponent <Item>().ID == ID) {
                    // Set weapon to the weaponManagers childs transform
                    weapon = weaponManager.transform.GetChild(i).gameObject;
                    print(weapon.name);
                }
            }
        }
    }

    public void Update() {
        if (equipped) { 
            if (Input.GetKeyDown(KeyCode.G))
                equipped = false;

            if (equipped == false)
                this.gameObject.SetActive(false);           
        }
    }
    public void ItemUsage() {
        if (itemAttribute == Attribute.Weapon) {
            weapon.SetActive(true);
            // Set this specific items equipped true
            weapon.GetComponent<Item>().equipped = true;
        }
    }
}
