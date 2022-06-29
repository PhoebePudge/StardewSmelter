using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    GameManager gM;
    public int index;

    void Start()
    {
        gM = GameManager.Instance;
    }

    public void UseItem()
    {
        gM.UseItem(gM.ReturnInventory()[int.Parse(name)]);
        transform.parent.GetComponent<Inventory>().CloseContext();
        transform.parent.GetComponent<Inventory>().UpdateInventory();
    }

    public void EquipItem()
    {
        gM.EquipItem(gM.ReturnInventory()[int.Parse(name)], index);
        transform.parent.GetComponent<Inventory>().CloseContext();
        transform.parent.GetComponent<Inventory>().UpdateInventory();
    }

    public void UnEquipItem()
    {
        gM.UnEquipItem(gM.ReturnInventory()[int.Parse(name)], index);
        transform.parent.GetComponent<Inventory>().CloseContext();
        transform.parent.GetComponent<Inventory>().UpdateInventory();
    }

    public void DropItem()
    {
        transform.parent.GetComponent<Inventory>().CloseContext();
        transform.parent.GetComponent<Inventory>().UpdateInventory();
    }

    public void DestroyItem()
    {
        gM.RemoveItem(gM.ReturnInventory()[int.Parse(name)]);
        transform.parent.GetComponent<Inventory>().CloseContext();
        transform.parent.GetComponent<Inventory>().UpdateInventory();
    }
}
