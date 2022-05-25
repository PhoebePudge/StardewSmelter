using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SmelteryTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    static SmelteryTooltip selected;

    public GameObject toolTip;

    public string name = "Name";
    public string quanity = "5";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (selected != null) { 
            toolTip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            toolTip.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quanity;

            toolTip.transform.position = Input.mousePosition;
        } 
    }

    public void OnPointerEnter(PointerEventData eventData) {
        selected = this;
    }
    public void OnPointerExit(PointerEventData eventData) {
        selected = null;
    }
}
