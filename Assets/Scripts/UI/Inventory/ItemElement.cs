using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Context Menu")]
    [SerializeField] GameObject contextPanelEquipPrefab;
    [SerializeField] GameObject contextPanelUnequipPrefab;
    [SerializeField] GameObject contextPanelUsePrefab;
    [SerializeField] GameObject contextPanelNormalPrefab;

    [Header("Tooltip")]
    [SerializeField] GameObject tooltipPrefab;

    GameObject currentContext;
    bool contextOpen = false;

    GameObject currentTooltip;
    bool tooltipOpen = false;

    public bool isEquipped = false;
    public int index;

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

            if (isEquipped)
            {
                currentContext = Instantiate(contextPanelUnequipPrefab, transform);
            }
            else
            {
                Attribute attribute = GameManager.Instance.ReturnInventory()[index].itemAttribute;

                if (attribute == Attribute.Armor || attribute == Attribute.Weapon)
                {
                    currentContext = Instantiate(contextPanelEquipPrefab, transform);
                }
                else if (attribute == Attribute.Damage || attribute == Attribute.Defence || attribute == Attribute.Health)
                {
                    currentContext = Instantiate(contextPanelUsePrefab, transform);
                }
                else
                {
                    currentContext = Instantiate(contextPanelNormalPrefab, transform);
                }
            }

            currentContext.name = name;
            currentContext.transform.SetParent(transform.parent.parent.parent);
            currentContext.transform.SetAsLastSibling();
            currentContext.GetComponent<ContextMenu>().index = index;

            currentContext.transform.parent.GetComponent<Inventory>().currentIndex = index;
            currentContext.transform.parent.GetComponent<Inventory>().currentBool = true;

            contextOpen = true;
        }

        if (tooltipOpen)
        {
            CloseTooltip();
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

            if (isEquipped) { childText[0].text = GameManager.Instance.ReturnInventory()[index].itemName + " - Equipped"; }
            else { childText[0].text = GameManager.Instance.ReturnInventory()[index].itemName; }
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
