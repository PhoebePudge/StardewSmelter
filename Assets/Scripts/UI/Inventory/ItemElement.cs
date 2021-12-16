using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Context Menu")]
    [SerializeField] GameObject contextPanelEquipPrefab;
    [SerializeField] GameObject contextPanelUsePrefab;
    [SerializeField] GameObject contextPanelNormalPrefab;

    [Header("Tooltip")]
    [SerializeField] GameObject tooltipPrefab;

    GameObject currentContext;
    bool contextOpen = false;

    GameObject currentTooltip;
    bool tooltipOpen = false;

    public void ContextMenuOpen()
    {
        int index = int.Parse(name);

        if (GameManager.Instance.ReturnInventory().Count > index)
        {
            if (contextOpen)
            {
                Destroy(currentContext);
                contextOpen = false;
            }

            ItemData.Attribute attribute = GameManager.Instance.ReturnInventory()[index].itemAttribute;

            if (attribute == ItemData.Attribute.Armor || attribute == ItemData.Attribute.Weapon)
            {
                currentContext = Instantiate(contextPanelEquipPrefab, transform);
            }
            else if (attribute == ItemData.Attribute.Damage || attribute == ItemData.Attribute.Defence || attribute == ItemData.Attribute.Health)
            {
                currentContext = Instantiate(contextPanelUsePrefab, transform);
            }
            else
            {
                currentContext = Instantiate(contextPanelNormalPrefab, transform);
            }

            currentContext.name = name;
            currentContext.transform.SetParent(transform.parent.parent.parent);
            currentContext.transform.SetAsLastSibling();

            currentContext.transform.parent.GetComponent<Inventory>().currentIndex = index;
            currentContext.transform.parent.GetComponent<Inventory>().currentBool = true;

            contextOpen = true;
        }
    }

    public void ClearContext()
    {
        if (contextOpen)
        {
            Destroy(currentContext);
            contextOpen = false;
        }
    }

    void OpenTooltip()
    {
        int index = int.Parse(name);

        if (GameManager.Instance.ReturnInventory().Count > index)
        {
            if (currentTooltip)
            {
                Destroy(currentTooltip);
                tooltipOpen = false;
            }

            currentTooltip = Instantiate(tooltipPrefab, transform);

            TextMeshProUGUI[] childText = currentTooltip.GetComponentsInChildren<TextMeshProUGUI>();

            childText[0].text = GameManager.Instance.ReturnInventory()[index].itemName;
            childText[1].text = GameManager.Instance.ReturnInventory()[index].itemDescription;

            currentTooltip.transform.SetParent(transform.parent.parent.parent);
            currentTooltip.transform.SetAsLastSibling();

            tooltipOpen = true;
        }
    }

    void CloseTooltip()
    {
        if (tooltipOpen)
        {
            Destroy(currentTooltip);
            tooltipOpen = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!contextOpen)
        {
            OpenTooltip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!eventData.pointerCurrentRaycast.gameObject.CompareTag("Tooltip"))
        {
            CloseTooltip();
        }
    }
}
