using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler , IDragHandler {
    public Attribute slotType = Attribute.None;

    [Header("Context Menu")]
    [SerializeField] GameObject descriptionPrefab;
    [SerializeField] GameObject amountPrefab;

	public string slotImagePath;
       
    private static bool displayDescription = false; 
    private GameObject amountBackground; 
    private static Slot descriptionSlot;

	// Quanitity is not a Wordygurdy
    public int quanitity = 0;
    public ItemData itemdata; 
    public GameObject objectData;

    
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


        if (target.slotType == Attribute.None | target.slotType == itemdata.itemAttribute) {
            //swap the icons (need to be updated to find from item)
            Sprite storedIcon = target.transform.GetChild(0).GetComponent<Image>().sprite;
            target.transform.GetChild(0).GetComponent<Image>().sprite = transform.GetChild(0).GetComponent<Image>().sprite;
            transform.GetChild(0).GetComponent<Image>().sprite = storedIcon;

            //swap the stored item data
            ItemData storedItem = target.itemdata;
            target.itemdata = itemdata;
            itemdata = storedItem;
              
            //swap the bools (need to be updated to find from item)
            int storedQuanitity = target.quanitity;
            target.quanitity = quanitity;
            quanitity = storedQuanitity;

            //update both slots
            target.UpdateSlot();
            UpdateSlot();

            //log the swap (debugging)
            Debug.LogError("swap " + Slots.name + " + " + gameObject.name);
        }
    } 
    public void OnPointerEnter(PointerEventData eventData) { 
        //if there is a item in this slot
        if (quanitity != 0) {
            displayDescription = true;
            descriptionSlot = this;
            StartCoroutine(ShowDescription());
        }
    }
    IEnumerator ShowDescription() {
        yield return new WaitForSeconds(2f);
        if (displayDescription & descriptionSlot == this) {
            //set the text to the description
            descriptionPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = descriptionSlot.itemdata.itemDescription;
            descriptionPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = descriptionSlot.itemdata.itemName;
            //set its position
            descriptionPrefab.transform.position = descriptionSlot.gameObject.transform.position + new Vector3(-100, 0, 0);
            //enable it
            descriptionPrefab.SetActive(true);
        }
        yield return new WaitForEndOfFrame(); // not sure if that is needed
    }
    public void OnPointerExit(PointerEventData eventData) {
        //when mouse moves of slot, hide description
        displayDescription = false;
        StopCoroutine(ShowDescription());
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
        amountBackground.transform.SetParent(transform.GetChild(0), false);
        amountBackground.transform.localPosition = Vector3.zero;
        amountBackground.SetActive(false); 
    }
    public bool SpaceAvilable(int amount = 1) { 
        //check there the amount if items we store is less than our max amount
        //needs improvment to check for adding multiple quanity at once
        if (itemdata.maxItemQuanity + 1 > quanitity + amount) 
            return true;
        else 
            return false; 
    }
    public void IncreaseQuanity(int amount = 1) {
        //add another quanitity
        quanitity += amount;
    }
    public void UpdateSlot() { 
        //if there is a item stored here
        if (quanitity != 0) {
            transform.GetChild(0).gameObject.SetActive(true);
            //update our quanitity amount
            amountBackground.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quanitity.ToString();

            //if we store more than one item, then we show the quantity amount
            if (quanitity > 1) {
                amountBackground.SetActive(true); 
            } else {
                amountBackground.SetActive(false);
            }

        } else { 
            amountBackground.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
        } 
        //update the slot icon
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(itemdata.imagePath);


        //if (quanitity == 0) {
        //    transform.GetChild(0).GetComponent<Image>().sprite = null;
        //}
    }
    public void UseItem() {
        // Do whatever ItemUsage does for this item  
        Debug.Log("ItemUsed");
        if (itemdata.itemAttribute == Attribute.Metal) {
            SmelteryController.AddItem(itemdata.itemName, quanitity);

            //Clear this slot
            quanitity--;
            UpdateSlot();
        }
    } 
}
