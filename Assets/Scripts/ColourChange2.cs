using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange2 : MonoBehaviour {
    public bool ShowColour;
    private List<Color> Colours;
    private List<Transform> trans; 
    void Start() {
        //set our origional colour
        Colours = new List<Color>();
        trans = new List<Transform>();
        foreach (Transform item in transform) {
            if (item.GetComponent<MeshRenderer>() != null) {
                trans.Add(item);
                Colours.Add(item.GetComponent<MeshRenderer>().material.color);
            }
        }
    } 
    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            ColourChange(ShowColour);
            ShowColour = !ShowColour;
        }
    }
    public void ColourChange(bool value) {
        //desaturate and saturate based on value
        if (value) {
            for (int i = 0; i < Colours.Count; i++) {
                trans[i].GetComponent<MeshRenderer>().material.color = Desaturate(Colours[i].r, Colours[i].g, Colours[i].b, .5f);
            }
        } else {

            for (int i = 0; i < Colours.Count; i++) {
                trans[i].GetComponent<MeshRenderer>().material.color = Colours[i];
            }
        }
    }

    private Color Desaturate(float r, float g, float b, float f = .2f) {
        //desaturate colour
        float L = 0.3f * r + 0.6f * g + 0.1f * b;
        float new_r = r + f * (L - r);
        float new_g = g + f * (L - g);
        float new_b = b + f * (L - b);

        return new Color(new_r, new_g, new_b);
    }
}
