using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler , IDragHandler {
     
    [Header("Context Menu")]
    [SerializeField] GameObject descriptionPrefab;
    [SerializeField] GameObject amountPrefab; 

    // Cloned identifiers from Item script  
    public Sprite icon = null; 
    public Item item;
    public bool empty;

    // Identify our slot to send icon to
    public Transform slotTransform; 

    private GameObject amountBackground;
    public void OnDrag(PointerEventData eventData) { 
        //prob can be removed
    }

    public void OnEndDrag(PointerEventData eventData) { 
        //find the slots from the list of items the pointer has hovered over
        GameObject Slots = null;
        foreach (var item in eventData.hovered) {
            if (item.name.Contains("Slot")) {
                Slots = item; 
            } 
        } 

        //this is our target slot to swap with
        Slot target = Slots.GetComponent<Slot>();
        
        //swap the icons (need to be updated to find from item)
        Sprite storedIcon = target.icon;
        target.icon = icon;
        icon = storedIcon;

        //swap the stored item data
        Item storedItem = target.item;
        target.item = item;
        item = storedItem;

        //swap the bools (need to be updated to find from item)
        bool storedEmpty = target.empty;
        target.empty = empty;
        empty = storedEmpty;

        //update both slots
        target.UpdateSlot();
        UpdateSlot();

        //log the swap (debugging)
        Debug.LogError("swap " + Slots.name + " + " + gameObject.name);
    }
    
    public void OnPointerEnter(PointerEventData eventData) { 
        //if there is a item in this slot
        if (item != null) { 
            //set the text to the description
            descriptionPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemdata.itemDescription;
            //set its position
            descriptionPrefab.transform.position = transform.position + new Vector3(-100,0,0);
            //enable it
            descriptionPrefab.SetActive(true);
        }
    } 
    public void OnPointerExit(PointerEventData eventData) {
        //when mouse moves of slot, hide description
        descriptionPrefab.SetActive(false);
    } 
    public void OnPointerClick(PointerEventData pointerEventData) { 
        if (pointerEventData.button == 0) {
            //left click to use item
            UseItem();
        } else { 
            //right click should open option menu (to be added)
            Debug.LogError("E");
        }
        //UseItem(); 
    }  
    public void Start() { 
        //Description Box 
        descriptionPrefab.SetActive(false);
              
        //amount background
        amountBackground = GameObject.Instantiate(amountPrefab);
        amountBackground.transform.SetParent(transform.GetChild(0));
        amountBackground.transform.localPosition = Vector3.zero;
        amountBackground.SetActive(false); 
    }
    public bool SpaceAvilable() {
        //check there the amount if items we store is less than our max amount
        //needs improvment to check for adding multiple quanity at once
        if (item.itemdata.maxItemQuanity > item.itemdata.itemQuanity) 
            return true;
        else 
            return false; 
    }
    public void IncreaseQuanity() {
        //add another quanitity
        item.itemdata.itemQuanity++;
    }
    public void UpdateSlot() {
        //if there is a item stored here
        if (item != null) { 
            //update our quanitity amount
            amountBackground.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemdata.itemQuanity.ToString();

            //if we store more than one item, then we show the quantity amount
            if (item.itemdata.itemQuanity > 0) {
                amountBackground.SetActive(true);
            } else {
                amountBackground.SetActive(false);
            }

        } else {
            amountBackground.SetActive(false);
        }

        //upodate the slot icon
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    public void UseItem() {
        // Do whatever ItemUsage does for this item 
        item.ItemUsage();
        Debug.Log("ItemUsed");
    } 
}
