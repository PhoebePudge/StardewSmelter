using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
    }
}
