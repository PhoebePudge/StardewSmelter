using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class CastingPanel : MonoBehaviour {
    public static int selectedIndex = 3;
    public Transform pivotPoint;
    [SerializeField] Material castMat;
    public Vector3 PointOffset;
    public ColourOutline outlineObject;
    // Start is called before the first frame update
    static CastingPanel instance;
    private void OnEnable() {
        //set instance at first time
        if (instance == null) {
            instance = this;
        }
        //transfer variables over on scene enter
        if (instance != this) {
            instance.pivotPoint = pivotPoint;
            instance.castMat = castMat;
            instance.outlineObject = outlineObject;
            Destroy(this);
        }
    }
    private Texture2D ConvertTexture(Texture2D baseTexture) {
        Texture2D newTexture = new Texture2D(baseTexture.width, baseTexture.height);

        //loop through pixels
        for (int x = 0; x < baseTexture.width; x++) {
            for (int y = 0; y < baseTexture.height; y++) {

                //if this is not a transparent pixel
                if (baseTexture.GetPixel(x, y).a != 0) {
                    //set it to be transparent
                    Color c = new Color(0, 0, 0, 0);
                    newTexture.SetPixel(x, y, c);
                } else {
                    //if it is trasparent, set it to gold
                    Color baseColour = new Color(248 / 255f, 197 / 255f, 58 / 255f, 1);
                    Color shadowColour = new Color(211 / 255f, 151 / 255f, 65 / 255f, 1);
                    newTexture.SetPixel(x, y, baseColour);

                    //if this is a edge, set a edge colour
                    if (y != 0) {
                        if (baseTexture.GetPixel(x, y - 1).a != 0) {
                            newTexture.SetPixel(x, y, shadowColour);
                        }
                    }

                    //if this is a edge. set a edge colour
                    if (x != 0) {
                        if (baseTexture.GetPixel(x - 1, y).a != 0) {
                            newTexture.SetPixel(x, y, shadowColour);
                        }
                    }

                }
            }
        }

        //apply stuff
        newTexture.Apply();
        newTexture.filterMode = FilterMode.Point;

        return newTexture;
    }
    private string ConvertCastTypeToText(string text) {
        string newText = "";

        //convert cast name into string, with spaces placed at every case
        foreach (char character in text.ToCharArray()) {
            if (char.IsUpper(character)) {
                newText += " " + character;
            } else {
                newText += character;
            }
        }
        return newText;
    }
    void Start() {
        //Loop through every casts we know
        for (int index = 0; index < Casts.Length; index++) {
            Cast castType = Casts[index];

            //parent and child stuff
            GameObject newCastPanel = Instantiate(transform.GetChild(0).gameObject);
            newCastPanel.transform.SetParent(transform, false); 
            GameObject spriteParent = newCastPanel.transform.GetChild(0).gameObject;


            //Convert our cast texture into a cast image
            Texture2D tex = ConvertTexture(Resources.Load<Sprite>(castType.path).texture);

            //Set it to our sprite
            spriteParent.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2());
            spriteParent.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = ConvertCastTypeToText(castType.types.ToString());
            spriteParent.name = index.ToString();

            //add button componet 
            spriteParent.AddComponent<CastingPanelButton>();
            spriteParent.GetComponent<CastingPanelButton>().Index = index;

            //sort out listener
            spriteParent.GetComponent<Button>().onClick.AddListener(
                delegate { spriteParent.GetComponent<CastingPanelButton>().OnClick(); });

            //sort out display cost
            Transform IngotCostVisual = spriteParent.transform.GetChild(2);
            for (int i = 0; i < 3; i++) {
                if (Casts[index].cost <= i) {
                    Destroy(IngotCostVisual.transform.GetChild(i).gameObject);
                }
            }
        }

        //destroy reference
        Destroy(transform.GetChild(0).gameObject);
    }
    private void Update() {
        if (pivotPoint != null) {
            //sort out textures
            for (int i = 0; i < transform.childCount; i++) {
                if (i == selectedIndex) {
                    transform.GetChild(i).localScale = new Vector3(1.2f, 1.2f, 1.2f);
                } else {
                    transform.GetChild(i).localScale = new Vector3(1f, 1f, 1f);
                }
            }

            //set position
            gameObject.transform.position = Camera.main.WorldToScreenPoint(pivotPoint.transform.position) + PointOffset;


            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 3) {
                //when switched to active, enabled objects and set to saturated colour
                if (!gameObject.transform.GetChild(0).gameObject.activeInHierarchy) {
                    foreach (Transform child in gameObject.transform) {
                        child.gameObject.SetActive(true);
                    }
                    outlineObject.ColourChange(true);
                }
            } else {
                //when switched to disable, disable objects and set to desaturated colour
                if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy) {
                    foreach (Transform child in gameObject.transform) {
                        child.gameObject.SetActive(false);
                    }
                    outlineObject.ColourChange(false);
                }
            }
        }
    }

    //cast reference structs
    public static Cast[] Casts = {
        new Cast(1,"UI/Ingot",                          CastType.Ingot ),

        new Cast(2,"UI/CraftingParts/HelmCore",         CastType.HelmCore ),
        new Cast(2,"UI/CraftingParts/ChestCore",        CastType.ChestCore ),
        new Cast(2,"UI/CraftingParts/LegCore",          CastType.LegCore ),
        new Cast(2,"UI/CraftingParts/ArmCore",          CastType.ArmCore ),

        new Cast(3,"UI/CraftingParts/AxeHead",          CastType.AxeHead ),
        new Cast(2,"UI/CraftingParts/DaggerBlade",      CastType.KnifeBlade ),
        new Cast(3,"UI/CraftingParts/HammerHead",       CastType.HammerHead ),
        new Cast(3,"UI/CraftingParts/PickaxeHead",      CastType.PickaxeHead ),
        new Cast(1,"UI/CraftingParts/Plating",          CastType.Plating ),
        new Cast(2,"UI/CraftingParts/ShortSwordBlade",  CastType.ShortSwordBlade ),
        new Cast(3,"UI/CraftingParts/SwordBlade",       CastType.SwordBlade ),
        new Cast(2,"UI/CraftingParts/SwordGuard",       CastType.SwordGuard ),
        new Cast(1,"UI/CraftingParts/ToolBinding",      CastType.Binding ),
        new Cast(1,"UI/CraftingParts/ToolRod",          CastType.ToolRod )
    };
}

public struct Cast {
    public int cost;
    public string path;
    public CastType types;

    public Cast(int cost, string path, CastType types) {
        this.cost = cost;
        this.path = path;
        this.types = types;
    }
}
public enum CastType {
    ArmCore,
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
