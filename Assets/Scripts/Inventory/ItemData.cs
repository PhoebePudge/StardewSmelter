using UnityEngine;
[System.Serializable]
public class ItemData
{
    public string itemName;
     
    public int maxItemQuanity;

    public Sprite sprite;
    public string spritePath;
    public string itemDescription; 
      
    public Attribute itemAttribute;

    
    //public int itemUseValue; 
    //public int itemCost;

    //constructor
    public ItemData(string itemName, int maxItemQuanity, string itemPath, string itemDescription, Attribute itemAttribute) {
        this.itemName = itemName; 
        this.maxItemQuanity = maxItemQuanity;
        this.spritePath = itemPath;
        this.itemDescription = itemDescription;
        this.itemAttribute = itemAttribute; 
    }  
    public ItemData(ItemData itemData)
    {
        this.itemName = itemData.itemName;
        this.maxItemQuanity = itemData.maxItemQuanity; 
        this.sprite = itemData.sprite; 
        this.itemDescription = itemData.itemDescription;
        this.itemAttribute = itemData.itemAttribute;
    }
}
public class ItemWeapon : ItemData{
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

    public string headType;
    public int MetalLevel;
    public ItemWeapon(string itemName, int maxItemQuanity, string itemPath, string itemDescription, Attribute itemAttribute, string headType = "Wood") : 
        base(itemName, maxItemQuanity, itemPath, itemDescription, itemAttribute)
    {
        this.itemName = itemName;
        this.maxItemQuanity = maxItemQuanity;
        this.spritePath = itemPath;
        this.itemDescription = itemDescription;
        this.itemAttribute = itemAttribute;
         
        this.headType = headType;

        foreach (PickaxeLevels item in pickaxeLevels)
        {
            if (item.name == headType)
            {
                MetalLevel = item.level;
            }
        } 
    }
    public ItemWeapon(ItemData itemData, string headType = "Wood") : base(itemData)
    {
        this.itemName = itemData.itemName;
        this.maxItemQuanity = itemData.maxItemQuanity;
        this.sprite = itemData.sprite;
        this.itemDescription = itemData.itemDescription;
        this.itemAttribute = itemData.itemAttribute;
         
        this.headType = headType;

        foreach (PickaxeLevels item in pickaxeLevels)
        {
            if (item.name == headType)
            {
                MetalLevel = item.level;
            }
        }
        Debug.LogError(headType + " = " + MetalLevel);
    }
}

//types of attributes a item or a slot can have
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