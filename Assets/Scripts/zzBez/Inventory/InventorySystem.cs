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
    private bool nventoryEnabled;
    // This is the canvas gameobject
    public GameObject nventory;
    // This is creating our slot limit
    private int allSlots;
    // Saying whether or not our slot is being used
    private int enableSlots;
    // This is our actual array of slots, 
    // We'll want to make sure there is enough in the scene here or it might throw an error
    private GameObject[] slot;
    // This is a canvas gameobject holding all our item slots
    public GameObject slotHolder;

    private void Start()
    {
        // Set our full slots
        allSlots = 40;
        // Create new array index of slot and make it the same ammount as our array
        slot = new GameObject[allSlots];
        
        // Start looping through i using allSlots as our max
        for (int i = 0; i < allSlots; i++)
        {
            // Identify I as our slotHolder
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null)
                slot[i].GetComponent<Slot>().empty = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

        //Do magic code stuff that surely no one will understand 
        if (Input.GetKeyDown(KeyCode.I))
            nventoryEnabled = !nventoryEnabled;

        if (nventoryEnabled == true)
        {
            nventory.SetActive(true);
        }
        else
        {
            nventory.SetActive(false);
        }
        
    }

    // Our Collide event to pickup
    private void OnTriggerEnter(Collider other)
    {
        // Check tag
        if(other.tag == "Item")
        {
            // Create new Gameobject within our void and set it to our .other
            GameObject itemPickedUp = other.gameObject;
            // Call our item script and reference it to our item we picked up
            Item item = itemPickedUp.GetComponent<Item>();
            // Call our AddItem using the identified Gameobjects data output
            AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon);
        }
    }

    void AddItem (GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().description = itemDescription;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;

            }

            return;
        }
    } 
}
