using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCastController : MonoBehaviour {
    static MetalCastController instance;
    [Range(0f, 1f)] public float progress;
    private Vector3 origin;
    [SerializeField] float offset = 1f;
    public static CastType CastType = CastType.Ingot;
    public Metal castedMetal;
    void Start() {
        //if we dont have a instance, set this
        if (instance == null) {
            instance = this;
            origin = gameObject.transform.localPosition;
        }
    }

    void Update() {
        gameObject.transform.localPosition = Vector3.Lerp(origin, new Vector3(origin.x, origin.y + offset, origin.z), progress);
    }
    public void fillTheCast(Metal oreType) {
        castedMetal = oreType;
        StartCoroutine(fillCast());
        progress = 0f;
    }
    IEnumerator fillCast() {
        //fill cast slider
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
        Color metalColour = castedMetal.col;

        //foreach cast
        foreach (var item in CastingPanel.Casts) {
            //if this is our cast
            if (item.types == CastType) {
                //create the item
                ItemData newitem2 = new ItemData(castedMetal.n + " " + CastType.ToString(), 1, item.path, "ss", Attribute.CraftingPart);

                //tint the sprite
                if (newitem2.sprite != null) {
                    newitem2.sprite = tintSprite(newitem2.sprite.texture, metalColour);
                }

                //add the item
                InventorySystem.AddItem(gm, newitem2);

                return;
            }
        }
    }
    private Sprite tintSprite(Texture2D origional, Color tint) {
        Texture2D result = new Texture2D(origional.width, origional.height);

        //loop through pixels
        for (int x = 0; x < origional.width; x++) {
            for (int y = 0; y < origional.height; y++) {
                Color source = origional.GetPixel(x, y);

                //if we have a pixel with colour
                if (source.a != 0) {

                    //tint it
                    Color output = source * tint;
                    result.SetPixel(x, y, output);
                } else {

                    //not needed pixel
                    result.SetPixel(x, y, new Color(0, 0, 0, 0));
                }

            }
        }

        //apply it
        result.Apply();
        result.filterMode = FilterMode.Point;
        return Sprite.Create(result, new Rect(0, 0, origional.width, origional.height), new Vector2());
    }
}

