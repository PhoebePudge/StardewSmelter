using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySystem : MonoBehaviour{ 
    private bool InventoryEnabled; 
    public GameObject nventory; 
    static private int maxSlotAmount;  
    static private GameObject[] slot; 
    public GameObject slotHolder;
    public Sprite oreSprite;

    //array of items we know, we just look up index to add item now
    //more will be added later on
    public static ItemData[] itemList = new ItemData[] {
        new ItemData("Copper", 5, "Images/UI/CopperOreIcon1", "Dirty copper ore, just found in the dungeon", Attribute.Metal, CraftingUse.None),
        new ItemData("Iron", 5, "Images/UI/IronOreIcon1", "Dirty iron ore, just found in the dungeon", Attribute.Metal, CraftingUse.None),
        new ItemData("Silver", 5, "Images/UI/SilverOreIcon1", "Dirty silver ore, just found in the dungeon", Attribute.Metal, CraftingUse.None),
        new ItemData("Gold", 5, "Images/UI/GoldOreIcon1", "Dirty gold ore, just found in the dungeon", Attribute.Metal, CraftingUse.None),

        new ItemData("Helm", 1, "Images/UI/helmet", "Cheaply made helm, decent but not super great", Attribute.ArmourHead, CraftingUse.None),
        new ItemData("chestplate", 1, "Images/UI/chestplate", "Cheaply made chestplate, decent but not super great", Attribute.ArmourChest, CraftingUse.None),
        new ItemData("gloves", 1, "Images/UI/arms", "Cheaply made gloves, decent but not super great", Attribute.ArmourGloves, CraftingUse.None),
        new ItemData("boots", 1, "Images/UI/legs", "Cheaply made boots, decent but not super great", Attribute.ArmourBoot, CraftingUse.None),

        new ItemData("sword", 1, "Images/UI/weapon", "Cheaply made sword, decent but not super great", Attribute.Sword, CraftingUse.None),
        new ItemData("shield", 1, "Images/UI/shield", "Cheaply made shield, decent but not super great", Attribute.Shield, CraftingUse.None),

        new ItemData("String Binding", 10, "Images/UI/StringBinding", "some string stuff", Attribute.None, CraftingUse.Binding),
        new ItemData("Tool Rod", 10, "Images/UI/ToolRod", "you are a tool", Attribute.None, CraftingUse.ToolRod),
        new ItemData("Pickaxe Head", 5, "Images/UI/PickaxeHead", "what a prick", Attribute.None, CraftingUse.PickHead)
    };

    private void Start() {
        // Set our full slots, can change this later for ugrades, might change to maxSlots
        maxSlotAmount = 30; 
        slot = new GameObject[maxSlotAmount];
        
        // Start looping through slots using maxSlotAmount as our max to identify
        for (int i = 0; i < maxSlotAmount; i++) {
            // Identify our slotHolder and get our slots as children of object
            slot[i] = slotHolder.transform.GetChild(i).gameObject; 
        }
    }

    // Update is called once per frame
    void Update() {

        //Do magic code stuff that surely no one will understand 
        if (Input.GetKeyDown(KeyCode.I))
            InventoryEnabled = !InventoryEnabled;

        if (Input.GetKeyDown(KeyCode.Z)) {
            GameObject gm = new GameObject("Copper"); 
            AddItem(gm, itemList[0]);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            GameObject gm = new GameObject("Iron"); 
            AddItem(gm, itemList[1]);
        }
        if (InventoryEnabled == true) {
            GameObject gm = nventory.transform.GetChild(0).gameObject;
            for (int i = 0; i < gm.transform.childCount; i++) {
                if (i >= 10)
                    gm.transform.GetChild(i).gameObject.SetActive(true);
            } 
        } else {

            GameObject gm = nventory.transform.GetChild(0).gameObject;
            for (int i = 0; i < gm.transform.childCount; i++) {
                if (i >= 10)
                    gm.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
    }
     

    // Our Collide event to pickup
    private void OnTriggerEnter(Collider other) {
        // Check tag
        if(other.tag == "Item") {
            // Create new Gameobject within our void and set it to our .other
            GameObject itemPickedUp = other.gameObject; 

            AddItem(itemPickedUp, itemList[0]);
        }
    }

    // Create our void for our gameobject and basic item identifiers

    public static void AddItem (GameObject itemObject, ItemData itemdata, int amount = 1) {

        // Recreate our loop checker from Start()
        for (int i = 0; i < maxSlotAmount; i++) {
            if (slot[i].GetComponent<Slot>().quanitity != 0) { 
                if (slot[i].GetComponent<Slot>().itemdata.itemName == itemdata.itemName) {
                    if (slot[i].GetComponent<Slot>().SpaceAvilable(amount)) { 
                        slot[i].GetComponent<Slot>().IncreaseQuanity(amount);
                        slot[i].GetComponent<Slot>().UpdateSlot(); 
                        return;
                    }
                }
            } 
            // If slots empty
            if (slot[i].GetComponent<Slot>().quanitity == 0) {

                Slot slots = slot[i].GetComponent<Slot>(); 
                slots.itemdata = itemdata;
                slots.IncreaseQuanity(amount);
                
                itemObject.name = itemdata.itemName; 
                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slots.objectData = itemObject;

                // Hit the Update slot object
                slots.UpdateSlot(); 
                return;

            } 
        }
    } 
}
