using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StorageChest : MonoBehaviour
{
    public Transform pivotPoint;
    public static List<Slot> StorageSlots;
    public static StorageChest instance = null;
    // Start is called before the first frame update
    void Start()
    {

        if (StorageSlots == null)
        {
            StorageSlots = new List<Slot>();
            foreach (Transform item in gameObject.transform.GetChild(0).transform)
            {
                StorageSlots.Add(item.GetComponent<Slot>());
            }
            instance = this;
        }
        else
        {
            instance.pivotPoint = pivotPoint;
            Destroy(this);
        }


        //if (StorageSlots == null)
        //{
        //    Debug.LogError("new data");
        //    Storage is empty, setup variables
        //    StorageSlots = new List<Slot>();

        //    foreach (Transform item in gameObject.transform.GetChild(0).transform)
        //    {
        //        StorageSlots.Add(item.GetComponent<Slot>());
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Load old data");
        //    update slots from static variable
        //    int i = 0;
        //    foreach (Transform item in gameObject.transform.GetChild(0).transform)
        //    {
        //        if (StorageSlots[i].quantity != 0)
        //        {
        //            item.GetComponent<Slot>().itemdata = StorageSlots[i].itemdata;
        //            item.GetComponent<Slot>().quantity = StorageSlots[i].quantity;
        //            item.GetComponent<Slot>().UpdateSlot();
        //        }
        //        i++;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (pivotPoint.transform != null)
        { 
            gameObject.transform.position = Camera.main.WorldToScreenPoint(pivotPoint.transform.position);


            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 3)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
