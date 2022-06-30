using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SmelteryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public int index;
    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    public void OnPointerEnter(PointerEventData eventData) {
        SmelteryDisplayPanel.mouseTarget = transform;
        SmelteryDisplayPanel.mouseIndex = index;
    }
    public void OnPointerExit(PointerEventData eventData) {
        SmelteryDisplayPanel.mouseTarget = null;
    }
    void OnClick() {
        SmelteryDisplayPanel.SelectedMetalIndex = index;
        SmelteryDisplayPanel.UpdatePanel = true;
    }
}
