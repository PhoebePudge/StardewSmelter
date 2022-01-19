using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    // Cloned identifiers from Item script
    public int ID;
    public string type;
    public string description;
    public Sprite icon;
    // Used to reference our Item scripts ItemUsage() on the specific gameobject 
    public GameObject item;
    public bool empty;
    // Identify our slot to send icon to
    public Transform slotIconGO;





    public void OnPointerClick(PointerEventData pointerEventData)
    {
        
        UseItem();
        Debug.Log("PointerClick");
    }

    

    public void Start()
    {
        // Start at first slot
        slotIconGO = transform.GetChild(0);
    }
    public void UpdateSlot()
    {
        // Grab whatever image slotIconGo is at and replace it with current icon
        slotIconGO.GetComponent<Image>().sprite = icon;
    }

    public void UseItem()
    {
        // Do whatever ItemUsage does for this item
        // We may want to change this out and identify before UseItem()
        // So if tag is Weapon do UseWeapon(), but this is not my expertise so not sure if it's worth it
        item.GetComponent<Item>().ItemUsage();
        Debug.Log("ItemUsed");
    }
    
}
