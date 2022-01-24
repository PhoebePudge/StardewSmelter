using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Item : MonoBehaviour {

    private static int indexCount = 0;

    // Objects ID, we'll use this for specific items such as in our weaponManager to identify weapons we're trying to equip
    public int ID;

    public ItemData itemdata;
    /*
    public string description; 
    public Attribute itemAttribute;
    */
    public bool playersWeapon;
    public Sprite icon;
    public bool pickedUp;
    [HideInInspector] public bool equipped; 
    [HideInInspector] public GameObject weapon; 

    // Define our weaponManager gameobject which will handle our potions... Wait no, our weapons
    [HideInInspector] public GameObject weaponManager;

    public void Start() {

        ID = indexCount++;
         
        weaponManager = GameObject.FindWithTag("WeaponManager"); 
    }

    public void Update() { 
    }
    public void ItemUsage() {
        Debug.LogError(itemdata.itemAttribute);
        if (itemdata.itemAttribute == Attribute.Metal) { 
            SmelteryController.AddItem(itemdata.itemName, itemdata.itemQuanity);
        }
        /*
        if (itemAttribute == Attribute.Weapon) {
            weapon.SetActive(true);
            // Set this specific items equipped true
            weapon.GetComponent<Item>().equipped = true;
        }*/
    }
}
