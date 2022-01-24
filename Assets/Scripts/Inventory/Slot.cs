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
    }
    public void OnEndDrag(PointerEventData eventData) { 
        GameObject Slots = null;
        foreach (var item in eventData.hovered) {
            if (item.name.Contains("Slot")) {
                Slots = item; 
            } 
        } 
        Slot target = Slots.GetComponent<Slot>();
        
        Sprite storedIcon = target.icon;
        target.icon = icon;
        icon = storedIcon;

        Item storedItem = target.item;
        target.item = item;
        item = storedItem;

        bool storedEmpty = target.empty;
        target.empty = empty;
        empty = storedEmpty;

        target.UpdateSlot();
        UpdateSlot();

        Debug.LogError("swap " + Slots.name + " + " + gameObject.name);
    }
    
    public void OnPointerEnter(PointerEventData eventData) { 
        
        if (item != null) { 
            descriptionPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemdata.itemDescription;
            descriptionPrefab.transform.position = transform.position + new Vector3(-100,0,0);
            descriptionPrefab.SetActive(true);
        }
    } 
    public void OnPointerExit(PointerEventData eventData) {
        descriptionPrefab.SetActive(false);
    } 
    public void OnPointerClick(PointerEventData pointerEventData) { 
        if (pointerEventData.button == 0) {
            UseItem();
        } else { 
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
        if (item.itemdata.maxItemQuanity > item.itemdata.itemQuanity) {
            return true;
        } else {
            return false;
        }
    }
    public void IncreaseQuanity() {
        item.itemdata.itemQuanity++;
    }
    public void UpdateSlot() {
        //data display
        if (item != null) { 
            amountBackground.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemdata.itemQuanity.ToString();

            if (item.itemdata.itemQuanity > 0) {
                amountBackground.SetActive(true);
            } else {
                amountBackground.SetActive(false);
            }
        } else {
            amountBackground.SetActive(false);
        }

        //slot icon
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    public void UseItem() {
        // Do whatever ItemUsage does for this item 
        item.ItemUsage();
        Debug.Log("ItemUsed");
    } 
}
