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

    void Start() {
        origin = gameObject.transform.localPosition;
    }

    void Update() {
        gameObject.transform.localPosition = Vector3.Lerp(origin, new Vector3(origin.x, origin.y + offset, origin.z), progress);
        if (progress > .9f) {
            Debug.LogError("Complete");
        }
    }

    
    private void OnTriggerStay(Collider other) { 
        if (other.tag == "Player") {
            ObjectPickup op = other.transform.GetChild(0).GetComponent<ObjectPickup>();
            if (inProgress == false) {
                if (op.holding == true) { 
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
        yield return null;
    }

    private void outputCastedMetal() {
        switch (CastType) {
            case CastTypes.Ingot:
                break;
            case CastTypes.PickaxeHead:
                InventorySystem.AddItem(null, InventorySystem.itemList[12]);
                break;
            case CastTypes.ToolRod:
                InventorySystem.AddItem(null, InventorySystem.itemList[11]);
                break;
            case CastTypes.Binding:
                InventorySystem.AddItem(null, InventorySystem.itemList[10]);
                break;
            default:
                break;
        }
    }
}

