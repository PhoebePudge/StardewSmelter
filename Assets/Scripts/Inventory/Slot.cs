using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler , IDragHandler {
    public Attribute slotType = Attribute.None;

    [Header("Context Menu")]
    //[SerializeField] GameObject descriptionPrefab;
    [SerializeField] GameObject amountPrefab;

	public string slotImagePath;
       
    private static bool displayDescription = false; 
    private GameObject amountBackground; 


    public static Slot PointerSlot;

	// Quanitity is not a Wordygurdy
    public int quantity = 0;
    public ItemData itemdata = null; 
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
        if (Slots != null) { 
            Slot target = Slots.GetComponent<Slot>();  

            //do the actual swap
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
                int storedQuanitity = target.quantity;
                target.quantity = quantity;
                quantity = storedQuanitity;

                //update both slots
                target.UpdateSlot();
                UpdateSlot();
            }
        }
    } 
    public void OnPointerEnter(PointerEventData eventData) {
        PointerSlot = this; 
    }
    public void OnPointerExit(PointerEventData eventData) {
        PointerSlot = null;
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
        //descriptionPrefab.SetActive(false);
              
        //amount background
        amountBackground = GameObject.Instantiate(amountPrefab);
        amountBackground.transform.SetParent(transform.GetChild(0), false);
        amountBackground.transform.localPosition = Vector3.zero;
        amountBackground.SetActive(false); 
    }
    public bool SpaceAvilable(int amount = 1) { 
        //check there the amount if items we store is less than our max amount
        //needs improvment to check for adding multiple quanity at once
        if (itemdata.maxItemQuanity + 1 > quantity + amount) 
            return true;
        else 
            return false; 
    }
    public void IncreaseQuanity(int amount = 1) {
        //add another quanitity
        quantity += amount;
    }
    public bool SlotInUse() {
        return quantity != 0;
    }
    public void UpdateSlot() { 
        //if there is a item stored here
        if (SlotInUse()) {

            transform.GetChild(0).gameObject.SetActive(true);

            //update our quanitity amount
            amountBackground.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quantity.ToString();

            //if we store more than one item, then we show the quantity amount
            if (quantity > 1) {
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
        transform.GetChild(0).GetComponent<Image>().sprite = GetImage();


        switch (slotType)
        {
            case Attribute.ArmourHead:
                break;
            case Attribute.ArmourChest:
                break;
            case Attribute.ArmourBoot:
                break;
            case Attribute.ArmourGloves:
                break;
            case Attribute.CraftingPart:
                break;
            case Attribute.Equip1: // swords and tools
                if (quantity == 0)
                {
                    WeaponManager.ClearTexture();
                }
                else
                {
                    WeaponManager.SetTexture((ItemWeapon)itemdata);
                }
                break;
            case Attribute.Equip2:
                break;
            case Attribute.Damage:
                break;
            case Attribute.Defence:
                break;
            case Attribute.Health:
                break;
            case Attribute.Object:
                break;
            case Attribute.Metal:
                break;
            case Attribute.None:
                break;
            default:
                break;
        }
        //if (quanitity == 0) {
        //    transform.GetChild(0).GetComponent<Image>().sprite = null;
        //}
    }
    public Sprite GetImage()
    {
        return itemdata.sprite;
    }
    public void UseItem() {
        //// Do whatever ItemUsage does for this item  
        //Debug.Log("ItemUsed");
        //if (itemdata.itemAttribute == Attribute.Metal) {
        //    SmelteryController.AddItem(itemdata.itemName, quantity);

        //    //Clear this slot
        //    quantity--;
        //    UpdateSlot();
        //}
    } 
}
