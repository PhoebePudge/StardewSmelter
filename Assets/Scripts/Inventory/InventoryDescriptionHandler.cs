using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryDescriptionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        Slot slot = Slot.PointerSlot;

        if (slot != null) {
            if (slot.quantity != 0) { 
                SetActive(true);
                gameObject.transform.position = Input.mousePosition + new Vector3(-100, 0);
                //item name
                gameObject.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = slot.itemdata.itemName;

                //item Quantity
                gameObject.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = slot.quantity.ToString();

                //type
                gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "type: " + slot.itemdata.itemAttribute.ToString();

                //Description
                gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = slot.itemdata.itemDescription;

            } else {
                SetActive(false);
            }
        } else {
            SetActive(false);
        }
    }
    void SetActive(bool active) {
        foreach (Transform item in transform) {
            item.gameObject.SetActive(active);
        }
        gameObject.GetComponent<Image>().enabled = active;

    }
}
