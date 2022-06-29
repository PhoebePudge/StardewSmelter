using UnityEngine;
[System.Serializable]
public class ItemData {
    public string itemName = ""; 
    public int maxItemQuanity = 0; 
    public Sprite sprite;
    public string spritePath = "";
    public string itemDescription = ""; 
    public Attribute itemAttribute = Attribute.None;

    public ItemData(string itemName, int maxItemQuanity, string itemPath, string itemDescription, Attribute itemAttribute) {
        //create new item data based on input
        this.itemName = itemName;
        this.maxItemQuanity = maxItemQuanity;
        this.spritePath = itemPath; 
        this.sprite = ResourceLoaderHelper.LoadResource(this.spritePath); 
        this.itemDescription = itemDescription;
        this.itemAttribute = itemAttribute;
    }
    public ItemData(ItemData itemData) {
        //create new item data based on example
        this.itemName = itemData.itemName;
        this.maxItemQuanity = itemData.maxItemQuanity;
        this.sprite = itemData.sprite; 
        this.sprite = ResourceLoaderHelper.LoadResource(this.spritePath); 
        this.itemDescription = itemData.itemDescription;
        this.itemAttribute = itemData.itemAttribute;
    }
    public override string ToString() {
        string output = "";
        output += itemName + "," + maxItemQuanity + "," + spritePath + "," + itemDescription + "," + itemAttribute;
        return output;
    }
}

public class ArmourData : ItemData {
    public Metal[] metals;
    public Color[] colour;
    public ArmourData(Metal[] metals, string itemName, int maxItemQuanity, string itemPath, string itemDescription, Attribute itemAttribute) :
    base(itemName, maxItemQuanity, itemPath, itemDescription, itemAttribute) {
        //create new armour data based on input
        this.metals = metals;
        this.itemName = itemName;
        this.maxItemQuanity = maxItemQuanity;
        this.spritePath = itemPath;
        this.itemDescription = itemDescription;
        this.itemAttribute = itemAttribute; 
        colour = new Color[] { metals[0].col, metals[1].col, metals[2].col };
    }
    public ArmourData(Metal[] metals, ItemData itemData) : base(itemData) { 
        //create new armour data based on item data reference
        this.metals = metals;
        this.itemName = itemData.itemName;
        this.maxItemQuanity = itemData.maxItemQuanity;
        this.sprite = itemData.sprite;
        this.itemDescription = itemData.itemDescription;
        this.itemAttribute = itemData.itemAttribute; 
        colour = new Color[] { metals[0].col, metals[1].col, metals[2].col };
    }
}
public class ItemWeapon : ItemData {
    //pickaxe levels
    public static PickaxeLevels[] pickaxeLevels = {
    new PickaxeLevels("Wood", 0),
    new PickaxeLevels("Tin", 1),
    new PickaxeLevels("Copper", 1),
    new PickaxeLevels("Iron", 2),
    new PickaxeLevels("Gold", 3),
    new PickaxeLevels("Silver", 3),
    new PickaxeLevels("Admant", 4),
    new PickaxeLevels("Orichalcum", 5),
    new PickaxeLevels("Mithirl", 6)
    };
    //variables
    public WeaponTypes type;
    public Metal[] metals;
    public string headType;
    public int MetalLevel;
    public ItemWeapon(Metal[] metals, WeaponTypes type, string itemName, int maxItemQuanity, string itemPath, string itemDescription, Attribute itemAttribute, string headType = "Wood") :
        //create weapons based on input
        base(itemName, maxItemQuanity, itemPath, itemDescription, itemAttribute) {
        this.metals = metals;
        this.type = type;
        this.itemName = itemName;
        this.maxItemQuanity = maxItemQuanity;
        this.spritePath = itemPath;
        this.itemDescription = itemDescription;
        this.itemAttribute = itemAttribute; 
        this.headType = headType;

        foreach (PickaxeLevels item in pickaxeLevels) {
            if (item.name == headType) {
                MetalLevel = item.level;
            }
        }
    }
    public ItemWeapon(Metal[] metals, WeaponTypes type, ItemData itemData, string headType = "Wood") : base(itemData) {
        //create weapons based on item data reference
        this.metals = metals;
        this.type = type;
        this.itemName = itemData.itemName;
        this.maxItemQuanity = itemData.maxItemQuanity;
        this.sprite = itemData.sprite;
        this.itemDescription = itemData.itemDescription;
        this.itemAttribute = itemData.itemAttribute; 
        this.headType = headType;

        foreach (PickaxeLevels item in pickaxeLevels) {
            if (item.name == headType) {
                MetalLevel = item.level;
            }
        }
    }
}

//enums for use with these classes
public enum Attribute {
    ArmourHead,
    ArmourChest,
    ArmourBoot,
    ArmourGloves,
    CraftingPart,

    Equip2,
    Equip1,  
     
    Damage,
    Defence,
    Health,
    Object,
    Metal,
    None
}
public enum CraftingUse {
    None,
    ToolRod,
    Binding,
    PickHead
}