using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
[System.Serializable]
public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler , IDragHandler {

    public override string ToString()
    {
        string output = "";
        output += itemdata.ToString();
        output += "," + quantity;
        return output;
    }

    public Attribute slotType = Attribute.None;

    [Header("Context Menu")]
    //[SerializeField] GameObject descriptionPrefab;
    [SerializeField] GameObject amountPrefab;

	public string slotImagePath;
       
    private GameObject amountBackground; 


    public static Slot PointerSlot;

	// Quanitity is not a Wordygurdy
    public int quantity = 0;
    public ItemData itemdata = null; 
    public GameObject objectData;
    public void OnDrag(PointerEventData eventData) { 
        //prob can be removed
        transform.GetChild(0).transform.position = Input.mousePosition;
    } 
    public void OnEndDrag(PointerEventData eventData) {
        transform.GetChild(0).transform.localPosition = Vector3.zero;
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

                if (target.itemdata == itemdata)
                { 
                    if (target.quantity != itemdata.maxItemQuanity & quantity != itemdata.maxItemQuanity)
                    {  
                        int totalQuanitity = target.quantity + quantity;

                        if (totalQuanitity > itemdata.maxItemQuanity)
                        {  
                            target.quantity = itemdata.maxItemQuanity;
                            quantity = totalQuanitity - itemdata.maxItemQuanity;
                            target.UpdateSlot();
                            UpdateSlot(); 
                        }
                        else
                        { 
                            target.quantity = totalQuanitity;
                            quantity = 0;
                            target.UpdateSlot();
                            UpdateSlot();
                        } 
                    } 
                    //quick exit :)
                    return; 
                }

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
        else
        {
            Slot target = null;
            if (PointerSlot != null)
            { 
                if (PointerSlot.TryGetComponent<Slot>(out target))
                {
                    if (target != this & target.quantity == 0)
                    {
                        Debug.LogError("Can split stack instead here " + target + " vs " + this);

                        target.itemdata = itemdata;
                        int amount = (int)Mathf.Round((float)quantity / 2f);
                        target.quantity = amount;

                        quantity = quantity - amount;
                        UpdateSlot();
                        target.UpdateSlot();
                    }
                }
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
    public static Slot WeaponSlot = null;
    public void UpdateSlot()
    {
        //if there is a item stored here
        if (SlotInUse())
        { 
            transform.GetChild(0).gameObject.SetActive(true); 
            //update our quanitity amount
            amountBackground.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quantity.ToString();

            //if we store more than one item, then we show the quantity amount
            if (quantity > 1)
            {
                amountBackground.SetActive(true);
            }
            else
            {
                amountBackground.SetActive(false);
            }

        }
        else
        {
            amountBackground.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
        }

        //update the slot icon
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        transform.GetChild(0).GetComponent<Image>().sprite = GetImage();

        if (WeaponSlot == this)
        { 
            if (slotType == Attribute.Equip1)
            {
                //we can update our weapon to this one 
                if (quantity != 0)
                { 
                    WeaponManager.SetWeapon((ItemWeapon)itemdata); 
                } else {
                    //we should remove our weapon from being displayed  
                    WeaponManager.ClearWeapon();
                    WeaponSlot = null;
                }
            }
            else
            {
                //we should remove it 
                WeaponManager.ClearWeapon();
                WeaponSlot = null;
            } 
        }
        

        Color[] datacolour = new Color[] { Color.white, Color.white, Color.white };

        if (itemdata is ArmourData)
        {

            Debug.LogError("sssssssssssssss");
            ArmourData data;
            data = (ArmourData)itemdata;
            datacolour = data.colour;
        }


        switch (slotType)
        {
            case Attribute.ArmourHead:
                Debug.LogError("You eqiuped a helm");
                ArmourManager.StaticSetArmour(Attribute.ArmourHead, SlotInUse(), datacolour);

                break;

            case Attribute.ArmourChest:
                Debug.LogError("You eqiuped a chest"); 
                ArmourManager.StaticSetArmour(Attribute.ArmourChest, SlotInUse(), datacolour);

                break;

            case Attribute.ArmourBoot:
                Debug.LogError("You eqiuped a boots"); 
                ArmourManager.StaticSetArmour(Attribute.ArmourBoot, SlotInUse(), datacolour);

                break;

            case Attribute.ArmourGloves:
                Debug.LogError("You eqiuped a gloves"); 
                ArmourManager.StaticSetArmour(Attribute.ArmourGloves, SlotInUse(), datacolour);

                break;
        }
    }
    public Sprite GetImage()
    {
        return itemdata.sprite;
    }
    public void UseItem() { 

        if (itemdata.itemAttribute == Attribute.Equip1 & quantity != 0)
        {
            WeaponManager.SetWeapon((ItemWeapon)itemdata);
            WeaponSlot = this; 
        } 
    } 
}
