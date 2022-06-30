using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SmelteryDisplayPanel : MonoBehaviour {
    public Transform pivotPoint;
    public Slot inputSlot;

    public SmelteryController controller;
    public Vector3 PositionOffset;
    private TextMeshProUGUI ContentTextOutput;
    private TextMeshProUGUI CombinationTextOutput;
    private Image ContentImageOutput;

    public ColourOutline colourOutline;

    public static int SelectedMetalIndex = 0;
    public GameObject LabelGM;
    public static Transform mouseTarget = null;
    public static int mouseIndex;

    public GameObject tooltopGM;
    GameObject ContentPointer;
    public static bool UpdatePanel = true;
    List<GameObject> SmelteryLabels = new List<GameObject>();

    bool animating = false;
    static SmelteryDisplayPanel instance;
    private void OnEnable() {
        if (instance == null) {
            instance = this;
        }
        if (instance != this) {
            instance.pivotPoint = pivotPoint;
            instance.controller = controller;
            instance.colourOutline = colourOutline;

            Debug.LogError(controller);
            instance.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            instance.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => SmelteryController.instance.OutputLowestMetal());

            Destroy(this);
        }
    }
    //increase metal selection
    public void IncreaseSelectedMetal() {
        if (SelectedMetalIndex + 1 < SmelteryController.oreStorage.Count) {
            Metal A = SmelteryController.oreStorage[SelectedMetalIndex];
            Metal B = SmelteryController.oreStorage[SelectedMetalIndex + 1];

            SmelteryController.oreStorage[SelectedMetalIndex + 1] = A;
            SmelteryController.oreStorage[SelectedMetalIndex] = B;

            UpdatePanel = true;
        }
        SmelteryDisplayPanel.UpdatePanel = true;
    }
    //decrease metal selection
    public void DecreaseSelectedMetal() {
        if (SelectedMetalIndex > 0) {
            Metal A = SmelteryController.oreStorage[SelectedMetalIndex];
            Metal B = SmelteryController.oreStorage[SelectedMetalIndex - 1];

            SmelteryController.oreStorage[SelectedMetalIndex - 1] = A;
            SmelteryController.oreStorage[SelectedMetalIndex] = B;

            UpdatePanel = true;
        }
        SmelteryDisplayPanel.UpdatePanel = true;
    }
    void Start() {
        //set variables over on start
        ContentTextOutput = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        CombinationTextOutput = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        ContentImageOutput = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>();
        ContentPointer = transform.GetChild(0).GetChild(3).GetChild(1).gameObject;
        SmelteryDisplayPanel.UpdatePanel = true;
    }
    IEnumerator SmeltAnimation(int amount) {
        //if we are not already playing this animation
        if (!animating) {
            //loop through each ore
            for (int i = 0; i < amount + 1; i++) {
                animating = true;
                //update slider
                inputSlot.transform.GetChild(1).GetComponent<Slider>().value = (i) / (float)amount;

                if (i != 0) {
                    //remove one from metal, and add new metal to smeltery
                    SmelteryController.AddItem(inputSlot.itemdata.itemName, 1);
                    inputSlot.quantity--;
                    inputSlot.UpdateSlot();
                }

                //wait
                yield return new WaitForSeconds(.5f);
            }

            //reset slider
            inputSlot.transform.GetChild(1).GetComponent<Slider>().value = 0;
            animating = false;
        }
    }
    IEnumerator LerpSize(int a, int b) {
        //lerp size from small to big
        float time = 0f;

        while (time < b) {
            time += 0.05f;
            float progress = Mathf.Lerp(a, b, time);

            transform.localScale = new Vector3(progress, progress, time);

            yield return new WaitForSeconds(0.005f);
        }
    }
    IEnumerator DecreaseLerp(int a, int b) {
        //lerp size from big to small
        float time = 0f;
        while (time < a) {
            time += 0.1f;
            float progress = Mathf.Lerp(a, b, time);

            transform.localScale = new Vector3(progress, progress, time);

            yield return new WaitForSeconds(0.005f);
        }

        foreach (Transform child in gameObject.transform) {
            child.gameObject.SetActive(false);
        }
    }

    public void ButtonClick(GameObject gm) {
        Debug.LogError("Clicked " + gm.name);
    }
    void Update() {
        //if we have a pivot point
        if (pivotPoint != null) {
            //if we have a mouse target
            if (mouseTarget != null) {
                //set tooltip
                tooltopGM.SetActive(true);
                tooltopGM.transform.position = Input.mousePosition;

                tooltopGM.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = SmelteryController.oreStorage[mouseIndex].n;
                tooltopGM.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = SmelteryController.oreStorage[mouseIndex].quantity.ToString();
            } else {
                tooltopGM.SetActive(false);
            }

            //if our slot is in use, start smelt animation
            if (inputSlot.SlotInUse()) {
                StartCoroutine(SmeltAnimation(inputSlot.quantity));
            }

            //if we update the panel
            if (UpdatePanel) { 
                if (SmelteryLabels.Count == 0) {
                    ContentPointer.SetActive(false);
                } else {
                    ContentPointer.SetActive(true);
                }


                //get total ingots stored
                string text = "";
                int amount = 0;
                int index = 0;

                //foreach ores stored
                foreach (var item in SmelteryController.oreStorage) {
                    //get our text output
                    if (index == SelectedMetalIndex) {
                        text += "<b>";
                    }
                    text += item.ToString() + " : " + item.quantity.ToString() + " \n";
                    amount += item.quantity;
                    if (index == SelectedMetalIndex) {
                        text += "</b>";
                    }
                    index++;
                }
                //text += amount + " ingots stored here out of " + SmelteryController.capacity; 
                ContentTextOutput.text = text;

                //if there are too little labels
                if (SmelteryLabels.Count < SmelteryController.oreStorage.Count) {
                    while (SmelteryLabels.Count < SmelteryController.oreStorage.Count) {
                        //Debug.LogError(SmelteryLabels.Count  + " vs " +  SmelteryController.oreStorage.Count);
                        GameObject gm = GameObject.Instantiate(LabelGM);
                        gm.name = "Contents " + SmelteryController.oreStorage.Count;

                        SmelteryLabels.Add(gm);
                        gm.transform.SetParent(ContentImageOutput.transform);
                        gm.AddComponent<SmelteryButton>();
                        gm.GetComponent<SmelteryButton>().index = SmelteryController.oreStorage.Count - 1;
                        gm.SetActive(true);
                    }
                }

                //to many labels, set them to inactive
                if (SmelteryLabels.Count > SmelteryController.oreStorage.Count) {
                    for (int i = SmelteryController.oreStorage.Count; i < SmelteryLabels.Count; i++) {
                        SmelteryLabels.RemoveAt(i);
                        i--;
                    }
                }

                //set to only update on each new metal added
                Texture2D texture = new Texture2D(1, 20);

                index = 0;
                int z = 0;

                //foreach ore storage
                foreach (var item in SmelteryController.oreStorage) {
                    if (item.metalObject != null) {
                        Color col = item.metalObject.GetComponent<Renderer>().material.color;
                        for (int i = 0; i < item.quantity; i++) {
                            texture.SetPixel(0, index + i, col);
                        }

                        if (SelectedMetalIndex == z) {
                            ContentPointer.transform.localPosition = new Vector3(-73f, ((index + (SmelteryController.oreStorage[z].quantity / 2f)) - 10f) * 3.15f * 1.5f);
                        }
                    }
                    //set active
                    SmelteryLabels[z].gameObject.SetActive(true);

                    //update ore name on label
                    SmelteryLabels[z].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SmelteryController.oreStorage[z].n;

                    //set position and scale
                    SmelteryLabels[z].transform.localPosition = new Vector3(0, ((index + (SmelteryController.oreStorage[z].quantity / 2f)) - 10f) * 3.15f * 1.5f);
                    SmelteryLabels[z].GetComponent<RectTransform>().sizeDelta = new Vector2(76 * 2.4f, item.quantity * 4.85f * 1.5f);
                    SmelteryLabels[z].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.n;

                    index += item.quantity;
                    z++;
                }

                //set pixel for smeltery display texture
                while (index < 20) {
                    texture.SetPixel(0, index, new Color(0, 0, 0, 0));
                    index++;
                }

                //apply texture
                texture.Apply();
                texture.filterMode = FilterMode.Point;

                //output texture
                ContentImageOutput.sprite = Sprite.Create(texture, new Rect(0, 0, 1, 20), new Vector2());
                CombinationTextOutput.text = "";

                foreach (var item in SmelteryController.Combinations) {
                    //check for alloy combinations
                    bool containedSource = false;
                    string t = "";
                    for (int i = 0; i < item.AlloyParents.Count; i++) {
                        foreach (var source in item.AlloyParents) {
                            foreach (var stored in SmelteryController.oreStorage) {
                                if (source == stored.n) {
                                    containedSource = true;
                                }
                            }
                        }
                        t += item.AlloyParents[i];
                        if (i + 1 < item.AlloyParents.Count) {
                            t += " + ";
                        }

                    }
                    if (containedSource) {
                        t += " = " + item.Alloy;
                        CombinationTextOutput.text += t + "\n";
                    }
                }

                TextMeshProUGUI textMeshProUGUI = transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();
                float percent = ((float)amount / (float)SmelteryController.capacity) * 100;
                textMeshProUGUI.text = percent.ToString() + "%";

                UpdatePanel = false;
            }

            //Distance active
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 2) {
                //when switched to active
                if (isShown == false) {
                    StartCoroutine(LerpSize(0, 1));
                    foreach (Transform child in gameObject.transform) {
                        child.gameObject.SetActive(true);
                    }
                    colourOutline.ColourChange(true);
                    colourOutline.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Pause();
                    CombinationTextOutput.transform.parent.gameObject.SetActive(true);
                }

                isShown = true;
            } else {

                //when switched to disabled
                if (isShown == true) {
                    StartCoroutine(DecreaseLerp(1, 0));
                    colourOutline.ColourChange(false);
                    colourOutline.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                    tooltopGM.SetActive(false);
                }

                isShown = false;
            }
        }
    }
    bool isShown = true;
}
