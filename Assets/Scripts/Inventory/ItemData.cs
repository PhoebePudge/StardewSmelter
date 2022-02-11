[System.Serializable]
public struct ItemData
{
    public string itemName;
     
    public int maxItemQuanity;

    public string imagePath;

    public string itemDescription; 
      
    public Attribute itemAttribute;

    public CraftingUse craftingUse;
    //public int itemUseValue; 
    //public int itemCost;

    //constructor
    public ItemData(string itemName, int maxItemQuanity, string imagePath,
        string itemDescription, Attribute itemAttribute, CraftingUse craftingUse) {
        this.itemName = itemName; 
        this.maxItemQuanity = maxItemQuanity;
        this.imagePath = imagePath;
        this.itemDescription = itemDescription;
        this.itemAttribute = itemAttribute;
        this.craftingUse = craftingUse;
    }
}

//types of attributes a item or a slot can have
public enum Attribute {
    ArmourHead,
    ArmourChest,
    ArmourBoot,
    ArmourGloves,

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