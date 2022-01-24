[System.Serializable]
public struct ItemData
{
    public string itemName;

    public int itemQuanity;

    public int maxItemQuanity;

    public string itemDescription; 
      
    public Attribute itemAttribute;

    //public int itemUseValue; 
    //public int itemCost;
     
    //constructor
    public ItemData(string itemName, int itemQuanity, int maxItemQuanity,
        string itemDescription, Attribute itemAttribute) {
        this.itemName = itemName;
        this.itemQuanity = itemQuanity;
        this.maxItemQuanity = maxItemQuanity;
        this.itemDescription = itemDescription;
        this.itemAttribute = itemAttribute;
    }
}
//types of attributes a item or a slot can have
public enum Attribute {
    Weapon,
    Armor,
    Damage,
    Defence,
    Health,
    Object,
    Metal,
    None
}