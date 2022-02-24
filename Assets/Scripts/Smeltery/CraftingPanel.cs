using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CraftingPanel : MonoBehaviour
{
    [SerializeField] GameObject basepoint;

    [SerializeField] Slot slot1;
    [SerializeField] Slot slot2;
    [SerializeField] Slot slot3;

    [SerializeField] Slot slotResult;
    // Start is called before the first frame update
    void Start()
    {
        slot1 = transform.GetChild(0).transform.GetChild(0).GetComponent<Slot>();
        slot2 = transform.GetChild(0).transform.GetChild(1).GetComponent<Slot>();
        slot3 = transform.GetChild(0).transform.GetChild(2).GetComponent<Slot>();

        slotResult = transform.GetChild(1).GetComponent<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, basepoint.transform.position) < 3) {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        } else {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }

        gameObject.transform.position = Camera.main.WorldToScreenPoint(basepoint.transform.position);


        if (slot1.quanitity != 0  & slot2.quanitity != 0 & slot3.quanitity != 0) {
            string debug = slot1.itemdata.itemName + ", " + slot2.itemdata.itemName + ", " + slot3.itemdata.itemName;
            Debug.Log("Making items from " + debug);
        }
    }
}
