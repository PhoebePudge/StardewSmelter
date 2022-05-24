using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastingPanel : MonoBehaviour {
    public int selectedIndex = 3;
    public Transform pivotPoint;
    [SerializeField] Material castMat;
    Texture2D[] textures;
    public Vector3 PointOffset;
    public ColourOutline outlineObject;
    public int[] castCost = new int[] {
            1,
            2,
            3,
            1,
            2,
            1,
            1};
    string[] castPath = new string[] {
         "UI/StringBinding",
         "UI/ToolRod",
         "UI/PickaxeHead",
         "UI/Ingot",
         "UI/SwordBlade",
         "UI/SwordGuard" };

    // Start is called before the first frame update
    void Start() {
        Sprite[] castList = new Sprite[] {
         Resources.Load<Sprite>(castPath[0]),
         Resources.Load<Sprite>(castPath[1]),
         Resources.Load<Sprite>(castPath[2]),
         Resources.Load<Sprite>(castPath[3]),
         Resources.Load<Sprite>(castPath[4]),
         Resources.Load<Sprite>(castPath[5]) };

        textures = new Texture2D[castList.Length];
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
            textures[index] = texture;



            //32 x texture
            Texture2D tex = new Texture2D(32, 32);
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {

                    Color color = item.texture.GetPixel(x, y);

                    color.r = 1;
                    color.g = .84f;
                    color.b = 0f;

                    color.a = 1 - color.a;

                    tex.SetPixel(x, y, color);
                }
            }
            tex.Apply();
            tex.filterMode = FilterMode.Point;
            textures[index] = tex;

            //rest

            newCastPanel.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, 16, 16), new Vector2());
            newCastPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((CastTypes)index).ToString();
            newCastPanel.name = index.ToString();
            newCastPanel.GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(newCastPanel); });

            Transform IngotCostVisual = newCastPanel.transform.GetChild(1);
             
            for (int i = 0; i < 3; i++) {
                if (castCost[index] > i)
                {

                }
                else
                {
                    Destroy(IngotCostVisual.transform.GetChild(i).gameObject);
                }
            }


            index++;
        }
        Destroy(transform.GetChild(0).gameObject);
    }
    public void ButtonClick(GameObject child) {
        selectedIndex = int.Parse(child.name);
        MetalCastController.CastType = (CastTypes)selectedIndex;
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
                    gameObject.GetComponent<Image>().enabled = true;
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
                    gameObject.GetComponent<Image>().enabled = false;
                }
            }
        }
    } 
}

public enum CastTypes {
    Binding,
    ToolRod, 
    PickaxeHead,
    Ingot, 
    Blade,
    SwordGuard
}
