using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SmelteryController : MonoBehaviour {
    //Outputting
    public GameObject metalStream;
    public GameObject metalStreamOutput;
    public MetalCastController metalCastController;

    static SmelteryController instance;
    //Storage Amount
    public static int capacity = 20;
    static int totalValue = 0;

    public CastingPanel castingPanel;

    //Actual Data
    public static List<Metal> oreStorage = new List<Metal>();

    public static Metal[] oreDictionary = new Metal[] {
        new Metal("Iron", new Color(0.25f, 0.25f, 0.25f)),      //0
        new Metal("Copper", new Color(1f, .64f, 0f)),           //1
        new Metal("Gold", new Color(.9f, 1f, 0f)),              //2
        new Metal("Silver", new Color(1f, 1f, 1f)),             //3
        new Metal("Bronze", new Color(0.69f, 0.52f, 0.21f)),    //4
        new Metal("Admant", new Color(1f, 1f, 1f)),             //5
        new Metal("Mithirl", new Color(0.25f, 0.75f, 0.90f)),   //6
        new Metal("Orichalcum", new Color(0.45f, 0.29f, 0.17f)),//7
        new Metal("Tin", new Color(0.88f, 0.75f, 0.53f)),       //8
        new Metal("Wood", new Color(0.58f, 0.43f, .2f))         //9
    };

    public static AlloyCombinations[] Combinations = new AlloyCombinations[] {
        new AlloyCombinations("Bronze", new List<string>{"Copper", "Silver"})
    };
    private void Start() {
        instance = this;
        metalStreamOutput.SetActive(false);
    }
    #region Adding and Removing
    public static Metal SearchDictionaryForMetal(string itemType, out bool result) {
        foreach (var item in oreDictionary) {
            if (itemType == item.ToString()) {
                result = true;
                return item;
            }
        }
        result = false;
        Debug.LogWarning("Parse Error, Unknown Enum!");
        return null;
    }
    public static void AddItem(string itemType, int quantity) {
        bool worked;
        Metal metal = SearchDictionaryForMetal(itemType, out worked);
        bool availableCapacity = totalValue + quantity < capacity + 1;
        if (worked & availableCapacity) {
            if (oreStorage.Contains(metal)) {
                foreach (var item in oreStorage) {
                    if (item.ToString() == itemType) {
                        item.quantity += quantity;
                        break;
                    }
                }
            } else {
                oreStorage.Add(metal);
                Metal item = oreStorage[oreStorage.Count - 1];
                item.quantity = quantity;
                UpdateMetal(item);
            }
        }
        SmelteryDisplayPanel.UpdatePanel = true;
    }
    public static void RemItem(string itemType, int quantity) {
        bool worked;
        Metal metal = SearchDictionaryForMetal(itemType, out worked);
        if (worked) {
            if (oreStorage.Contains(metal)) {
                foreach (var item in oreStorage) {
                    if (item.ToString() == itemType) {
                        if (item.quantity - quantity < 0) {
                            Debug.LogError("you tried to use more materials than you have");
                        } else {
                            item.quantity -= quantity;
                        }

                        if (item.quantity == 0) {
                            Debug.LogError("There is no left, Remove from list");
                            oreStorage.Remove(item);
                        }
                        break;
                    }
                }

            } else {
                Debug.LogError("you tried to remove a metal that was not stored");
            }
        }
        SmelteryDisplayPanel.UpdatePanel = true;
    }
    #endregion

    private void LateUpdate() {
        int index = 0;
        totalValue = 0;

        foreach (var item in oreStorage) {
            UpdateMetal(item);
            index++;
        }
    }
    static bool ButtonPressed = false;
    IEnumerator toggleButtonPress() {
        ButtonPressed = true;
        yield return new WaitForSeconds(1.5f);
        ButtonPressed = false;
    }
    public void OutputLowestMetal() {

        if (ButtonPressed) {
            WarningMessage.SetWarningMessage("Wait", "Wait for previous cast to finish pouring");
            return;
        }

        StartCoroutine(toggleButtonPress());
        //check that its not empty

        int maxAmountToCast = CastingPanel.Casts[CastingPanel.selectedIndex].cost;

        //check that there are any metals stored
        if (oreStorage.Count == 0) {
            WarningMessage.SetWarningMessage("No metal stored", "No metals added to the smeltery, add some in to cast metal");
            return;
        }


        Metal metalToCast = oreStorage[0];
        bool validMetal = false;
        if (oreStorage[0].quantity == 0) {
            foreach (Metal item in oreStorage) {
                if (item.quantity != 0) {
                    metalToCast = item;
                    validMetal = true;
                    break;
                }
            }
        } else {
            validMetal = true;
        }

        //all metals are empty here
        if (validMetal == false) {
            WarningMessage.SetWarningMessage("No metal stored", "No metals added to the smeltery, add some in to cast metal");
            return;
        }

        if (maxAmountToCast <= metalToCast.quantity) {
            outputMetal(metalToCast, maxAmountToCast);
            metalCastController.fillTheCast(metalToCast);
        } else {
            WarningMessage.SetWarningMessage("Not enough metal", "Add more metal to cast, or select cast with a lower cost!");
        }

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
    private static void CreateNewMetalKey(Metal item) {
        item.metalObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        item.metalObject.name = item.ToString();
        item.metalObject.transform.SetParent(instance.transform);

        item.metalObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Ores/" + item.ToString());
    }
    private static void UpdateMetal(Metal item) {
        if (item.metalObject == null) {
            CreateNewMetalKey(item);
        }

        if (item.quantity == 0) {
            item.metalObject.SetActive(false);
        } else {

            if (item.metalObject != null) {
                setPositionAndScale(item.metalObject, item.quantity);
            }

            item.metalObject.SetActive(true);
            totalValue += item.quantity;
        }
    }
    private static void setPositionAndScale(GameObject target, int Value) {
        float multiplyer = 1f / capacity;
        float offset = instance.transform.localScale.y / 2;
        target.transform.localPosition = new Vector3(0, ((totalValue + (Value / 2)) * multiplyer) - offset, 0);
        target.transform.localScale = new Vector3(1, Value * multiplyer, 1);
    }
    private void outputMetal(Metal item, int Value) {
        RemItem(item.n, Value);
        foreach (var childRenderer in metalStream.GetComponentsInChildren<MeshRenderer>()) {
            childRenderer.material = item.metalObject.GetComponent<MeshRenderer>().material;
        }
        StartCoroutine(displayStream(Value));

        /*
        MO = GameObject.Instantiate(metalStreamOutput);
        MO.SetActive(true);
        MO.transform.position = metalStreamOutput.transform.position;

        if (MO.GetComponent<BucketOfMetal>().oreType == item) {
            MO.GetComponent<BucketOfMetal>().oreQuantity += Value;
        } else if (MO.GetComponent<BucketOfMetal>().oreType == null) {
            MO.GetComponent<BucketOfMetal>().oreType = item;
            MO.GetComponent<BucketOfMetal>().oreQuantity = Value;
        }
        */


    }

    static private void CheckForAlloyCombinations() {
        foreach (var item in Combinations) {

            bool craftable = true;
            int min = capacity;
            foreach (var parent in item.AlloyParents) {
                bool worked = false;
                Metal par = SearchDictionaryForMetal(parent, out worked);

                craftable = craftable && worked;
                if (worked) {
                    if (oreStorage.Contains(par)) {
                        foreach (var c in oreStorage) {
                            if (c.ToString() == parent.ToString()) {
                                if (min > c.quantity) {
                                    min = c.quantity;
                                }
                            }
                        }
                    }
                }
            }
            if (craftable) {

                int quanity = 0;
                foreach (var parent in item.AlloyParents) {
                    RemItem(parent, min);
                    quanity++;
                }

                AddItem(item.Alloy, quanity);
            }

        }
    }
}
public class Metal {
    public GameObject metalObject; 
    public int quantity = 0;
    public string n;
    public Color col;
    public Metal(string name, Color col) {
        n = name;
        this.col = col; 
    }
    public override string ToString() {
        return n; 
    }
}
public struct AlloyCombinations {
    public string Alloy;
    public List<string> AlloyParents; 
    //add in a list of int to determin percentage of each value
    public AlloyCombinations(string Alloy, List<string> AlloyParents) {
        this.Alloy = Alloy;
        this.AlloyParents = AlloyParents; 
    }
}
