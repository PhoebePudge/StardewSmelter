using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryDescriptionHandler : MonoBehaviour
{
    private bool overSlot = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        Slot slot = Slot.PointerSlot;

        if (slot != null) {
            if (slot.quanitity != 0) { 
                SetActive(true);
                gameObject.transform.position = Input.mousePosition + new Vector3(-100, 0);
                gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = slot.itemdata.itemName;
                gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = slot.itemdata.itemDescription;

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
