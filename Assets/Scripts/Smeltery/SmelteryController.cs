using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SmelteryController : MonoBehaviour {

    public static Dictionary<Metal, int> oreStorage = new Dictionary<Metal, int>();
    public TextMeshProUGUI textOutput;
     
    public Material[] oreMaterials;
    public GameObject metalStream;
    public GameObject metalStreamOutput;

    private static Metal[] oreTypes = new Metal[] {
        new Metal("Iron", "blah blah blah"),
        new Metal("Copper", "blah blah blah"),
        new Metal("Gold", "blah blah blah"),
        new Metal("Silver", "blah blah blah"),
        new Metal("Bronze", "Blah")
    };

    static int capacity = 20;
    static int totalValue = 0;
    #region Adding and Removing
    private static Metal StringToMetal(string itemType, out bool result) {
        Metal metal = null;
        result = false;
        foreach (var item in oreTypes) {
            if (itemType == item.metalData.itemName) {
                result = true;
                metal = item;
            }
        }
        if (!result) {
            Debug.LogWarning("Parse Error, Unknown Enum!");
        }
        return metal;
    }
    public static void AddItem(string itemType, int quantity) {
        bool worked;
        Metal metal = StringToMetal(itemType, out worked);
        bool availableCapacity = totalValue + quantity < capacity + 1;
        //Debug.Log("Add " + itemType + quantity + " : Status -> " + worked + " : Capacity -> "  + availableCapacity); 
        if (worked & availableCapacity) { 
            if (oreStorage.ContainsKey(metal)) { 
                oreStorage[metal] += quantity;
            } else {
                oreStorage.Add(metal, quantity);
            }
        }
    }
    public static void RemItem(string itemType, int quantity) {
        bool worked;
        Metal metal = StringToMetal(itemType, out worked); 
        //Debug.Log("Rem " + itemType + quantity + " : Status -> " + worked); 
        if (worked) { 
            if (oreStorage.ContainsKey(metal)) { 
                if (oreStorage[metal] - quantity < 0) { 
                    Debug.LogError("you tried to use more materials than you have");
                }else {
                    oreStorage[metal] -= quantity;
                }
            } else {
                Debug.LogError("you tried to remove a metal that was not stored");
            }
        }
        
    }
    #endregion

    private void LateUpdate() {
        if (Input.GetKeyDown(KeyCode.I)) 
            CheckForAlloyCombinations();

        if (Input.GetKeyDown(KeyCode.U)) {
            bool worked;
            Metal metal = StringToMetal("Bronze", out worked);
            outputMetal(metal, 1);
        }

        textOutput.text = "Smeltery storage : \n";
        int index = 0;
        totalValue = 0;

        foreach (var item in oreStorage) {
            UpdateMetal(item.Key, item.Value);
            index++;
        }

        textOutput.text += totalValue + " ingots stored here";
    }

    IEnumerator displayStream(int value) {
        Vector3[] origScale = { metalStream.transform.GetChild(0).localScale, metalStream.transform.GetChild(1).localScale };
        Vector3[] origPosition = { metalStream.transform.GetChild(0).localPosition, metalStream.transform.GetChild(1).localPosition };
        for (int x = 0; x < 2; x++) { 
            Transform stream = metalStream.transform.GetChild(x);
            stream.gameObject.SetActive(true);
            Vector3 localScale = stream.localScale; 
            Vector3 localPosition = stream.localPosition;
            for (int i = 0; i < 20; i++) {
                float count = i / 20f;
                stream.localScale = Vector3.Lerp(new Vector3(), localScale, count);
                Vector3 or;
                if (x == 1) {
                    or = localPosition + (localScale / 2) - (stream.localScale / 2);
                } else {
                    or = localPosition - (localScale / 2) + (stream.localScale / 2);
                }
                stream.localPosition = new Vector3(or.x, or.y, localPosition.z);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        metalStreamOutput.GetComponent<Renderer>().material = metalStream.transform.GetComponentInChildren<Renderer>().material;
        yield return new WaitForSeconds(value * 0.1f);
        for (int x = 0; x < 2; x++) {
            Transform stream = metalStream.transform.GetChild(x); 
            Vector3 localScale = stream.localScale; 
            Vector3 localPosition = stream.localPosition;
            for (int i = 0; i < 20; i++) {
                float count = i / 20f;
                stream.localScale = Vector3.Lerp(localScale, new Vector3(), count);
                Vector3 or;
                if (x == 1) {
                    or = localPosition - (localScale / 2) + (stream.localScale / 2);
                } else {
                    or = localPosition + (localScale / 2) - (stream.localScale / 2);
                }
                stream.localPosition = new Vector3(or.x, or.y, localPosition.z);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            stream.gameObject.SetActive(false);
            stream.localScale = origScale[x];
            stream.localPosition = origPosition[x];
        }

        yield return new WaitForEndOfFrame();
    }
    private void CreateNewMetalKey(Metal item) {
        item.metalObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        item.metalObject.name = item.ToString();
        item.metalObject.transform.SetParent(transform); 

        bool materialFound = false;
        foreach (var material in oreMaterials) {
            if (material.name == item.ToString()) {
                item.metalObject.GetComponent<MeshRenderer>().material = material;
                materialFound = true;
                break;
            }
        }

        if (materialFound) { Debug.LogWarning("No material has been set for this metal, default used"); }
    }
    private void UpdateMetal(Metal item, int Value) {
        if (item.metalObject == null) {
            CreateNewMetalKey(item);
        }

        if (Value == 0) {
            item.metalObject.SetActive(false);
        } else {
            textOutput.text += item.ToString() + " : " + Value.ToString() + " \n";

            if (item.metalObject != null) { 
                setPositionAndScale(item.metalObject, Value); 
            }

            item.metalObject.SetActive(true);
            totalValue += Value; 
        }
    }
    private void setPositionAndScale(GameObject target, int Value) {
        float multiplyer = 1f / capacity;
        float offset = transform.localScale.y / 2;
        target.transform.localPosition = new Vector3(0, ((totalValue + (Value / 2)) * multiplyer) - offset, 0);
        target.transform.localScale = new Vector3(1, Value * multiplyer, 1);
    }

    private void outputMetal(Metal item, int Value) {
        RemItem(item.metalData.itemName, Value);
        foreach (var childRenderer in metalStream.GetComponentsInChildren<MeshRenderer>()) {
            childRenderer.material = item.metalObject.GetComponent<MeshRenderer>().material;
        } 
        StartCoroutine(displayStream(Value));

        if (metalStreamOutput.GetComponent<BucketOfMetal>().oreType == item) {
            metalStreamOutput.GetComponent<BucketOfMetal>().oreQuantity += Value;
        } else if (metalStreamOutput.GetComponent<BucketOfMetal>().oreType == null) {
            metalStreamOutput.GetComponent<BucketOfMetal>().oreType = item;
            metalStreamOutput.GetComponent<BucketOfMetal>().oreQuantity = Value;
        }
    }

    private void CheckForAlloyCombinations() { 
        foreach (var item in Combinations) { 
            string output = ""; 
            bool craftable = true;
            int min = capacity;
            foreach (var parent in item.AlloyParents) {
                bool worked = false;
                Metal par = StringToMetal(parent, out worked);
                output += parent.ToString() + " (" + worked  + ") ";
                craftable = craftable && worked;
                if (worked) {
                    if (oreStorage.ContainsKey(par)) { 
                        if (min > oreStorage[par]) {
                            min = oreStorage[par];
                        }
                    }
                }
            }
            if (craftable) {
                output += " craftable for : " + min;
                //Debug.Log(item.Alloy + " -> " + output);
                int quanity = 0;
                foreach (var parent in item.AlloyParents) {
                    RemItem(parent, min);
                    quanity++; 
                }
                AddItem(item.Alloy, quanity);
            }
             
        }
    } 
    AlloyCombinations[] Combinations = new AlloyCombinations[] {
        new AlloyCombinations("Bronze", new List<string>{"Copper", "Silver"})
    };
}
public class Metal {
    public GameObject metalObject;
    public ItemData metalData;
    public Metal(string name, string description) {
        metalData.itemName = name;
        metalData.itemDescription = description;
        metalData.itemAttribute = Attribute.Metal;
    }
    public override string ToString() {
        return metalData.itemName;
    }
}
public class AlloyCombinations {
    public string Alloy;
    public List<string> AlloyParents; 
    public AlloyCombinations(string Alloy, List<string> AlloyParents) {
        this.Alloy = Alloy;
        this.AlloyParents = AlloyParents; 
    }
}
