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
     
    public ColourOutline colourOutline;


    public static int SelectedMetalIndex = 0;
    public GameObject LabelGM;
    GameObject ContentPointer;
    public static bool UpdatePanel = true;
    List<GameObject> SmelteryLabels = new List<GameObject>();

    bool animating = false;
    public void IncreaseSelectedMetal()
    {
        if (SelectedMetalIndex + 1 <= SmelteryLabels.Count)
        {
            Debug.LogError("You can increase this");

            GameObject temp = SmelteryLabels[SelectedMetalIndex];
            SmelteryLabels[SelectedMetalIndex] = SmelteryLabels[SelectedMetalIndex + 1];
            SmelteryLabels[SelectedMetalIndex + 1] = temp;

        }
        SmelteryDisplayPanel.UpdatePanel = true;  
    } 
    public void DecreaseSelectedMetal()
    {
        SmelteryDisplayPanel.UpdatePanel = true; 
        if (SelectedMetalIndex > 0)
        { SelectedMetalIndex--; }
    }  
    void Start()
    { 
        ContentTextOutput = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        CombinationTextOutput = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        ContentImageOutput = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>();
        ContentPointer = transform.GetChild(0).GetChild(3).GetChild(1).gameObject; 
        SmelteryDisplayPanel.UpdatePanel = true;
    }
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
    IEnumerator LerpSize(int a, int b)
    {
        float time = 0f;

        while (time < b) { 
            time += 0.05f;
            float progress = Mathf.Lerp(a, b, time);

            transform.localScale = new Vector3(progress, progress, time);
             
            yield return new WaitForSeconds(0.005f);
        } 
    }
    IEnumerator DecreaseLerp(int a, int b)
    {

        float time = 0f;

        Debug.LogError(a + " : " + b);
        while (time < a)
        {
            time += 0.1f;
            float progress = Mathf.Lerp(a, b, time);

            transform.localScale = new Vector3(progress, progress, time);
             
            yield return new WaitForSeconds(0.005f);
        }

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void ButtonClick(GameObject gm)
    { 
        Debug.LogError("Clicked " + gm.name);
    }
    void Update()
    {   
        if (pivotPoint != null)
        { 
            if (inputSlot.SlotInUse())
            {  
                StartCoroutine(SmeltAnimation(inputSlot.quantity)); 
            }
             
            if (UpdatePanel)
            {

                if (SmelteryLabels.Count == 0)
                {
                    ContentPointer.SetActive(false);
                }
                else
                {
                    ContentPointer.SetActive(true);
                }


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
                        gm.AddComponent<SmelteryButton>();
                        gm.GetComponent<SmelteryButton>().index = SmelteryController.oreStorage.Count - 1;
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
                            Debug.LogError("ddddddddddddddddddddddd");
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
                if (!gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    StartCoroutine(LerpSize(0, 1));
                    foreach (Transform child in gameObject.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                    colourOutline.ColourChange(true);
                    colourOutline.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Pause();
                    CombinationTextOutput.transform.parent.gameObject.SetActive(true);
                }
            }
            else
            {
                if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    StartCoroutine(DecreaseLerp(1, 0));
                    colourOutline.ColourChange(false);
                    colourOutline.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                }
            }
        }
    }
}
