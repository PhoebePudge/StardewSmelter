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
    // Start is called before the first frame update
    void Start()
    { 
        ContentTextOutput = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        CombinationTextOutput = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        ContentImageOutput = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>();
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
                SmelteryController.AddItem(inputSlot.itemdata.itemName, inputSlot.quantity);
                inputSlot.quantity = 0;
                inputSlot.UpdateSlot();
            }

            //set its screen position
            gameObject.transform.position = Camera.main.WorldToScreenPoint(pivotPoint.transform.position) + PositionOffset;

            //get total ingots stored
            string text = "";
            int amount = 0;
            foreach (var item in SmelteryController.oreStorage)
            {
                text += item.ToString() + " : " + item.quantity.ToString() + " \n";
                amount += item.quantity;
            }
            //text += amount + " ingots stored here out of " + SmelteryController.capacity; 
            ContentTextOutput.text = text;

            //set to only update on each new metal added
            Texture2D texture = new Texture2D(1, 20);

            int index = 0;
            foreach (var item in SmelteryController.oreStorage)
            {
                if (item.metalObject != null)
                {
                    Color col = item.metalObject.GetComponent<Renderer>().material.color;
                    for (int i = 0; i < item.quantity; i++)
                    {
                        texture.SetPixel(0, index + i, col);
                    }
                }
                index += item.quantity;
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
            //Distance active
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 3)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(true);
                }
                outlineObject.SetActive(true);
                CombinationTextOutput.transform.parent.gameObject.SetActive(foundAlloy);
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
