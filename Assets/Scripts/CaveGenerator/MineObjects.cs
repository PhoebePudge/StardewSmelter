using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObjects : MonoBehaviour
{ 
    public int itemType = 0;
    public int PickaxeLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addItem(int pickaxeLevel)
    {
        if (pickaxeLevel >= PickaxeLevel)
        {
            //Debug.LogError("You can mine it");
        }
        else
        {
            WarningMessage.SetWarningMessage("Pickaxe too weak", "This ore requires a mining level of " + PickaxeLevel + " to mine");
            return;
        }
        GameObject go = new GameObject("go");
        InventorySystem.AddItem(go, InventorySystem.itemList[itemType]);

        StartCoroutine(ObjectAnimation());
        
    }
    IEnumerator ObjectAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 30; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, i / 30f);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
public struct PickaxeLevels
{
    public string name;
    public int level;

    public PickaxeLevels(string name, int level)
    {
        this.name = name;
        this.level = level;
    }
}
