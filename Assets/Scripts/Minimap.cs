using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Minimap : MonoBehaviour
{
    [SerializeField] CaveGenerator gen;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        InvokeRepeating("UpdateData", 0, .5f);
    }
    public static Texture2D floorTexture;
    // Update is called once per frame
    void UpdateData()
    {
        //if (floorTexture != null)
        {
            Debug.LogError("you are setting image here");
            gameObject.GetComponent<Image>().sprite = Sprite.Create(floorTexture, new Rect(0, 0, 70, 70), new Vector2(0, 0));

            Transform position = GameObject.FindGameObjectWithTag("Player").transform;
            gameObject.transform.parent.transform.GetChild(1).transform.localPosition = VectorInt(new Vector2(position.position.x, position.position.z));

            Quaternion rotation = Quaternion.Euler(0, 0, position.rotation.eulerAngles.y);
            rotation.z = -rotation.z;

            gameObject.transform.parent.transform.GetChild(1).transform.rotation = rotation;
            foreach (Transform item in gameObject.transform.parent.transform.GetChild(2).transform)
            {
                item.gameObject.SetActive(false);
            }
            //adds more
            while (gameObject.transform.parent.transform.GetChild(2).childCount - 1 <= MinimapDetetector.EnemyList.Count)
            {
                GameObject s = GameObject.Instantiate(gameObject.transform.parent.transform.GetChild(2).GetChild(0).gameObject);
                s.transform.SetParent(gameObject.transform.parent.transform.GetChild(2));
                s.SetActive(true);
            }
            for (int i = 0; i < MinimapDetetector.EnemyList.Count; i++)
            {
                GameObject item = MinimapDetetector.EnemyList[i];
                gameObject.transform.parent.transform.GetChild(2).GetChild(i + 1).transform.localPosition = VectorInt(new Vector2(item.transform.position.x, item.transform.position.z));
                gameObject.transform.parent.transform.GetChild(2).GetChild(i + 1).gameObject.SetActive(true);
            }
        }
         
    }
    private Vector2 VectorInt(Vector2 input)
    {
        return new Vector2(
            Mathf.RoundToInt(input.x),
            Mathf.RoundToInt(input.y)
            );
    }
}
