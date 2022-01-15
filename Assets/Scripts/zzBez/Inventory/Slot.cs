using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public bool empty;
    public int ID;
    public string type;
    public string description;
    public Sprite icon;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
        Debug.Log("PointerClick");
    }

    public Transform slotIconGO;

    public void Start()
    {
        slotIconGO = transform.GetChild(0);
    }
    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }

    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage();
        Debug.Log("ItemUsed");
    }
    
}
