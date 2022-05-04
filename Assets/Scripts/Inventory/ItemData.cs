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
    public string headType;
    public ItemWeapon(string itemName, int maxItemQuanity, string itemPath, string itemDescription, Attribute itemAttribute, string headType = "wood") : 
        base(itemName, maxItemQuanity, itemPath, itemDescription, itemAttribute)
    {
        this.itemName = itemName;
        this.maxItemQuanity = maxItemQuanity;
        this.spritePath = itemPath;
        this.itemDescription = itemDescription;
        this.itemAttribute = itemAttribute;

        Debug.LogError("Made weapon with " + headType);
        this.headType = headType;
    }
    public ItemWeapon(ItemData itemData, string headType = "wood") : base(itemData)
    {
        this.itemName = itemData.itemName;
        this.maxItemQuanity = itemData.maxItemQuanity;
        this.sprite = itemData.sprite;
        this.itemDescription = itemData.itemDescription;
        this.itemAttribute = itemData.itemAttribute;

        Debug.LogError("Made weapon with " + headType);
        this.headType = headType;
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