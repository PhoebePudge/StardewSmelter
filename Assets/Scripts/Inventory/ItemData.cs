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
}
/*
public class CraftingItem : ItemData{
    public CraftingUse craftingUse;

    public CraftingItem(string itemName, int maxItemQuanity, string imagePath, string itemDescription, Attribute itemAttribute, CraftingUse craftingUse) :  
        base(itemName, maxItemQuanity, imagePath, itemDescription, itemAttribute) {

        this.itemAttribute = Attribute.CraftingPart;
        this.craftingUse = craftingUse;
    }
}
*/
//types of attributes a item or a slot can have
public enum Attribute {
    ArmourHead,
    ArmourChest,
    ArmourBoot,
    ArmourGloves,
    CraftingPart,

    Shield,
    Sword,  
     
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