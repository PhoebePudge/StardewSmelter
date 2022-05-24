using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SmelteryDisplayPanel : MonoBehaviour
{
    public Transform pivotPoint; 
    public Slot inputSlot;

    public SmelteryController controller;
    public Vector3 PositionOffset;
    private TextMeshProUGUI ContentTextOutput;
    private TextMeshProUGUI CombinationTextOutput;
    private Image ContentImageOutput;

    public GameObject outlineObject;

    public int SelectedMetalIndex = 0;

    public void IncreaseSelectedMetal()
    {
        SmelteryDisplayPanel.UpdatePanel = true;

        if (SmelteryController.oreStorage.Count > SelectedMetalIndex + 1)
        { SelectedMetalIndex++; }
    }

    public void DecreaseSelectedMetal()
    {
        SmelteryDisplayPanel.UpdatePanel = true;

        if (SelectedMetalIndex > 0)
        { SelectedMetalIndex--; }
    }
    GameObject LabelGM;
    GameObject ContentPointer;

    // Start is called before the first frame update
    void Start()
    { 
        ContentTextOutput = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        CombinationTextOutput = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        ContentImageOutput = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>();
        ContentPointer = transform.GetChild(0).GetChild(3).GetChild(1).gameObject;
        LabelGM = transform.GetChild(6).gameObject;

        SmelteryDisplayPanel.UpdatePanel = true;
    }
    public static bool UpdatePanel = true;
    List<GameObject> SmelteryLabels = new List<GameObject>();

    bool animating = false;
    IEnumerator SmeltAnimation(int amount)
    {
        if (!animating)
        {
            for (int i = 0; i < amount + 1; i++)
            {
                animating = true;
                inputSlot.transform.GetChild(1).GetComponent<Slider>().value = (i) / (float)amount;

                if (i != 0)
                { 
                    SmelteryController.AddItem(inputSlot.itemdata.itemName, 1);
                    inputSlot.quantity--;
                    inputSlot.UpdateSlot();
                }
                yield return new WaitForSeconds(.5f);
            }
            inputSlot.transform.GetChild(1).GetComponent<Slider>().value = 0;
            animating = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (pivotPoint != null)
        {
            //check for input
            if (inputSlot.SlotInUse())
            {
                //add in inputted item and remove its quantity
                //SmelteryController.AddItem(inputSlot.itemdata.itemName, inputSlot.quantity);
                StartCoroutine(SmeltAnimation(inputSlot.quantity));


                //inputSlot.quantity = 0;
                //inputSlot.UpdateSlot();

                
            }

            //set its screen position
            gameObject.transform.position = Camera.main.WorldToScreenPoint(pivotPoint.transform.position) + PositionOffset;

            if (UpdatePanel)
            {
                //Debug.LogError("You just updated the panel");


                //get total ingots stored
                string text = "";
                int amount = 0;
                int index = 0;


                foreach (var item in SmelteryController.oreStorage)
                {
                    if (index == SelectedMetalIndex)
                    {
                        text += "<b>";
                    }
                    text += item.ToString() + " : " + item.quantity.ToString() + " \n";
                    amount += item.quantity;
                    if (index == SelectedMetalIndex)
                    {
                        text += "</b>";
                    }
                    index++;
                }
                //text += amount + " ingots stored here out of " + SmelteryController.capacity; 
                ContentTextOutput.text = text;


                 
                //if there are too little labels
                if (SmelteryLabels.Count < SmelteryController.oreStorage.Count)
                {
                    while (SmelteryLabels.Count < SmelteryController.oreStorage.Count)
                    {
                        //Debug.LogError(SmelteryLabels.Count  + " vs " +  SmelteryController.oreStorage.Count);
                        GameObject gm = GameObject.Instantiate(LabelGM);
                        SmelteryLabels.Add(gm);
                        gm.transform.SetParent(ContentImageOutput.transform);
                        gm.SetActive(true);
                    }
                }

                //to many labels, set them to inactive
                if (SmelteryLabels.Count > SmelteryController.oreStorage.Count)
                {
                    for (int i = SmelteryController.oreStorage.Count; i < SmelteryLabels.Count; i++)
                    {
                        SmelteryLabels.RemoveAt(i);
                        i--;
                    }
                }




                //set to only update on each new metal added
                Texture2D texture = new Texture2D(1, 20);

                index = 0;
                int z = 0;
                foreach (var item in SmelteryController.oreStorage)
                {
                    //Debug.LogError(item.n);
                    if (item.metalObject != null)
                    {
                        Color col = item.metalObject.GetComponent<Renderer>().material.color;
                        for (int i = 0; i < item.quantity; i++)
                        {
                            texture.SetPixel(0, index + i, col);
                        }
                        
                        if (SelectedMetalIndex == z)
                        {
                            ContentPointer.transform.localPosition = new Vector3(-73f, ((index + (SmelteryController.oreStorage[z].quantity / 2f)) - 10f) * 3.15f * 1.5f);
                        }
                    }

                    SmelteryLabels[z].gameObject.SetActive(true);
                    SmelteryLabels[z].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SmelteryController.oreStorage[z].n;
                    SmelteryLabels[z].transform.localPosition = new Vector3(0, ((index + (SmelteryController.oreStorage[z].quantity / 2f)) - 10f) * 3.15f * 1.5f);
                    SmelteryLabels[z].GetComponent<RectTransform>().sizeDelta = new Vector2(76 * 2.4f, item.quantity * 4.85f * 1.5f);//4.32f
                    SmelteryLabels[z].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.n;


                    index += item.quantity;
                    z++;
                }

                while (index < 20)
                {
                    texture.SetPixel(0, index, new Color(0, 0, 0, 0));
                    index++;
                }

                texture.Apply();
                texture.filterMode = FilterMode.Point;

                ContentImageOutput.sprite = Sprite.Create(texture, new Rect(0, 0, 1, 20), new Vector2());

                CombinationTextOutput.text = "";

                bool foundAlloy = false;
                foreach (var item in SmelteryController.Combinations)
                {
                    bool containedSource = false;
                    string t = "";
                    for (int i = 0; i < item.AlloyParents.Count; i++)
                    {
                        foreach (var source in item.AlloyParents)
                        {
                            foreach (var stored in SmelteryController.oreStorage)
                            {
                                if (source == stored.n)
                                {
                                    containedSource = true;
                                }
                            }
                        }
                        t += item.AlloyParents[i];
                        if (i + 1 < item.AlloyParents.Count)
                        {
                            t += " + ";
                        }

                    }
                    if (containedSource)
                    {
                        foundAlloy = true;
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
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 3)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(true);
                }
                outlineObject.SetActive(true);
                CombinationTextOutput.transform.parent.gameObject.SetActive(true);
                //transform.GetChild(2).gameObject.SetActive(foundAlloy);
            }
            else
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
                outlineObject.SetActive(false);
            }
        }
    }
}
