using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CraftingPanel : MonoBehaviour
{
    [SerializeField] GameObject basepoint;

    [SerializeField] Slot slot1;
    [SerializeField] Slot slot2;
    [SerializeField] Slot slot3;

    [SerializeField] Image slotResult;
    public Vector3 offset;

    public GameObject outlineObject;
    // Start is called before the first frame update
    void Start() {
        slot1 = transform.GetChild(1).transform.GetChild(0).GetComponent<Slot>();
        slot2 = transform.GetChild(1).transform.GetChild(1).GetComponent<Slot>();
        slot3 = transform.GetChild(1).transform.GetChild(2).GetComponent<Slot>();

        slotResult = transform.GetChild(2).transform.GetChild(0).GetComponent<Image>();

        string output = "";
        foreach (ToolPattern item in patterns)
        {
            output += InventorySystem.itemList[item.toolIndex].itemName + " = " + item.part1 + " + " + item.part2 + " + " + item.part3 + " \n  \n";
        }
        transform.GetChild(4).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = output;
    }

    // Update is called once per frame

    bool updatedSlot = false;
    int craftingType = 0;
    string[] parts;
    void Update() { 
        if (basepoint != null)
        {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, basepoint.transform.position) < 3)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(true);
                }
                outlineObject.SetActive(true);
            }
            else
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
                outlineObject.SetActive(false);
            }

            gameObject.transform.position = Camera.main.WorldToScreenPoint(basepoint.transform.position) + offset;


            if (slot1.quantity != 0 & slot2.quantity != 0 & slot3.quantity != 0)
            {
                string debug = slot1.itemdata.itemName + ", " + slot2.itemdata.itemName + ", " + slot3.itemdata.itemName;

                List<string> temp = new List<string>()
                {
                    slot1.itemdata.itemName,
                    slot2.itemdata.itemName,
                    slot3.itemdata.itemName
                };

                List<string> slotNames = new List<string>()
                {
                    RemoveFirstWordOfString(slot1.itemdata.itemName),
                    RemoveFirstWordOfString(slot2.itemdata.itemName),
                    RemoveFirstWordOfString(slot3.itemdata.itemName)
                };
                slotNames.Sort();
                parts = new string[3];
                for (int i = 0; i < 3; i++)
                { 
                    for (int x = 0; x < 3; x++)
                    {
                        if (temp[i].Contains(slotNames[x]))
                        {
                            parts[x] = temp[i];
                        }
                    }  
                }

                foreach (var item in parts)
                {
                    Debug.LogError("ssssssssssss " + item);
                }

                foreach (ToolPattern item in patterns)
                {
                    Debug.Log(item.part1 + " vs " + slotNames[0] + " = " + slotNames[0].Contains(item.part1));
                    Debug.Log(item.part2 + " vs " + slotNames[1] + " = " + slotNames[1].Contains(item.part2));
                    Debug.Log(item.part3 + " vs " + slotNames[2] + " = " + slotNames[2].Contains(item.part3)); 

                    if (slotNames[0].Contains(item.part1) &
                        slotNames[1].Contains(item.part2) &
                        slotNames[2].Contains(item.part3))
                    {
                        craftingType = item.toolIndex;
                    }
                }

                if (updatedSlot == false)
                {
                    slotResult.sprite = displayCrafted();
                }

                updatedSlot = true;
            }
            else
            {
                updatedSlot = false;
            }
        }
    }
    private string RemoveFirstWordOfString(string input)
    {
        string ret = "";
        int startPosition = 0;
        char[] read = input.ToCharArray();
        for (int i = 0; i < input.ToCharArray().Length; i++)
        {  
            if (read[i] == ' ')
            {
                startPosition = i; 
                break;
            }
        }

        ret = input.Substring(startPosition, input.Length - startPosition); 
        return ret;
    }
    private string GetFirstWordOfString(string input)
    {
        string ret = "";
        int startPosition = 0;
        char[] read = input.ToCharArray();
        for (int i = 0; i < input.ToCharArray().Length; i++)
        {
            if (read[i] == ' ')
            {
                startPosition = i;
                break;
            }
        }

        ret = input.Substring(0, startPosition);
        return ret;
    }
    public Texture2D finalTex;
    public Sprite displayCrafted()
    {

        Texture2D tex1 = slot1.GetImage().texture;
        Texture2D tex2 = slot2.GetImage().texture;
        Texture2D tex3 = slot3.GetImage().texture;

        finalTex = new Texture2D(tex1.width, tex1.height);

        for (int x = 0; x < tex1.width; x++)
        {
            for (int y = 0; y < tex1.height; y++)
            {
                Color col = new Color(0, 0, 0, 0);
                if (tex1.GetPixel(x, y).a != 0)
                {
                    col = tex1.GetPixel(x, y);
                }
                if (tex2.GetPixel(x, y).a != 0)
                {
                    col = tex2.GetPixel(x, y);
                }
                if (tex3.GetPixel(x, y).a != 0)
                {
                    col = tex3.GetPixel(x, y);
                }
                finalTex.SetPixel(x, y, col);
            }
        }
        finalTex.Apply();
        finalTex.filterMode = FilterMode.Point;
        Sprite spr = Sprite.Create(finalTex, new Rect(0, 0, 32, 32), new Vector2());
        return spr;
    }
    public void craftTool() {
        if (craftingType == 0)
        {
            WarningMessage.SetWarningMessage("Crafting issue", "Could not find part combination");
            return;
        }
        //do check for texture size  
        GameObject gm = new GameObject("Tool");
        ItemWeapon data = new ItemWeapon( InventorySystem.itemList[craftingType], GetFirstWordOfString(parts[1]));  
        Sprite spr = displayCrafted();
        data.sprite = spr;

        
        InventorySystem.AddItem(gm, data);

        //Remove old stuff
        slot1.quantity = 0;
        slot1.UpdateSlot();

        slot2.quantity = 0;
        slot2.UpdateSlot();

        slot3.quantity = 0;
        slot3.UpdateSlot();

        slotResult.sprite = null;
    }

    ToolPattern[] patterns = new ToolPattern[] {
        new ToolPattern(9, "Binding", "Pickaxe Head", "Tool Rod") ,
        new ToolPattern(8, "Tool Rod", "Sword Blade", "Sword Guard") 
    };
    public struct ToolPattern {
        public int toolIndex;
        public string part1;
        public string part2;
        public string part3;
        public ToolPattern(int toolIndex, string part1, string part2, string part3) {
            this.toolIndex = toolIndex;
            this.part1 = part1;
            this.part2 = part2;
            this.part3 = part3;

        }
    }
}
