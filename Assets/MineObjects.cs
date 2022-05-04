using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObjects : MonoBehaviour
{
    public int itemType = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addItem()
    {
        Debug.LogError("sssssss");
        GameObject go = new GameObject("go");
        InventorySystem.AddItem(go, InventorySystem.itemList[itemType]);
        Destroy(gameObject);
    }
}
