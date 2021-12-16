using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    GameManager gM;

    void Start()
    {
        gM = GameManager.Instance;
    }

    public void UseItem()
    {
        gM.UseItem(gM.ReturnInventory()[int.Parse(name)]);
    }

    public void EquipItem()
    {
        gM.EquipItem(gM.ReturnInventory()[int.Parse(name)]);
    }

    public void UnEquipItem()
    {
        gM.UnEquipItem(gM.ReturnInventory()[int.Parse(name)]);
    }

    public void DropItem()
    {

    }

    public void DestroyItem()
    {
        gM.RemoveItem(gM.ReturnInventory()[int.Parse(name)]);
    }
}
