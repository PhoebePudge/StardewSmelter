using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCastController : MonoBehaviour
{
    [Range(0f, 1f)] public float progress;
    private Vector3 origin;
    [SerializeField] float offset = 1f;
    bool inProgress = false;
    public static CastTypes CastType = CastTypes.Ingot;
    public Metal castedMetal;

    void Start() {
        origin = gameObject.transform.localPosition;
    }

    void Update() {
        gameObject.transform.localPosition = Vector3.Lerp(origin, new Vector3(origin.x, origin.y + offset, origin.z), progress); 
    }

    
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
    }
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
        ItemData newitem = InventorySystem.itemList[12];
        Color metalColour = castedMetal.metalObject.GetComponent<MeshRenderer>().material.color; 
        switch (CastType) {
            case CastTypes.Ingot:
                break;
            case CastTypes.PickaxeHead:
                newitem = InventorySystem.itemList[12]; 
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;
            case CastTypes.ToolRod:
                newitem = InventorySystem.itemList[11];
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;
            case CastTypes.Binding: 
                newitem = InventorySystem.itemList[10];
                newitem.sprite = tintSprite(newitem.sprite.texture, metalColour);
                InventorySystem.AddItem(gm, newitem);
                break;
            default:
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

