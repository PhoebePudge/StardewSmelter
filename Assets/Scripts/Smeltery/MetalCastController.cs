using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCastController : MonoBehaviour
{
    static MetalCastController main;
    [Range(0f, 1f)] public float progress;
    private Vector3 origin;
    [SerializeField] float offset = 1f;
    bool inProgress = false;
    public static CastType CastType = CastType.Ingot;
    public Metal castedMetal;

    void Start() {
        main = this;
        origin = gameObject.transform.localPosition;
    }

    void Update() {
        gameObject.transform.localPosition = Vector3.Lerp(origin, new Vector3(origin.x, origin.y + offset, origin.z), progress); 
    }
    public void fillTheCast(Metal oreType)
    {
        castedMetal = oreType;
        StartCoroutine(fillCast());
        progress = 0f;
    }
    /*
    private void OnTriggerStay(Collider other) { 
        if (other.tag == "Player") {
            ObjectPickup op = other.transform.GetChild(0).GetComponent<ObjectPickup>();

            
            //castedMetal = op.heldItem.transform.GetChild(1).gameObject.GetComponent<BucketOfMetal>().oreType;

            if (inProgress == false) {
                if (op.holding == true) {
                    castedMetal = other.transform.GetChild(0).GetChild(0).GetComponent<BucketOfMetal>().oreType;


                    inProgress = true;
                    Destroy(op.heldItem.gameObject);
                    op.holding = false;
                    other.transform.GetChild(1).GetComponent<Animator>().SetBool("Holding", false);

                    StartCoroutine(fillCast());
                    inProgress = false;
                    progress = 0f;
                }
            }
        } 
    }*/
    IEnumerator fillCast() {
        while (progress < 1f) {
            progress += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        outputCastedMetal();
        progress = 0;
        yield return null;
    }

    private void outputCastedMetal() {
        GameObject gm = new GameObject("Test");
        ItemData newitem = null;

        Color metalColour = castedMetal.metalObject.GetComponent<MeshRenderer>().material.color; 

        switch (CastType) {
            case CastType.Ingot:
                newitem = new ItemData( InventorySystem.itemList[13]);
                newitem.itemName = castedMetal.n + " Ingot";
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;

            case CastType.PickaxeHead:
                newitem = new ItemData(InventorySystem.itemList[12]);
                newitem.itemName = castedMetal.n + " Pickaxe Head";
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;

            case CastType.ToolRod:
                newitem = new ItemData(InventorySystem.itemList[11]);
                newitem.itemName = castedMetal.n + " Tool Rod";
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;

            case CastType.Binding: 
                newitem = new ItemData(InventorySystem.itemList[10]);
                newitem.itemName = castedMetal.n + " Binding";
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;

            case CastType.SwordGuard:
                newitem = new ItemData(InventorySystem.itemList[15]);
                newitem.itemName = castedMetal.n + " Sword Guard";
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;

            case CastType.SwordBlade:
                newitem = new ItemData(InventorySystem.itemList[14]);
                newitem.itemName = castedMetal.n + " Sword Blade";
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;

            default:
                Debug.Log("Unknown cast, please link the cast to its item data here... Setting it to null cast");

                newitem = new ItemData(InventorySystem.itemList[23]);
                newitem.itemName = castedMetal.n + " " + CastType.ToString();
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;
        } 
    }
    private Sprite tintSprite(Texture2D origional, Color tint)
    {
        Texture2D result = new Texture2D(origional.width, origional.height);
        for (int x = 0; x < origional.width; x++)
        {
            for (int y = 0; y < origional.height; y++)
            {
                Color source = origional.GetPixel(x, y);
                if (source.a != 0)
                {
                    Color output = source * tint;
                    result.SetPixel(x,y, output);
                }
                else
                { 
                    result.SetPixel(x, y, new Color(0, 0, 0, 0));
                }

            }
        }
        result.Apply();
        result.filterMode = FilterMode.Point;
        return Sprite.Create(result, new Rect(0, 0, 32, 32), new Vector2());
    }
}

