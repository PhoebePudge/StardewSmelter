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
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().sprite = Sprite.Create(gen.floorTexture, new Rect(0,0,70,70), new Vector2(0,0));

        Vector3 position = GameObject.FindGameObjectWithTag("Player").transform.position;
        gameObject.transform.parent.transform.GetChild(1).transform.localPosition = new Vector2(position.x, position.z);
    }
}
