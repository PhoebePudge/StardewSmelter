using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        SaveData();
    //    }
    //    if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        storedData data = LoadData();

    //        slotsFromString(data.inventory, InventorySystem.slot);
    //    }
    //}
    public void LoadOurData()
    {
        storedData data = LoadData();

        slotsFromString(data.inventory, InventorySystem.slot);
    }
    private void slotsFromString(string[] data, GameObject[] slots)
    {
        for (int i = 0; i < data.Length; i++)
        { 
            string[] line = new string[6];

            string read = "";
            int index = 0;
            foreach (char character in data[i])
            {
                if (character == ',')
                { 
                    line[index] = read;
                    
                    index++;
                    read = ""; 
                }
                else
                {
                    read += character;
                }
            }

            //now override our slots here
            Slot slot = slots[i].GetComponent<Slot>();
            string itemName = line[0];
            int maxQuantity = int.Parse(line[1]);
            string imagePath = line[2];
            string itemDescription = line[3];
            Attribute itemAttribute = (Attribute)System.Enum.Parse(typeof(Attribute), line[4]);
            int quantity = int.Parse(line[5]);


            slot.itemdata = new ItemData(itemName, maxQuantity, imagePath, itemDescription, itemAttribute);
            slot.quantity = quantity;
            slot.UpdateSlot();
        }
    }
    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/StardewSmelter";
        Debug.LogError(Application.persistentDataPath + "/StardewSmelter");
        FileStream stream = new FileStream(path, FileMode.Create);

        //Get our inventory slots
        string[] slots = new string[InventorySystem.slot.Length];
        for (int i = 0; i < InventorySystem.slot.Length; i++)
        {
            slots[i] = InventorySystem.slot[i].GetComponent<Slot>().ToString() + ","; 
        }

        string[] chest = new string[StorageChest.StorageSlots.Count];
        for (int i = 0; i < StorageChest.StorageSlots.Count; i++)
        {
            chest[i] = StorageChest.StorageSlots[i].GetComponent<Slot>().ToString() + ",";
        }

        storedData charData = new storedData(slots, chest);

        Debug.LogError(charData.ToString());

        formatter.Serialize(stream, charData);
        stream.Close();
    }
    public static storedData LoadData()
    {
        string path = Application.persistentDataPath + "/StardewSmelter";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            storedData data = formatter.Deserialize(stream) as storedData;

            stream.Close();

            Debug.Log(data.ToString());
            return data;
        }
        else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }
}
[System.Serializable]
public class storedData
{
    public string[] inventory;
    public string[] chest;
    public storedData(string[] inventory, string[] chest)
    {
        this.inventory = inventory;
        this.chest = chest;

        Debug.LogError(this.inventory);
        Debug.LogError(this.chest);
    }
    public override string ToString()
    {
        string output = "";
        foreach (string item in inventory)
        {
            output += item;
        }
        foreach (string item in chest)
        {
            output += item;
        }
        return output;
    }

}