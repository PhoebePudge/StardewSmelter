using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameManagerBootstrapper
{
    [RuntimeInitializeOnLoadMethod]
    static void Initalise()
    {
        GameObject gO = new GameObject();
        gO.name = "Game Manager";
        gO.AddComponent<GameManager>();
    }
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] PlayerData playerData;

    void Awake()
    {
        instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    #region Save & Load
    public bool LoadData()
    {
        string dataPath = Application.persistentDataPath + "/player.data";
        if (File.Exists(dataPath))
        {
            BinaryFormatter bF = new BinaryFormatter();

            FileStream stream = new FileStream(dataPath, FileMode.Open);
            stream.Position = 0;
            playerData = (PlayerData)bF.Deserialize(stream);
            stream.Close();
            return true;
        }
        else { return false; }
    }

    public void SaveData()
    {
        BinaryFormatter bF = new BinaryFormatter();
        
        string dataPath = Application.persistentDataPath + "/player.data";
        if (File.Exists(dataPath)) { File.Delete(dataPath); }

        FileStream stream = new FileStream(dataPath, FileMode.Create);

        bF.Serialize(stream, playerData);
        stream.Close();
    }

    public void SceneChanger(int sceneIndex) { SceneManager.LoadScene(sceneIndex); }

    public void InitaliseData() { playerData = InitalisePlayerData(); SaveData(); }
    #endregion

    #region Player Data
    public enum PlayerDataAttributes
    {
        CurrentHealth,
        MaxHealth,
        Damage,
        Defence,
        MaxSlots,
        TotalCount,
        Currency,
        Scene
    }

    //Returns any Int Values that are in our Player Data
    public int ReturnIntData(PlayerDataAttributes attributes)
    {
        switch (attributes)
        {
            case PlayerDataAttributes.CurrentHealth: { return playerData.m_currentHealth; }
            case PlayerDataAttributes.MaxHealth: { return playerData.m_maxHealth; }
            case PlayerDataAttributes.Damage: { return playerData.m_damage; }
            case PlayerDataAttributes.Defence: { return playerData.m_defence; }
            case PlayerDataAttributes.MaxSlots: { return playerData.m_maxInventorySlots; }
            case PlayerDataAttributes.TotalCount: { return playerData.m_totalInventoryCount; }
            case PlayerDataAttributes.Currency: { return playerData.m_currency; }
            case PlayerDataAttributes.Scene: { return playerData.m_currentSceneIndex; }
            default: { return 0; }
        }
    }

    //Modify any Int Values that are in our Player Data
    public void ModifyIntData(PlayerDataAttributes attributes, int value)
    {
        switch (attributes)
        {
            case PlayerDataAttributes.CurrentHealth: { playerData.m_currentHealth = value; break; }
            case PlayerDataAttributes.MaxHealth: { playerData.m_maxHealth = value; break; }
            case PlayerDataAttributes.Damage: { playerData.m_damage = value; break; }
            case PlayerDataAttributes.Defence: { playerData.m_defence = value; break; }
            case PlayerDataAttributes.MaxSlots: { playerData.m_maxInventorySlots = value; break; }
            case PlayerDataAttributes.TotalCount: { playerData.m_totalInventoryCount = value; break; }
            case PlayerDataAttributes.Currency: { playerData.m_currency = value; break; }
            case PlayerDataAttributes.Scene: { playerData.m_currentSceneIndex = value; break; }
        }
    }

    //Returns Inventory or Inventory Count
    public List<ItemData> ReturnInventory() { return playerData.m_inventory; }
    public List<int> ReturnInventoryCount() { return playerData.m_inventoryCount; }

    //Adds New or Existing Items into the List
    public void AddItem(ItemData item)
    {
        if(playerData.m_inventory.Count == 0)
        {
            playerData.m_inventory.Add(item);
            playerData.m_inventoryCount.Add(1);
            playerData.m_totalInventoryCount += 1;
        }
        else
        {
            for (int i = 0; i < playerData.m_inventory.Count; ++i)
            {
                if (playerData.m_inventory[i].itemName == item.itemName) { playerData.m_inventoryCount[i] += 1; playerData.m_totalInventoryCount += 1; }
                else { playerData.m_inventory.Add(item); playerData.m_inventoryCount.Add(1); playerData.m_totalInventoryCount += 1; }
            }
        }       
    }

    //Removes 1 or All Items into the List
    public void RemoveItem(ItemData item)
    {
        for(int i = 0; i < playerData.m_inventory.Count; ++i)
        {
            if (playerData.m_inventory[i].itemName == item.itemName)
            {
                if (playerData.m_inventoryCount[i] > 1) { playerData.m_inventoryCount[i] -= 1; playerData.m_totalInventoryCount -= 1; }
                else if (playerData.m_inventoryCount[i] == 1)
                {
                    playerData.m_inventory.RemoveAt(i);
                    playerData.m_inventoryCount.RemoveAt(i);
                    playerData.m_totalInventoryCount -= 1;
                }
            }
        }
    }

    //Initalises a New Set of Data
    PlayerData InitalisePlayerData()
    {
        PlayerData newPlayerData = new PlayerData
        {
            m_maxHealth = 100,
            m_currentHealth = 100,
            m_damage = 10,
            m_defence = 10,
            m_currency = 0,
            m_maxInventorySlots = 8,
            m_totalInventoryCount = 0,
            m_inventory = new List<ItemData>(),
            m_inventoryCount = new List<int>(),
            m_currentSceneIndex = 1
        };

        return newPlayerData;
    }
    #endregion

    void OnApplicationQuit()
    {
        SaveData();
    }
}

[System.Serializable]
struct PlayerData
{
    //Health
    public int m_currentHealth;
    public int m_maxHealth;

    //Damage and Defence
    public int m_damage;
    public int m_defence;

    //Inventory
    public List<ItemData> m_inventory;
    public List<int> m_inventoryCount;
    public int m_totalInventoryCount;
    public int m_maxInventorySlots;

    //Equipped Items
    public List<ItemData> m_currentEquippedItems;


    //Currency
    public int m_currency;

    //Engine Data
    public int m_currentSceneIndex;
}