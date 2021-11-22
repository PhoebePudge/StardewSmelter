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
                    //show and error if trying to use more metal than you have 
                }
            } else {
                //no metal remaining
            }
        }
        
    }

    private static GameObject[] visualMetals = new GameObject[4];
    public Material[] metalMaterials = new Material[4];

    private void Update() {
        output.text = "";
        int index = 0;
        int height = 0;
        float multipler = .1f;
        foreach (var item in oresStored) {

            if (visualMetals[index] == null) {
                visualMetals[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                visualMetals[index].name = item.Key.ToString();
                visualMetals[index].transform.SetParent(transform);

                //height and scale is somehow messed up
                visualMetals[index].transform.localPosition = new Vector3(0,(height + (item.Value / 2)) * multipler, 0); 
                visualMetals[index].transform.localScale = new Vector3(1, item.Value * multipler, 1);

                //change to less intensive method (dictionary?)
                foreach (var material in metalMaterials) {
                    if (material.name == item.Key.ToString()) {
                        visualMetals[index].GetComponent<MeshRenderer>().material = material;
                        break;
                    }
                } 

                //add in case for material not found


            } else { 
                visualMetals[index].transform.localPosition = new Vector3(0, height * multipler, 0);
                visualMetals[index].transform.localScale = new Vector3(1, item.Value * multipler, 1);
            }

            if (item.Value == 0) {
                visualMetals[index].SetActive(false);
                oresStored.Remove(item.Key); 
            } else {
                visualMetals[index].SetActive(true);
                index++;
                height += item.Value;
                output.text += item.Key.ToString() + " : " + item.Value.ToString() + " \n";
            }
        }
    }



    private static Metals StringToMetal(string itemType, out bool result) {
        Metals metal;
        result = System.Enum.TryParse(itemType, true, out metal);

        if (!result) {
            Debug.LogError("Parse Error, Unknown Enum!");
        }
        //handle no enum being found
        return metal;
    }
}

[System.Serializable] public enum Metals {
    Iron,
    Copper,
    Gold,
    Silver
}
