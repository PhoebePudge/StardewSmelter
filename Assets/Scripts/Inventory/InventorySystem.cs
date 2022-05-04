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

    public RectTransform SliderTransform;
	//public Collider player;

    //[SerializeField] Transform ArmourSlots;

    //array of items we know, we just look up index to add item now
    //more will be added later on
    public static ItemData[] itemList = new ItemData[] {
        new ItemData("Copper", 5, "UI/CopperOreIcon1", "Dirty copper ore, just found in the dungeon", Attribute.Metal),//0
        new ItemData("Iron", 5, "UI/IronOreIcon1", "Dirty iron ore, just found in the dungeon", Attribute.Metal),//1
        new ItemData("Silver", 5, "UI/SilverOreIcon1", "Dirty silver ore, just found in the dungeon", Attribute.Metal),//2
        new ItemData("Gold", 5, "UI/GoldOreIcon1", "Dirty gold ore, just found in the dungeon", Attribute.Metal),//3

        new ItemData("Helm", 1, "UI/helmet", "Cheaply made helm, decent but not super great", Attribute.ArmourHead),//4
        new ItemData("Chestplate", 1, "UI/chestplate", "Cheaply made chestplate, decent but not super great", Attribute.ArmourChest),//5
        new ItemData("Gloves", 1, "UI/arms", "Cheaply made gloves, decent but not super great", Attribute.ArmourGloves),//6
        new ItemData("Boots", 1, "UI/legs", "Cheaply made boots, decent but not super great", Attribute.ArmourBoot),//7

        new ItemWeapon("Sword", 1, "UI/Sword", "Cheaply made sword, decent but not super great", Attribute.Equip1),//8
        new ItemWeapon("Pickaxe", 1, "UI/Pickaxe", "Cheaply made pickaxe, decent but not super great", Attribute.Equip1),//9

        //new CraftingItem("String Binding", 10, "Images/UI/StringBinding", "some string stuff", Attribute.None, CraftingUse.Binding),
        //new CraftingItem("Tool Rod", 10, "Images/UI/ToolRod", "you are a tool", Attribute.None, CraftingUse.ToolRod),
        //new CraftingItem("Pickaxe Head", 5, "Images/UI/PickaxeHead", "what a prick", Attribute.None, CraftingUse.PickHead)

        new ItemData("String Binding", 10, "UI/StringBinding", "some string stuff", Attribute.CraftingPart),//10
        new ItemData("Tool Rod", 10, "UI/ToolRod", "you are a tool", Attribute.CraftingPart),//11
        new ItemData("Pickaxe Head", 5, "UI/PickaxeHead", "what a prick", Attribute.CraftingPart),//12
        new ItemData("Ingot", 5, "UI/Ingot", "One unit of metal", Attribute.None),//13
        new ItemData("Sword Blade", 5, "UI/SwordBlade", "Blade part for a sword", Attribute.CraftingPart),//14
        new ItemData("Sword Guard", 5, "UI/SwordGuard", "Will sort out description when I have time", Attribute.CraftingPart),//15 
         
        new ItemWeapon("Wooden Sword", 1, "UI/WoodenSword", "Basic training sword, a good starting item", Attribute.Equip1),//16 
        new ItemWeapon("Wooden Pickaxe", 1, "UI/WoodenPickaxe", "Basic training pickaxe, a good starting item", Attribute.Equip1),//17 

        new ItemData("Rock", 10, "UI/Rock", "A simple rock, it currently has no use", Attribute.Equip1)//18
}; 
    private void Start() {
        DontDestroyOnLoad(transform.parent); 
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

        GameObject gm = new GameObject("Silver");
        AddItem(gm, itemList[16]);
         
        AddItem(gm, itemList[17]);
    }

    // Update is called once per frame
    void Update() {

        // Turn on inventory
        if (Input.GetKeyDown(KeyCode.I))
            InventoryEnabled = !InventoryEnabled;
        /*
        if (Input.GetKeyDown(KeyCode.Z)) {
            GameObject gm = new GameObject("Copper"); 
            AddItem(gm, itemList[0]);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            GameObject gm = new GameObject("Iron"); 
            AddItem(gm, itemList[1]);
        }


        */
        if (InventoryEnabled == true) {
            GameObject gm = nventory.transform.GetChild(0).gameObject;
            for (int i = 0; i < gm.transform.childCount; i++) {
                if (i >= 10)
                    gm.transform.GetChild(i).gameObject.SetActive(true);
            }
            SliderTransform.localPosition = new Vector3(305,80, 0);
        } else {

            GameObject gm = nventory.transform.GetChild(0).gameObject;
            for (int i = 0; i < gm.transform.childCount; i++) {
                if (i >= 10)
                    gm.transform.GetChild(i).gameObject.SetActive(false);
            }
            SliderTransform.localPosition = new Vector3(305,172,0);
        }
        
    }

	public void Pickup() {
        /*

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
        */
	}

    // Create our void for our gameobject and basic item identifiers
    public static void AddItem (GameObject itemObject, ItemData itemdata, int amount = 1) {
        Debug.LogError("Adding " + amount);
        // Recreate our loop checker from Start()
        for (int i = 0; i < maxSlotAmount; i++) {
            Slot slots = slot[i].GetComponent<Slot>();

            //existing item
            if (slots.SlotInUse()) { 
                if (slots.itemdata.itemName == itemdata.itemName) {
                    if (slots.SpaceAvilable(amount)) {
                        slots.IncreaseQuanity(amount);
                        slots.UpdateSlot(); 
                        return;
                    }
                }
            } 

            // If slots empty
            if (slots.quantity == 0) {
                if (slots.slotType == Attribute.None)
                {
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
}
