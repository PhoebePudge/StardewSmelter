using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySystem : MonoBehaviour{ 
    private bool InventoryEnabled; 
    public GameObject nventory; 
    private int maxSlotAmount;  
    private GameObject[] slot; 
    public GameObject slotHolder;
    public Sprite oreSprite;

    //array of items we know, we just look up index to add item now
    //more will be added later on
    ItemData[] itemList = new ItemData[] {
        new ItemData("Copper", 1, 5, "Dirty copper ore, just found in the dungeon", Attribute.Metal)
    };

    private void Start() {
        // Set our full slots, can change this later for ugrades, might change to maxSlots
        maxSlotAmount = 30; 
        slot = new GameObject[maxSlotAmount];
        
        // Start looping through slots using maxSlotAmount as our max to identify
        for (int i = 0; i < maxSlotAmount; i++) {
            // Identify our slotHolder and get our slots as children of object
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
            // Check if there is an item in the slot
            if (slot[i].GetComponent<Slot>().item == null)
            // Make empty switching a bool on the slot item ready to be used in our AddItem() below
                slot[i].GetComponent<Slot>().empty = true;
        }
    }

    // Update is called once per frame
    void Update() {

        //Do magic code stuff that surely no one will understand 
        if (Input.GetKeyDown(KeyCode.H))
            InventoryEnabled = !InventoryEnabled;

        if (Input.GetKeyDown(KeyCode.Z)) {
            GameObject gm = new GameObject("Copper");
            gm.AddComponent<Item>(); 
            AddItem(gm, itemList[0], oreSprite);
        }

        if (InventoryEnabled == true) {
            nventory.SetActive(true);
        } else {
            nventory.SetActive(false);
        }
        
    }
     

    // Our Collide event to pickup
    private void OnTriggerEnter(Collider other) {
        // Check tag
        if(other.tag == "Item") {
            // Create new Gameobject within our void and set it to our .other
            GameObject itemPickedUp = other.gameObject;
            // Call our item script and reference it to our item we picked up
            Item item = itemPickedUp.GetComponent<Item>(); 

            AddItem(itemPickedUp, itemList[0], item.icon);
        }
    }

    // Create our void for our gameobject and basic item identifiers

    void AddItem (GameObject itemObject, ItemData itemdata, Sprite itemIcon) {

        // Recreate our loop checker from Start()
        for (int i = 0; i < maxSlotAmount; i++) {
            if (slot[i].GetComponent<Slot>().empty == false) {
                if (slot[i].GetComponent<Slot>().item.name == itemdata.itemName) {
                    if (slot[i].GetComponent<Slot>().SpaceAvilable()) { 
                        slot[i].GetComponent<Slot>().IncreaseQuanity();
                        slot[i].GetComponent<Slot>().UpdateSlot(); 
                        return;
                    }
                }
            }

            // If slots empty
            if (slot[i].GetComponent<Slot>().empty) {

                // Item is stored within the gameobject
                itemObject.GetComponent<Item>().pickedUp = true;
                itemObject.GetComponent<Item>().itemdata = itemdata;
                itemObject.name = itemdata.itemName;

                // Apply all the things into our slot from the item we just grabbed
                slot[i].GetComponent<Slot>().item = itemObject.GetComponent<Item>();
                slot[i].GetComponent<Slot>().icon = itemIcon; 

                // Move it to current object in array
                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                // Hit the Update slot object
                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;
                return;

            } 
        }
    } 
}
