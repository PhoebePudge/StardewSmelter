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

    [HideInInspector] public Sprite[] CraftingSprites1 = new Sprite[11];
    [HideInInspector] public Sprite[] CraftingSprites2 = new Sprite[11];
    [HideInInspector] public Sprite[] CraftingSprites3 = new Sprite[11]; 

    public ColourOutline outlineObject;
    

    int craftingType = 0;
    int previousCraftingType = 0;
    string[] parts;
    bool Open = true;
     
    void Start() {
        slot1 = transform.GetChild(1).transform.GetChild(0).GetComponent<Slot>();
        slot2 = transform.GetChild(1).transform.GetChild(1).GetComponent<Slot>();
        slot3 = transform.GetChild(1).transform.GetChild(2).GetComponent<Slot>();

        slotResult = transform.GetChild(2).transform.GetChild(0).GetComponent<Image>();

        string output = "";
        foreach (ToolPattern item in patterns)
        {
            output += item.ToString() + " = " + item.parts[0].ToString() + " + " + item.parts[1].ToString() + " + " + item.parts[2].ToString() + " \n";
        }
        transform.GetChild(4).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = output;

        ToggleExpand();
    } 
    public void ToggleExpand()
    {
        Open = !Open;
        if (Open)
        {
            transform.GetChild(4).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Close";
        } else{
            transform.GetChild(4).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Expand";
        }
        transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(Open);
        transform.GetChild(4).GetComponent<Image>().enabled = Open;
    }
    IEnumerator LerpSize(int a, int b)
    {
        float time = 0f;

        while (time < b)
        {
            time += 0.05f;
            float progress = Mathf.Lerp(a, b, time);

            transform.localScale = new Vector3(progress, progress, time);

            yield return new WaitForSeconds(0.005f);
        }
    }
    IEnumerator DecreaseLerp(int a, int b)
    {
        float time = 0f;

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
    private bool slotsFilled()
    {
        return slot1.quantity != 0 & slot2.quantity != 0 & slot3.quantity != 0;
    }
    public void ToggleDisplay()
    {
        //Distance popup stuff
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, basepoint.transform.position) < 3)
        {
            if (!gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
            { 
                StartCoroutine(LerpSize(0, 1));
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(true);
                }
                outlineObject.ColourChange(true);
            }
        }
        else
        {
            if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
            { 
                StartCoroutine(DecreaseLerp(1, 0));
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
                outlineObject.ColourChange(false);
            }
        }
    }

    static string[] previousInput;
    void Update() {
        if (Input.GetKeyDown(KeyCode.J))
        {
            slotResult.sprite = displayCrafted();
        }

        if (basepoint != null)
        {
            ToggleDisplay();
        }

        if (slotsFilled())
        { 
            string[] slotNames = new string[] { slot1.itemdata.itemName, slot2.itemdata.itemName, slot3.itemdata.itemName};

            bool updateOurData = false;

            if (previousInput != null)
            {
                updateOurData = (previousInput[0].Contains(slotNames[0]) & previousInput[1].Contains(slotNames[1]) & previousInput[2].Contains(slotNames[2]));
            }  
            previousInput = slotNames;

            //string output = "";

            //foreach (string item in slotNames)
            //{
            //    output += item + " ";
            //}

            //output += " / ";

            //foreach (string item in previousInput)
            //{
            //    output += item + " ";
            //}

            //output += " = " + updateOurData;

            //Debug.LogError(output);

            if (!updateOurData) 
            { 
                //get our input names and sort them
                List<CastType> input = new List<CastType>()
                {
                     (CastType) System.Enum.Parse(typeof(CastType), RemoveFirstWordOfString(slot1.itemdata.itemName)),
                     (CastType) System.Enum.Parse(typeof(CastType), RemoveFirstWordOfString(slot2.itemdata.itemName)),
                     (CastType) System.Enum.Parse(typeof(CastType), RemoveFirstWordOfString(slot3.itemdata.itemName))
                };
                input.Sort();

                craftingType =  -1;
                
                //loop through each pattern we know
                for (int i = 0; i < patterns.Length; i++)
                {
                    ToolPattern item = patterns[i];

                    if ((item.parts[0] == input[0]) & (item.parts[1] == input[1]) & (item.parts[2] == input[2]))
                    {
                        craftingType = i;
                        currentType = item.type;
                        previousCraftingType = craftingType;
                        break;
                    }
                }

                slotResult.sprite = displayCrafted();
            } 
        }
    }
    WeaponTypes currentType;
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

    bool alreadyRunning = false;
    public Sprite displayCrafted()
    {
        if (alreadyRunning)
        {
            return null;
        }

        
        if (craftingType == -1)
        {
            Debug.LogError("You do not have a valid pattern");
            return null;
        }


        alreadyRunning = true;
        //ToolPattern type = patterns[craftingType];
        //Debug.LogError("You want to craft " + type.ToString());

        
        Texture2D tex1 = CraftingSprites1[craftingType].texture;
        Texture2D tex2 = CraftingSprites2[craftingType].texture;
        Texture2D tex3 = CraftingSprites3[craftingType].texture; 

        finalTex = new Texture2D(tex1.width, tex1.height);


        for (int x = 0; x < tex1.width; x++)
        {
            for (int y = 0; y < tex1.height; y++)
            {
                Color col = tex1.GetPixel(x, y);

                //if (tex1.GetPixel(x, y).a != 0 )
                //{
                //    col = tex1.GetPixel(x, y);
                //}

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

        alreadyRunning = false;
        return spr; 
    } 
    public void craftTool() {
        
        if (craftingType == -1)
        {
            WarningMessage.SetWarningMessage("Crafting issue", "Could not find part combination");
            return;
        }
        //do check for texture size  
        GameObject gm = new GameObject("Tool");
        bool result;
        Metal[] metals = new Metal[3];
        metals[0] = SmelteryController.SearchDictionaryForMetal(GetFirstWordOfString(slot1.itemdata.itemName), out result);
        metals[1] = SmelteryController.SearchDictionaryForMetal(GetFirstWordOfString(slot1.itemdata.itemName), out result);
        metals[2] = SmelteryController.SearchDictionaryForMetal(GetFirstWordOfString(slot1.itemdata.itemName), out result); 
        ItemWeapon data = new ItemWeapon(metals, currentType, InventorySystem.itemList[patterns[craftingType].toolIndex], metals[0].ToString());

        data.itemName = currentType.ToString();
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

    public ToolPattern[] patterns = new ToolPattern[] {
        new ToolPattern(WeaponTypes.Pickaxe, 9, CastType.Binding, CastType.PickaxeHead, CastType.ToolRod) , //pickaxe
         
        new ToolPattern(WeaponTypes.None, 8, CastType.HelmCore, CastType.Plating, CastType.Clasp, "Helm") , //helm
        new ToolPattern(WeaponTypes.None,8, CastType.ChestCore, CastType.Plating, CastType.Clasp, "Chest") , //chest
        new ToolPattern(WeaponTypes.None,8, CastType.LegCore, CastType.Plating, CastType.Clasp, "Legs") , //boot
        new ToolPattern(WeaponTypes.None,8, CastType.ArmCore, CastType.Plating, CastType.Clasp, "Arm") , //gloves

        new ToolPattern(WeaponTypes.Sword, 8, CastType.SwordBlade, CastType.SwordGuard, CastType.ToolRod) , // sword
        new ToolPattern(WeaponTypes.Axe, 8, CastType.AxeHead, CastType.Binding, CastType.ToolRod) , //waraxe
        new ToolPattern(WeaponTypes.Dagger, 8, CastType.KnifeBlade, CastType.ToolRod, CastType.ToolRod) , //dagger
        new ToolPattern(WeaponTypes.ShortSword, 8, CastType.ShortSwordBlade, CastType.SwordGuard, CastType.ToolRod) , //short sword
        new ToolPattern(WeaponTypes.Claymore, 8, CastType.SwordBlade, CastType.ToolRod, CastType.SwordGuard) , //claymore
        new ToolPattern(WeaponTypes.WarHammer, 8, CastType.HammerHead, CastType.Binding, CastType.ToolRod) , //warhammer
    };
}
public struct ToolPattern
{
    public WeaponTypes type;
    public int toolIndex;
    public List<CastType> parts;
    private string name;
    public ToolPattern(WeaponTypes type, int toolIndex, CastType part1, CastType part2, CastType part3, string name = "")
    {
        if (name == "")
        {
            name = type.ToString();
        }
        this.name = name;
        this.type = type;
        parts = new List<CastType>()
            {
                part1,
                part2,
                part3
            };
        parts.Sort();
        this.toolIndex = toolIndex;
    }
    public override string ToString()
    {
        return name;
    }
}