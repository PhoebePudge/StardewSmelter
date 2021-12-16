using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemData itemData;
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

        gM.RemoveItem(ItemData);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.AddItem(ItemData);
        }
    }
}
