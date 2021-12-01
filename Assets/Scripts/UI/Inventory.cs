using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    UIHandler uiH;
    GameManager gM;

    [SerializeField] GameObject gridParent;
    [SerializeField] GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        uiH = transform.parent.gameObject.GetComponent<UIHandler>();
        gM = GameManager.Instance;

        SetInventorySlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Play();
        }
    }

    public void Play()
    {
        uiH.events = UIHandler.UIEVENTS.PLAY;
    }

    void SetInventorySlots()
    {
        List<GameObject> inventoryPanels = new List<GameObject>();

        for (int i = 0; i < gM.ReturnIntData(GameManager.PlayerDataAttributes.MaxSlots); ++i)
        {
            inventoryPanels.Add(Instantiate(inventoryPanel, gridParent.transform));
            inventoryPanels[i].name = i.ToString();
        }

        if(gM.ReturnIntData(GameManager.PlayerDataAttributes.TotalCount) > 1)
        {
            for(int i = 0; i < gM.ReturnInventory().Count; ++i)
            {
                inventoryPanels[i].GetComponent<Image>().sprite = (Sprite)Resources.Load(gM.ReturnInventory()[i].ItemData.itemImagePath);
                inventoryPanels[i].GetComponentInChildren<Text>().text = gM.ReturnInventoryCount()[i].ToString();
            }
        }
    }

    public void ContextMenu()
    {

    }
}
