using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    UIHandler uiH;
    [SerializeField]GameManager gM;

    [SerializeField] GameObject gridParent;
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] GameObject inventoryPanel;

    List<GameObject> inventoryPanels = new List<GameObject>();

    public int currentIndex = 0;
    public bool currentBool = false;

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
        inventoryPanels = new List<GameObject>();

        for (int i = 0; i < gM.ReturnIntData(GameManager.PlayerDataAttributes.MaxSlots); ++i)
        {
            inventoryPanels.Add(Instantiate(inventoryPanel, gridParent.transform));
            inventoryPanels[i].name = i.ToString();
        }

        if(gM.ReturnIntData(GameManager.PlayerDataAttributes.TotalCount) > 0)
        {
            for(int i = 0; i < gM.ReturnInventory().Count; ++i)
            {
                inventoryPanels[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnInventory()[i].itemImagePath);
                inventoryPanels[i].GetComponentInChildren<Text>().text = gM.ReturnInventoryCount()[i].ToString();
            }
        }
    }

    public void CloseContext()
    {
        if (currentBool)
        {
            inventoryPanels[currentIndex].GetComponent<ItemElement>().ClearContext();
            currentBool = false;
        }
    }
}
