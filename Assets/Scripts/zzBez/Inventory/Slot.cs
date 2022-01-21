using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot : MonoBehaviour, IPointerClickHandler
{
    // Cloned identifiers from Item script
    public int ID;

    public Attribute itemAttribute;
    public string type;
    public string description;
    public Sprite icon;
    // Used to reference our Item scripts ItemUsage() on the specific gameobject 
    public GameObject item;
    public bool empty;
    // Identify our slot to send icon to
    public Transform slotIconGO;

    private GameObject optionMenu;

    public void OnPointerClick(PointerEventData pointerEventData) { 
        if (pointerEventData.button == 0) {
            Debug.LogError("0");
        } else {
            optionMenu.SetActive(true);
            Debug.LogError("E");
        }
        //UseItem();
        //Debug.Log("PointerClick");
    } 
    public void Start() {
        // Start at first slot
        slotIconGO = transform.GetChild(0);

        optionMenu = new GameObject();
        optionMenu.transform.SetParent(slotIconGO.transform);
        optionMenu.transform.position = slotIconGO.transform.position;
        optionMenu.AddComponent<TextMeshProUGUI>().text = "test";
        optionMenu.SetActive(false);
    }
    public void UpdateSlot() {
        slotIconGO.GetComponent<Image>().color = Color.white;
        // Grab whatever image slotIconGo is at and replace it with current icon
        slotIconGO.GetComponent<Image>().sprite = icon;
    }

    public void UseItem() {
        // Do whatever ItemUsage does for this item
        // We may want to change this out and identify before UseItem()
        // So if tag is Weapon do UseWeapon(), but this is not my expertise so not sure if it's worth it
        item.GetComponent<Item>().ItemUsage();
        Debug.Log("ItemUsed");
    }
    
}
