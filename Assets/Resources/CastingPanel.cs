using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastingPanel : MonoBehaviour {
    int selectedIndex = 3;
    public Transform pivotPoint;

    // Start is called before the first frame update
    void Start() {
        Sprite[] castList = new Sprite[] {
         Resources.Load<Sprite>("Images/UI/StringBinding"),
         Resources.Load<Sprite>("Images/UI/ToolRod"),
         Resources.Load<Sprite>("Images/UI/PickaxeHead"),
         Resources.Load<Sprite>("Images/UI/Ingot")
     };



        int index = 0;
        foreach (var item in castList) {
            GameObject newCastPanel = GameObject.Instantiate(transform.GetChild(0).gameObject);
            newCastPanel.transform.SetParent(transform, false);

            Texture2D texture = new Texture2D(16, 16);
            for (int x = 0; x < 16; x++) {
                for (int y = 0; y < 16; y++) {

                    Color color = item.texture.GetPixel(x * 2, y * 2);

                    color.r = 1;
                    color.g = .84f;
                    color.b = 0f;

                    color.a = 1 - color.a;

                    texture.SetPixel(x, y, color);
                }
            }
            texture.Apply();
            texture.filterMode = FilterMode.Point;

            newCastPanel.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, 16, 16), new Vector2());
            newCastPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((CastTypes)index).ToString();
            newCastPanel.name = index.ToString();
            newCastPanel.GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(newCastPanel); });
            index++;
        }
        Destroy(transform.GetChild(0).gameObject);
    }
    public void ButtonClick(GameObject child) {
        Debug.LogError(child.name);
        selectedIndex = int.Parse(child.name);
    }

    private void Update() {
        for (int i = 0; i < transform.childCount; i++) {
            if (i == selectedIndex) {
                transform.GetChild(i).localScale = new Vector3(1.2f, 1.2f, 1.2f);
            } else {
                transform.GetChild(i).localScale = new Vector3(1f, 1f, 1f);
            }
        }


        gameObject.transform.position = Camera.main.WorldToScreenPoint(pivotPoint.transform.position);

        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 3) {

            foreach (Transform child in gameObject.transform) {
                child.gameObject.SetActive(true);
            }

        } else {

            foreach (Transform child in gameObject.transform) {
                child.gameObject.SetActive(false);
            }

        }
    }
}

public enum CastTypes {
    Binding,
    ToolRod, 
    PickaxeHead,
    Ingot
}
