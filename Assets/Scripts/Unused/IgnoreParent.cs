using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IgnoreParent : MonoBehaviour {
    private Rect rect;
    public RectTransform reference;
    public Canvas canvas;
    bool moving;
    public GameObject parent;
    public Slider slider;
    // Update is called once per frame
    void Update() { 
        if (moving) {
            transform.SetParent(canvas.transform);
        } else {
            transform.SetParent(parent.transform);
        }
        moving = false;
    }
    public void moveSlider() {
        moving = true;
        Debug.Log("ss");
    }
}
