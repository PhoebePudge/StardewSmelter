using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SmelteryDisplayPanel : MonoBehaviour
{
    private TextMeshProUGUI textOutput;
    private Image display;
    // Start is called before the first frame update
    void Start()
    {
        display = transform.GetChild(0).GetComponent<Image>();
        textOutput = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.parent.LookAt(Camera.main.transform.position);

        string text = "Smeltery storage : \n";
        int amount = 0;
        foreach (var item in SmelteryController.oreStorage) {
            text += item.ToString() + " : " + item.quantity.ToString() + " \n";
            amount += item.quantity;
        }
        text += amount + " ingots stored here out of " + SmelteryController.capacity;

        textOutput.text = text;


        //set to only update on each new metal added
        Texture2D texture = new Texture2D(1, 20);

        int index = 0; 
        foreach (var item in SmelteryController.oreStorage) {
            Color col = item.metalObject.GetComponent<Renderer>().material.color; 
            for (int i = 0; i < item.quantity; i++) {
                texture.SetPixel(0, index + i, col);
            }
            index += item.quantity;
        }
        while (index < 20) {
            texture.SetPixel(0, index, new Color(0,0,0,0));
            index++;
        }
        texture.Apply();
        texture.filterMode = FilterMode.Point;

        display.sprite = Sprite.Create(texture, new Rect(0,0,1,20), new Vector2());
        
    }
}
