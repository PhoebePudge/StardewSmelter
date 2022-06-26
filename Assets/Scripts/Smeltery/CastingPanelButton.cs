using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CastingPanelButton : MonoBehaviour
{
    static GameObject previousButton;
    public int Index;
    public void OnClick() {
        CastingPanel.selectedIndex = Index;
        MetalCastController.CastType = CastingPanel.Casts[CastingPanel.selectedIndex].types; 
        if (previousButton != null)
        {
            previousButton.transform.parent.GetComponent<Canvas>().sortingOrder = 1;
        }

        transform.parent.GetComponent<Canvas>().sortingOrder = 2;
        previousButton = gameObject;
    }
}
