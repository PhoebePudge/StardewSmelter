using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObjects : MonoBehaviour {
    public int itemType = 0;
    public int PickaxeLevel;
    public void addItem(int pickaxeLevel) {
        //pickaxe level is too weak. return and send warning message.
        if (pickaxeLevel >= PickaxeLevel) {
        } else {
            WarningMessage.SetWarningMessage("Pickaxe too weak", "This ore requires a mining level of " + PickaxeLevel + " to mine");
            return;
        }

        //add in mined object to inventory
        GameObject go = new GameObject("go");
        InventorySystem.AddItem(go, InventorySystem.itemList[itemType]); 
        StartCoroutine(ObjectAnimation()); 
    }
    IEnumerator ObjectAnimation() {
        //animate the locale scale from big to zero
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 30; i++) {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, i / 30f);
            yield return new WaitForSeconds(0.05f);
        }

        //destroy this object
        Destroy(gameObject);
    }
}
public struct PickaxeLevels {
    public string name;
    public int level;

    public PickaxeLevels(string name, int level) {
        this.name = name;
        this.level = level;
    }
}
