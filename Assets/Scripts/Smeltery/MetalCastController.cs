using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCastController : MonoBehaviour
{
    [Range(0f, 1f)] public float progress;
    private Vector3 origin;
    [SerializeField] float offset = 1f;
    bool inProgress = false;
    public CastTypes CastType = CastTypes.Ingot;
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
                    Destroy(op.gameObject);
                    op.holding = false;
                    other.transform.GetChild(1).GetComponent<Animator>().SetBool("Holding", false);

                    StartCoroutine(fillCast());
                }
            }
        } 
    }
    IEnumerator fillCast() {
        Debug.LogError("qqq");
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
        ItemData newitem;
        switch (CastType) {
            case CastTypes.Ingot:
                break;
            case CastTypes.PickaxeHead:
                newitem = InventorySystem.itemList[12];
                newitem.sprite = tintSprite(newitem.sprite.texture, Color.red);
                InventorySystem.AddItem(gm, newitem);

                break;
            case CastTypes.ToolRod:
                newitem = InventorySystem.itemList[11];
                newitem.sprite = tintSprite(newitem.sprite.texture, Color.red);
                InventorySystem.AddItem(gm, newitem);
                break;
            case CastTypes.Binding:
                newitem = InventorySystem.itemList[10];
                newitem.sprite = tintSprite(newitem.sprite.texture, Color.red);
                InventorySystem.AddItem(gm, newitem);
                break;
            default:
                break;
        }
    }
    private Sprite tintSprite(Texture2D a, Color tint)
    {
        for (int x = 0; x < a.width; x++)
        {
            for (int y = 0; y < a.height; y++)
            {
                Color source = a.GetPixel(x, y);
                if (source.a != 0)
                {
                    Color result = tint;
                    a.SetPixel(x,y, result);
                }

            }
        }
        a.Apply();
        return Sprite.Create(a, new Rect(0, 0, 32, 32), new Vector2());
    }
}

