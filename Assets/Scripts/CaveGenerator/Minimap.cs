using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Minimap : MonoBehaviour {
    [SerializeField] CaveGenerator gen; 
    void Start() {
        gameObject.transform.parent.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        InvokeRepeating("UpdateData", 0, .5f);
    }
    public static Texture2D floorTexture;
    [SerializeField] Texture2D text; 
    void UpdateData() {
        //set the background texture
        text = floorTexture;
        GetComponent<RawImage>().texture = floorTexture;

        //set player position on map
        Transform position = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.transform.parent.transform.GetChild(1).transform.localPosition = VectorInt(new Vector2(position.position.x, position.position.z));

        //set player rotation on map
        Quaternion rotation = Quaternion.Euler(0, 0, position.rotation.eulerAngles.y);
        rotation.z = -rotation.z;

        //adjust children amount for monster armount
        gameObject.transform.parent.transform.GetChild(1).transform.rotation = rotation;
        foreach (Transform item in gameObject.transform.parent.transform.GetChild(2).transform) {
            item.gameObject.SetActive(false);
        }

        //adds more monster children on map
        while (gameObject.transform.parent.transform.GetChild(2).childCount - 1 <= MinimapDetetector.EnemyList.Count) {
            GameObject s = GameObject.Instantiate(gameObject.transform.parent.transform.GetChild(2).GetChild(0).gameObject);
            s.transform.SetParent(gameObject.transform.parent.transform.GetChild(2));
            s.SetActive(true);
        }

        //represent monster position on map
        for (int i = 0; i < MinimapDetetector.EnemyList.Count; i++) {
            GameObject item = MinimapDetetector.EnemyList[i];
            if (item != null) {
                gameObject.transform.parent.transform.GetChild(2).GetChild(i + 1).transform.localPosition = VectorInt(new Vector2(item.transform.position.x, item.transform.position.z));
                gameObject.transform.parent.transform.GetChild(2).GetChild(i + 1).gameObject.SetActive(true);
            }
        }
    }
    private Vector2 VectorInt(Vector2 input) {
        //round vector to int
        return new Vector2(
            Mathf.RoundToInt(input.x),
            Mathf.RoundToInt(input.y)
            );
    }
}
