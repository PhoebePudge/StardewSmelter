using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    protected ItemData itemData;
    public ItemData ItemData { get { return itemData; } }

    public void Use()
    {
        GameManager gM = GameManager.Instance;

        switch (itemData.itemAttribute)
        {
            case ItemData.Attribute.Weapon:
            {
                break;
            }
            case ItemData.Attribute.Armor:
            {
                break;
            }
            case ItemData.Attribute.Damage:
            {
                gM.ModifyIntData(GameManager.PlayerDataAttributes.Damage, gM.ReturnIntData(GameManager.PlayerDataAttributes.Damage) + itemData.itemUseValue);
                break;
            }
            case ItemData.Attribute.Defence:
            {
                gM.ModifyIntData(GameManager.PlayerDataAttributes.Defence, gM.ReturnIntData(GameManager.PlayerDataAttributes.Defence) + itemData.itemUseValue);
                break;
            }
            case ItemData.Attribute.Health:
            {
                gM.ModifyIntData(GameManager.PlayerDataAttributes.CurrentHealth, gM.ReturnIntData(GameManager.PlayerDataAttributes.CurrentHealth) + itemData.itemUseValue);
                break;
            }
        }

        gM.RemoveItem(this);
    }
}

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
        Health
    }

    public Attribute itemAttribute;

    public int itemUseValue;

    public int itemCost;
}