using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    UIHandler uiH;
    [SerializeField]GameManager gM;

    // Our parent grid that the UI will sit on?
    [SerializeField] GameObject gridParent;
    // The Background panel of our UI
    [SerializeField] GameObject backgroundPanel;
    // The inventory panel that will house our UI
    [SerializeField] GameObject inventoryPanel;
    // The Stats panel for our player stuff
    [SerializeField] GameObject playerStatsPanel;
    // The panel for 
    [SerializeField] GameObject playerEquipPanel;

    List<GameObject> inventoryPanels;
    List<GameObject> equipsPanels;

    public int currentIndex = 0;
    public bool currentBool = false;

    // Start is called before the first frame update
    void Start()
    {
        uiH = transform.parent.gameObject.GetComponent<UIHandler>();
        gM = GameManager.Instance;

        SetInventorySlots();
        SetEquippedItems();
        SetPlayerStats();
    }

    public void UpdateInventory()
    {
        foreach (GameObject go in inventoryPanels)
        {
            Destroy(go);
        }

        inventoryPanels.Clear();

        foreach(GameObject go in equipsPanels)
        {
            go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/Empty"); 
        }

        equipsPanels.Clear();

        SetInventorySlots();
        SetEquippedItems();
        SetPlayerStats();
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

                for(int j = 0; j < gM.ReturnEquippedItems().Length; ++j)
                {
                    if (gM.ReturnEquippedItems()[i].itemName != "null")
                    {
                        if (gM.ReturnInventory()[i].itemName == gM.ReturnEquippedItems()[j].itemName)
                        {
                            inventoryPanels[i].GetComponent<ItemElement>().isEquipped = true;
                        }

                        string[] splitString = gM.ReturnEquippedItems()[i].itemName.Split("- ");

                        switch (splitString[1])
                        {
                            case "Weapon":
                            {
                                inventoryPanels[i].GetComponent<ItemElement>().index = 0;
                                break;
                            }
                            case "Shield":
                            {
                                inventoryPanels[i].GetComponent<ItemElement>().index = 1;
                                break;
                            }
                            case "Helmet":
                            {
                                inventoryPanels[i].GetComponent<ItemElement>().index = 2;
                                break;
                            }
                            case "Chestplate":
                            {
                                inventoryPanels[i].GetComponent<ItemElement>().index = 3;
                                break;
                            }
                            case "Arms":
                            {
                                inventoryPanels[i].GetComponent<ItemElement>().index = 4;
                                break;
                            }
                            case "Legs":
                            {
                                inventoryPanels[i].GetComponent<ItemElement>().index = 5;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    void SetEquippedItems()
    {
        //Index for Children
        //0 - Weapon, 1 - Shield, 2 - Helmet, 3 - Chest, 4-5 - Arms, 6-7 - Leggings

        equipsPanels = new List<GameObject>();

        foreach (Transform trans in playerEquipPanel.GetComponentsInChildren<Transform>())
        {
            equipsPanels.Add(trans.gameObject);
        }

        //Removes Parent from List
        equipsPanels.RemoveAt(0);

        for(int i = 0; i < gM.ReturnEquippedItems().Length; ++i)
        {
            if(gM.ReturnEquippedItems()[i].itemName != "null")
            {
                string[] splitString = gM.ReturnEquippedItems()[i].itemName.Split("- ");

                switch (splitString[1])
                {
                    case "Weapon":
                    {
                        equipsPanels[0].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnEquippedItems()[i].itemImagePath);
                        break;
                    }
                    case "Shield":
                    {
                        equipsPanels[1].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnEquippedItems()[i].itemImagePath);
                        break;
                    }
                    case "Helmet":
                    {
                        equipsPanels[2].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnEquippedItems()[i].itemImagePath);
                        break;
                    }
                    case "Chestplate":
                    {
                        equipsPanels[3].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnEquippedItems()[i].itemImagePath);
                        break;
                    }
                    case "Arms":
                    {
                        equipsPanels[4].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnEquippedItems()[i].itemImagePath);
                        equipsPanels[5].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnEquippedItems()[i].itemImagePath);
                        break;
                    }
                    case "Legs":
                    {
                        equipsPanels[6].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnEquippedItems()[i].itemImagePath);
                        equipsPanels[7].GetComponent<Image>().sprite = Resources.Load<Sprite>(gM.ReturnEquippedItems()[i].itemImagePath);
                        break;
                    }
                }
            }
        }
    }

    void SetPlayerStats()
    {
        TextMeshProUGUI[] playerStatsChildren = playerStatsPanel.GetComponentsInChildren<TextMeshProUGUI>();

        playerStatsChildren[0].text = "Health: " + GameManager.Instance.ReturnIntData(GameManager.PlayerDataAttributes.CurrentHealth).ToString() + "/" + GameManager.Instance.ReturnIntData(GameManager.PlayerDataAttributes.MaxHealth);
        playerStatsChildren[1].text = "Atk Damage: " + GameManager.Instance.ReturnIntData(GameManager.PlayerDataAttributes.Damage);
        playerStatsChildren[2].text = "Defence: " + GameManager.Instance.ReturnIntData(GameManager.PlayerDataAttributes.Defence);
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
