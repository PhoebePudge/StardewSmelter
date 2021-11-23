using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SmelteryController : MonoBehaviour {

    public static Dictionary<Metals, int> oresStored = new Dictionary<Metals, int>();
    public TextMeshProUGUI output;

    public static void AddItem(string itemType, int quantity) { 
        bool worked;
        Metals metal = StringToMetal(itemType, out worked);

        Debug.Log("Add " + itemType + quantity + " : Status -> " + worked); 

        if (worked) { 
            if (oresStored.ContainsKey(metal)) {
                oresStored[metal] += quantity;
            } else {
                oresStored.Add(metal, quantity);
            }
        }
    }

    public static void RemItem(string itemType, int quantity) { 
        bool worked;
        Metals metal = StringToMetal(itemType, out worked);

        Debug.Log("Rem " + itemType + quantity + " : Status -> " + worked);

        if (worked) { 
            if (oresStored.ContainsKey(metal)) {
                oresStored[metal] -= quantity;
                if (oresStored[metal] <= 0) { 
                    Debug.LogError("you tried to use more materials than you have");
                }
            } else {
                Debug.LogError("you tried to use more materials than you have");
            }
        }
        
    }

    private static GameObject[] visualMetals = new GameObject[4];
    public Material[] metalMaterials = new Material[4];

    int lastKnownTotalHeight = 1;
    private void Update() { 
        output.text = "Smeltery storage : \n";
        int index = 0;
        int height = 0;
        float multipler = 1f / (float)(lastKnownTotalHeight); 
        foreach (var item in oresStored) {
            if (item.Value == 0) {
                visualMetals[index].SetActive(false);
                oresStored.Remove(item.Key);
            } else { 
                height += item.Value;
                output.text += item.Key.ToString() + " : " + item.Value.ToString() + " \n";

                if (visualMetals[index] == null) {
                    visualMetals[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    visualMetals[index].name = item.Key.ToString();
                    visualMetals[index].transform.SetParent(transform);

                    //height and scale is somehow messed up
                    visualMetals[index].transform.localPosition = new Vector3(0, (height - (item.Value / 2)), 0);
                    visualMetals[index].transform.localScale = new Vector3(1, item.Value, 1);

                    //change to less intensive method (dictionary?)
                    bool materialFound = false;
                    foreach (var material in metalMaterials) {
                        if (material.name == item.Key.ToString()) {
                            visualMetals[index].GetComponent<MeshRenderer>().material = material;
                            materialFound = true;
                            break;
                        }
                    }
                     
                    if (materialFound) {
                        Debug.LogWarning("No material has been set for this metal, default used");
                    }
                } else {
                    visualMetals[index].transform.localPosition = new Vector3(0, height * multipler, 0);
                    visualMetals[index].transform.localScale = new Vector3(1, item.Value * multipler, 1);
                } 
                visualMetals[index].SetActive(true);
            }
            index++;
        }

        output.text += height + " ingots stored here";
        lastKnownTotalHeight = height;
    }



    private static Metals StringToMetal(string itemType, out bool result) {
        Metals metal;
        result = System.Enum.TryParse(itemType, true, out metal);

        if (!result) {
            Debug.LogError("Parse Error, Unknown Enum!");
        }

        return metal;
    }
}

[System.Serializable] public enum Metals {
    Iron,
    Copper,
    Gold,
    Silver
}
