using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToolTips : MonoBehaviour
{
    public string text;
    bool thisSelcted = false;
    //static bool isActive;
    public static ToolTips selected = null;
    private void Start()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        gm.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void Update()
    {
        //GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        //gm.transform.GetChild(0).gameObject.SetActive(false);

        if (thisSelcted & selected == this)
        {
            //gm.transform.GetChild(0).gameObject.SetActive(true);
            //Debug.LogError("You selected this");
        }
    }
    void OnMouseOver()
    {
        //Debug.LogError(isActive);
        GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        thisSelcted = true;
        //if (!isActive)
        //    isActive = true;

        gm.transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnMouseEnter()
    {
        selected = this;
        GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        if (gm != null)
        {
            //gm.SetActive(true);
            gm.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            //gm.transform.position = Input.mousePosition;
            gm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        }
        
    }
    private void OnMouseExit()
    {
        thisSelcted = false;
        //Debug.LogError("Exit");
        GameObject gm = GameObject.FindGameObjectWithTag("Tooltip");
        gm.transform.GetChild(0).gameObject.SetActive(false);
        //isActive = false;
    }
     
}
