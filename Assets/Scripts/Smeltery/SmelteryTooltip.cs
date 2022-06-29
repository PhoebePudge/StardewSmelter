using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SmelteryTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    static SmelteryTooltip selected;
    public GameObject toolTip;
    public string metalName = "Name";
    public string quanity = "5";

    void Update() {
        if (selected != null) {
            //if we have a tooltip metal selected, update its data to tooltip
            toolTip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = metalName;
            toolTip.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quanity;

            toolTip.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //set tooltip to this
        selected = this;
    }
    public void OnPointerExit(PointerEventData eventData) {
        //clear tooltip
        selected = null;
    }
}
