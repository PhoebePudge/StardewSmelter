[System.Serializable]
public struct ItemData
{
    public string itemName;

    public string itemDescription;

    public string itemImagePath;

    public enum Attribute
    {
        Weapon,
        Armor,
        Damage,
        Defence,
        Health,
        Object,
        Metal,
        None
    }

    public Attribute itemAttribute;

    public int itemUseValue;

    public int itemCost;
}