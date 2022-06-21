using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class CastingPanel : MonoBehaviour{
    public int selectedIndex = 3;
    public Transform pivotPoint;
    [SerializeField] Material castMat;
    Texture2D[] textures;
    public Vector3 PointOffset;
    public ColourOutline outlineObject; 
    // Start is called before the first frame update


    void Start() {

        List<Sprite> castList = new List<Sprite>();
        foreach (var item in Casts)
        {
            castList.Add(Resources.Load<Sprite>(item.path));
        } 

        textures = new Texture2D[Casts.Length];

        for (int index = 0; index < Casts.Length; index++)
        {   
            GameObject newCastPanel = Instantiate(transform.GetChild(0).gameObject);
            newCastPanel.transform.SetParent(transform, false);

            GameObject childTransform = newCastPanel.transform.GetChild(0).gameObject;

            Texture2D baseTexture = castList[index].texture;
            Texture2D newTexture = new Texture2D(baseTexture.width, baseTexture.height);

            for (int x = 0; x < baseTexture.width; x++)
            {
                for (int y = 0; y < baseTexture.height; y++)
                { 
                    if (baseTexture.GetPixel(x,y).a != 0)
                    {
                        Color c = new Color(0, 0, 0, 0);
                        newTexture.SetPixel(x, y, c);
                    }
                    else
                    {
                        Color baseColour = new Color(248 / 255f, 197 / 255f, 58 / 255f, 1);
                        Color shadowColour = new Color(211 / 255f, 151 / 255f, 65 / 255f, 1);
                        newTexture.SetPixel(x,y, baseColour);

                        if (y != 0)
                        {
                            if (baseTexture.GetPixel(x, y - 1).a != 0)
                            {
                                newTexture.SetPixel(x, y, shadowColour);
                            }
                        }

                        if (x != 0)
                        {
                            if (baseTexture.GetPixel(x - 1, y).a != 0)
                            {
                                newTexture.SetPixel(x, y, shadowColour);
                            }
                        }

                    }
                }
            }
            newTexture.Apply();
            newTexture.filterMode = FilterMode.Point;

            textures[index] = newTexture;

            childTransform.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2());
            string text = Casts[index].types.ToString(); 
            string newText = "";

            foreach (char character in text.ToCharArray())
            {
                if (char.IsUpper(character))
                {
                    newText += " " + character;
                }
                else
                {
                    newText += character;
                }
            }
            childTransform.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = newText;
            childTransform.name = index.ToString();
            childTransform.GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(childTransform); }); 
            EventTrigger myEventTrigger = childTransform.GetComponent<EventTrigger>();


            Transform IngotCostVisual = childTransform.transform.GetChild(2); 

            for (int i = 0; i < 3; i++) {
                if (Casts[index].cost > i)
                {

                }
                else
                {
                    Destroy(IngotCostVisual.transform.GetChild(i).gameObject);
                }
            }


        }
        Destroy(transform.GetChild(0).gameObject);
    }
    GameObject previousButton;
    public void ButtonClick(GameObject child) {
        selectedIndex = int.Parse(child.name);
        MetalCastController.CastType = (CastType)selectedIndex;


        if (previousButton != null)
        {
            Debug.LogError("comparing " + previousButton.name + " to " + child.name);
            previousButton.transform.parent.GetComponent<Canvas>().sortingOrder = 1;
        }

        child.transform.parent.GetComponent<Canvas>().sortingOrder = 2;
        previousButton = child;
    }

    private void Update() {
        if (pivotPoint != null)
        {
            //sort out textures
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == selectedIndex)
                {
                    transform.GetChild(i).localScale = new Vector3(1.2f, 1.2f, 1.2f); 
                    castMat.SetTexture("_BaseMap", textures[i]);
                }
                else
                {
                    transform.GetChild(i).localScale = new Vector3(1f, 1f, 1f);
                }
            }

            //set position
            gameObject.transform.position = Camera.main.WorldToScreenPoint(pivotPoint.transform.position) + PointOffset;

            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 3)
            {
                if (!gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                    outlineObject.ColourChange(true);
                    //gameObject.GetComponent<Image>().enabled = true;
                }
            }
            else
            {
                if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                    outlineObject.ColourChange(false);
                    //gameObject.GetComponent<Image>().enabled = false;
                }
            }
        }
    }

    public static Cast[] Casts =
    { 
        new Cast(1,"UI/Ingot", CastType.Ingot ), 

        new Cast(1,"UI/CraftingParts/HelmCore", CastType.HelmCore ),
        new Cast(1,"UI/CraftingParts/ChestCore", CastType.ChestCore ),
        new Cast(1,"UI/CraftingParts/LegCore", CastType.LegCore ),
        new Cast(1,"UI/CraftingParts/ArmCore", CastType.ArmCore ),
         
        new Cast(1,"UI/CraftingParts/AxeHead", CastType.AxeHead ),
        new Cast(1,"UI/CraftingParts/Clasp", CastType.Clasp ),
        new Cast(1,"UI/CraftingParts/DaggerBlade", CastType.KnifeBlade ),
        new Cast(1,"UI/CraftingParts/HammerHead", CastType.HammerHead ),
        new Cast(1,"UI/CraftingParts/PickaxeHead", CastType.PickaxeHead ),
        new Cast(1,"UI/CraftingParts/Plating", CastType.Plating ),
        new Cast(1,"UI/CraftingParts/ShortSwordBlade", CastType.ShortSwordBlade ),
        new Cast(1,"UI/CraftingParts/SwordBlade", CastType.SwordBlade ),
        new Cast(1,"UI/CraftingParts/SwordGuard", CastType.SwordGuard ),
        new Cast(1,"UI/CraftingParts/ToolBinding", CastType.Binding ),
        new Cast(1,"UI/CraftingParts/ToolRod", CastType.ToolRod )
    };
}

public struct Cast{
    public int cost;
    public string path;
    public CastType types;

    public Cast(int cost, string path, CastType types)
    {
        this.cost = cost;
        this.path = path;
        this.types = types;
    }
}
public enum CastType {
    ArmCore,
    Clasp,
    AxeHead,
    Binding,
    ChestCore,
    HammerHead,
    HelmCore,
    KnifeBlade,
    LegCore,
    PickaxeHead,
    Plating,
    ShortSwordBlade,
    SwordBlade,
    SwordGuard,
    ToolRod,
    Ingot
}
