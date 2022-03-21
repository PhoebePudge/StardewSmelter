using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour{

	public int itemPickedUp;
    private bool InventoryEnabled; 
    public GameObject nventory; 
    static private int maxSlotAmount;  
    static private GameObject[] slot; 
    public GameObject slotHolder;
    public Sprite oreSprite;
	//public Collider player;

    //[SerializeField] Transform ArmourSlots;

    //array of items we know, we just look up index to add item now
    //more will be added later on
    public static ItemData[] itemList = new ItemData[] {
        new ItemData("Copper", 5, "UI/CopperOreIcon1", "Dirty copper ore, just found in the dungeon", Attribute.Metal),
        new ItemData("Iron", 5, "UI/IronOreIcon1", "Dirty iron ore, just found in the dungeon", Attribute.Metal),
        new ItemData("Silver", 5, "UI/SilverOreIcon1", "Dirty silver ore, just found in the dungeon", Attribute.Metal),
        new ItemData("Gold", 5, "UI/GoldOreIcon1", "Dirty gold ore, just found in the dungeon", Attribute.Metal),

        new ItemData("Helm", 1, "UI/helmet", "Cheaply made helm, decent but not super great", Attribute.ArmourHead),
        new ItemData("Chestplate", 1, "UI/chestplate", "Cheaply made chestplate, decent but not super great", Attribute.ArmourChest),
        new ItemData("Gloves", 1, "UI/arms", "Cheaply made gloves, decent but not super great", Attribute.ArmourGloves),
        new ItemData("Boots", 1, "UI/legs", "Cheaply made boots, decent but not super great", Attribute.ArmourBoot),

        new ItemData("sword", 1, "UI/weapon", "Cheaply made sword, decent but not super great", Attribute.Sword),
        new ItemData("shield", 1, "UI/shield", "Cheaply made shield, decent but not super great", Attribute.Shield),

        //new CraftingItem("String Binding", 10, "Images/UI/StringBinding", "some string stuff", Attribute.None, CraftingUse.Binding),
        //new CraftingItem("Tool Rod", 10, "Images/UI/ToolRod", "you are a tool", Attribute.None, CraftingUse.ToolRod),
        //new CraftingItem("Pickaxe Head", 5, "Images/UI/PickaxeHead", "what a prick", Attribute.None, CraftingUse.PickHead)

        new ItemData("String Binding", 10, "UI/StringBinding", "some string stuff", Attribute.CraftingPart),
        new ItemData("Tool Rod", 10, "UI/ToolRod", "you are a tool", Attribute.CraftingPart),
        new ItemData("Pickaxe Head", 5, "UI/PickaxeHead", "what a prick", Attribute.CraftingPart)
    }; 
    private void Start() {

        foreach (var item in itemList)
        {
            item.sprite = Resources.Load<Sprite>(item.spritePath);
        }

        // Set our full slots, can change this later for ugrades
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

        // Turn on inventory
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
            //ArmourSlots.gameObject.SetActive(true);
        } else {

            GameObject gm = nventory.transform.GetChild(0).gameObject;
            for (int i = 0; i < gm.transform.childCount; i++) {
                if (i >= 10)
                    gm.transform.GetChild(i).gameObject.SetActive(false);
            }
            //ArmourSlots.gameObject.SetActive(false);
        }
        
    }
     

   // // Our Collide event to pickup
   // private void OnTriggerEnter(Collider other) {
   //     // Check tag
   //     if(other.tag == "Item") {
   //         // Create new Gameobject within our void and set it to our .other
   //         GameObject itemPickedUp = other.gameObject; 
   //         AddItem(itemPickedUp, itemList[0], 1);
			//Destroy(itemPickedUp);
   //     }
   // }

	public void Pickup() {


		if (itemPickedUp == 0) {
			GameObject gm = new GameObject("Copper");
			AddItem(gm, itemList[0]);
		}
		if (itemPickedUp == 1) {
			GameObject gm = new GameObject("Iron");
			AddItem(gm, itemList[1]);
		}
		if (itemPickedUp == 2) {
			GameObject gm = new GameObject("Silver");
			AddItem(gm, itemList[2]);
		}
		if (itemPickedUp == 3) {
			GameObject gm = new GameObject("Gold");
			AddItem(gm, itemList[3]);
		}
		if (itemPickedUp == 4) {
			GameObject gm = new GameObject("Helmet");
			AddItem(gm, itemList[4]);
		}
		if (itemPickedUp == 5) {
			GameObject gm = new GameObject("Chestplate");
			AddItem(gm, itemList[5]);
		}
		if (itemPickedUp == 6) {
			GameObject gm = new GameObject("Gloves");
			AddItem(gm, itemList[6]);
		}
		if (itemPickedUp == 7) {
			GameObject gm = new GameObject("Boots");
			AddItem(gm, itemList[7]);
		}
		if (itemPickedUp == 8) {
			GameObject gm = new GameObject("Sword");
			AddItem(gm, itemList[8]);
		}
		if (itemPickedUp == 9) {
			GameObject gm = new GameObject("Shield");
			AddItem(gm, itemList[9]);
		}
		if (itemPickedUp == 10) {
			GameObject gm = new GameObject("String Binding");
			AddItem(gm, itemList[10]);
		}
		if (itemPickedUp == 11) {
			GameObject gm = new GameObject("Tool Rod");
			AddItem(gm, itemList[11]);
		}
		if (itemPickedUp == 12) {
			GameObject gm = new GameObject("Pickaxe Head");
			AddItem(gm, itemList[12]);
		}
	}

    // Create our void for our gameobject and basic item identifiers

    public static void AddItem (GameObject itemObject, ItemData itemdata, int amount = 1) {

        // Recreate our loop checker from Start()
        for (int i = 0; i < maxSlotAmount; i++) {
            if (slot[i].GetComponent<Slot>().SlotInUse()) { 
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

                // Hit the UpdateSlot method on our slots
                slots.UpdateSlot(); 
                return;

            } 
        }
    } 
}
