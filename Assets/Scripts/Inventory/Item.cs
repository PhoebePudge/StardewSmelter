using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Item : MonoBehaviour { 

    public ItemData itemdata; 

    public bool playersWeapon;
    public Sprite icon;
    public bool pickedUp;
    [HideInInspector] public bool equipped; 
    [HideInInspector] public GameObject weapon; 

    // Define our weaponManager gameobject which will handle our potions... Wait no, our weapons
    [HideInInspector] public GameObject weaponManager;

    public void Start() {  
        weaponManager = GameObject.FindWithTag("WeaponManager"); 
    }

    public void Update() { 
    }
    public void ItemUsage() {
        //if its a metal, add it into the smeltery
        if (itemdata.itemAttribute == Attribute.Metal) { 
            SmelteryController.AddItem(itemdata.itemName, itemdata.itemQuanity);
        }
        //handling weapon equiping is currently disabled, will enable later
        /*
        if (itemAttribute == Attribute.Weapon) {
            weapon.SetActive(true);
            // Set this specific items equipped true
            weapon.GetComponent<Item>().equipped = true;
        }*/
    }
}
