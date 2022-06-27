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
    static public GameObject[] slot; 
    public GameObject slotHolder;
    public RectTransform SliderTransform;
    //array of items we know, we just look up index to add item now
    //more will be added later on
    public static ItemData[] itemList;
    private static bool alreadyDone = false;
    private void Start() {
        itemList = new ItemData[] {
            new ItemData("Copper", 5, "UI/CopperOreIcon1", "A piece of rock that's high in copper - can be melted down in the furnace", Attribute.Metal),//0
            new ItemData("Iron", 5, "UI/IronOreIcon1", "A piece of rock that's high in iron - can be melted down in the furnace", Attribute.Metal),//1
            new ItemData("Silver", 5, "UI/Silver", "A piece of rock that’s high in silver - can be melted down in the furnace", Attribute.Metal),//2
            new ItemData("Gold", 5, "UI/Gold", "Dirty gold ore - just found in the dungeon", Attribute.Metal),//3

            new ArmourData(new Metal[] { SmelteryController.oreDictionary[9], SmelteryController.oreDictionary[9], SmelteryController.oreDictionary[9]}, "Helm", 1, "UI/helmet", "A protective hat the keep your head intact", Attribute.ArmourHead),//4
            new ArmourData(new Metal[] { SmelteryController.oreDictionary[9], SmelteryController.oreDictionary[9], SmelteryController.oreDictionary[9]}, "Chestplate", 1, "UI/chestplate", "A protective garment that might help if someone tries to shank you in the gu", Attribute.ArmourChest),//5
            new ArmourData(new Metal[] { SmelteryController.oreDictionary[9], SmelteryController.oreDictionary[9], SmelteryController.oreDictionary[9]}, "Gloves", 1, "UI/arms", "A set of gloves that’ll keep scratches off your hands", Attribute.ArmourGloves),//6
            new ArmourData(new Metal[] { SmelteryController.oreDictionary[9], SmelteryController.oreDictionary[9], SmelteryController.oreDictionary[9]}, "Boots", 1, "UI/legs", "A set of boots - a little big to fill but you’ll do fine", Attribute.ArmourBoot),//7

            new ItemWeapon(new Metal[] { null, null, null}, WeaponTypes.Sword, "Sword", 1, "UI/Sword", "A fine weapon - be careful with the pointy bit", Attribute.Equip1),//8
            new ItemWeapon(new Metal[] { null, null, null}, WeaponTypes.Pickaxe, "Pickaxe", 1, "UI/Pickaxe", "A workhorse tool for breaking rocks and ores", Attribute.Equip1),//9
            
            new ItemData("String Binding", 10, "UI/StringBinding", "A bit of string used to tie things together", Attribute.CraftingPart),//10
            new ItemData("Tool Rod", 10, "UI/ToolRod", "A big stick made for holding", Attribute.CraftingPart),//11
            new ItemData("Pickaxe Head", 5, "UI/PickaxeHead", "what a prick", Attribute.CraftingPart),//12

            new ItemData("Ingot", 5, "UI/Ingot", "A refined hunk of metal", Attribute.None),//13

            new ItemData("Sword Blade", 5, "UI/SwordBlade", "The bit on a sword used for cutting - be careful putting this on", Attribute.CraftingPart),//14
            new ItemData("Sword Guard", 5, "UI/SwordGuard", "A guard for a sword that useful for keeping your hand safe", Attribute.CraftingPart),//15 
            
            new ItemWeapon(new Metal[] { SmelteryController.oreDictionary[0], SmelteryController.oreDictionary[0], SmelteryController.oreDictionary[0]}, WeaponTypes.Sword, "Wooden Sword", 1, "UI/WoodenSword", "Basic training sword - a good starting item", Attribute.Equip1),//16 
            new ItemWeapon(new Metal[] { SmelteryController.oreDictionary[0], SmelteryController.oreDictionary[0], SmelteryController.oreDictionary[0]}, WeaponTypes.Pickaxe, "Wooden Pickaxe", 1, "UI/WoodenPickaxe", "Basic training pickaxe - a good starting item", Attribute.Equip1),//17 

            new ItemData("Rock", 10, "UI/Rock", "A simple rock - it currently has no use", Attribute.None),//18

            new ItemData("Admant", 10, "UI/AdmantOre", "A simple rock - it currently has no use", Attribute.Metal),//19
            new ItemData("Mithirl", 10, "UI/Mithirl", "A simple rock - it currently has no use", Attribute.Metal),//20
            new ItemData("Orichalcum", 10, "UI/Orichalcum", "A simple rock - it currently has no use", Attribute.Metal),//21
            new ItemData("Tin", 10, "UI/Tin", "A simple rock - it currently has no use", Attribute.Metal),//22
            new ItemData("Null Part", 10, "UI/Null", "A nulled item - set to a crafting part", Attribute.CraftingPart)//23
        };

        if (alreadyDone)
        {
            GameObject.Destroy(transform.parent.gameObject);
            return;
        }
        alreadyDone = true;
        DontDestroyOnLoad(transform.parent); 

        // Set our full slots, can change this later for ugrades
        maxSlotAmount = (9 * 3) - 4; 
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
        for (int i = 1; i < 10; i++)
        {
            if (Input.GetKeyDown("" + i))
            { 
                StartCoroutine(ButtonPress(i - 1));
                nventory.transform.GetChild(0).transform.GetChild(i - 1).gameObject.GetComponent<Slot>().UseItem();
            }
        }
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
            gm.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 130);
            gm.transform.localPosition = new Vector3(-21.3923f, 2, 0);
            for (int i = 0; i < gm.transform.childCount; i++) {
                if (i >= 9)
                    gm.transform.GetChild(i).gameObject.SetActive(true);
            }
            SliderTransform.localPosition = new Vector3(305,78, 0);
        } else {

            GameObject gm = nventory.transform.GetChild(0).gameObject;
            gm.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 135 / 3 + 8);
            gm.transform.localPosition = new Vector3(-21.3923f, 40, 0);
            for (int i = 0; i < gm.transform.childCount; i++) {
                if (i >= 9)
                    gm.transform.GetChild(i).gameObject.SetActive(false);
            }
            SliderTransform.localPosition = new Vector3(305,158,0);
        } 
    }
    IEnumerator ButtonPress(int i)
    { 
        Debug.LogError("Stack here");
        Button button = nventory.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<Button>();
        Image image = nventory.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<Image>();
        Color colour = button.colors.pressedColor;
        image.color = colour;

        yield return new WaitForSeconds(0.1f);

        colour = button.colors.normalColor;
        image.color = colour;
    }
    // Create our void for our gameobject and basic item identifiers
    public static bool AddItem (GameObject itemObject, ItemData itemdata, int amount = 1) {  
        // Recreate our loop checker from Start()
        for (int i = 0; i < maxSlotAmount; i++) {
            Slot slots = slot[i].GetComponent<Slot>();

            //existing item
            if (slots.SlotInUse()) { 
                if (slots.itemdata.itemName == itemdata.itemName) {
                    if (slots.SpaceAvilable(amount)) {
                        slots.IncreaseQuanity(amount);
                        slots.UpdateSlot(); 
                        return true;
                    }
                }
            } 

            // If slots empty
            if (slots.quantity == 0) {
                if (slots.slotType == Attribute.None)
                {
                    slots.itemdata = itemdata;
                    slots.IncreaseQuanity(amount);

                    if (itemObject != null)
                    {
                        itemObject.name = itemdata.itemName;
                        itemObject.transform.parent = slot[i].transform;
                        itemObject.SetActive(false);
                    }

                    if (itemObject != null)
                    { 
                        slots.objectData = itemObject;
                    }

                    // Hit the UpdateSlot method on our slots
                    slots.UpdateSlot();
                    return true;
                }
            } 
        }
        Debug.LogError("Out of storage");
        WarningMessage.SetWarningMessage("Out of Storage", "You ran out of storage you idiot, do something");
        return false;
    } 
}
