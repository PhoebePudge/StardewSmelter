using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
public class SmelteryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SmelteryDisplayPanel.mouseTarget = transform;
        SmelteryDisplayPanel.mouseIndex = index;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SmelteryDisplayPanel.mouseTarget = null; 
    }
    void OnClick()
    {
        SmelteryDisplayPanel.SelectedMetalIndex = index;
        SmelteryDisplayPanel.UpdatePanel = true;
        Debug.LogError("You clicked a button " + index);
    }
}
