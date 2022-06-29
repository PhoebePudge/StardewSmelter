using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToolTips : MonoBehaviour {
    public string text;
    bool thisSelcted = false;
    public static ToolTips selected = null;
    private void Start() {
        //find tooltip object and deactivate it
        GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        gm.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void Update() {
        if (thisSelcted & selected == this) {
        }
    }
    void OnMouseOver() {
        //when we mouse over a object, set it to show
        GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        thisSelcted = true;
        gm.transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnMouseEnter() {
        //when over, set its text to reflect variable
        selected = this;
        GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        if (gm != null) {
            gm.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            gm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        }
    }
    private void OnMouseExit() {
        //deactivate on mouse exit
        thisSelcted = false;
        GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        gm.transform.GetChild(0).gameObject.SetActive(false);
    }
}
