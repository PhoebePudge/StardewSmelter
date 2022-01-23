using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Add this to the player, copy/paste the canvas from the shop scene 
    // Plug in the inventory and slot holder
    // Should work at that point

    // Use the Item script attached to items using Item tags to identify
    // We'll currently have to decide on and then add our tags and what they do function wise

    // Will change to inventory once we commit to this system, so that it doesn't throw any code clashes 
    private bool InventoryEnabled;
    // This is the canvas gameobject
    public GameObject nventory;
    // This is creating our slot limit
    private int maxSlotAmount;
    // Saying whether or not our slot is being used
    private int enableSlots;
    // This is our actual array of slots, 
    // We'll want to make sure there is enough in the scene here or it might throw an error
    private GameObject[] slot;
    // This is a canvas gameobject holding all our item slots
    public GameObject slotHolder;
    public Sprite oreSprite;
    public Sprite weaponSprite;
    public Sprite sprite;
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
            AddItem(gm, 0, Attribute.Metal, "Some Ore Stuff", oreSprite);
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
            // Call our AddItem using the identified Gameobjects data output
            AddItem(itemPickedUp, item.ID, item.itemAttribute, item.description, item.icon);
        }
    }

    // Create our void for our gameobject and basic item identifiers

    void AddItem (GameObject itemObject, int itemID, Attribute itemType, string itemDescription, Sprite itemIcon) {
        // Recreate our loop checker from Start()
        for (int i = 0; i < maxSlotAmount; i++) {
            // If slots empty
            if (slot[i].GetComponent<Slot>().empty) {
                // Access whatever item we're hitting and switch pickedUp
                itemObject.GetComponent<Item>().pickedUp = true;
                // Apply all the things into our slot from the item we just grabbed
                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().itemAttribute = itemType;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().description = itemDescription;
                // Move it to current object in array
                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);
                // Hit the Update slot object
                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;

            }

            return;
        }
    } 
}
