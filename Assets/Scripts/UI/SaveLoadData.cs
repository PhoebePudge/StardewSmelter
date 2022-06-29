using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadData : MonoBehaviour {
    public void LoadOurData() {
        //call loading our data
        storedData data = LoadData(); 
        slotsFromString(data.inventory, InventorySystem.slot);
    }
    private void slotsFromString(string[] data, GameObject[] slots) {
        for (int i = 0; i < data.Length; i++) {
            //convert each line of data and split into data
            string[] line = new string[6];

            string read = "";
            int index = 0;
            foreach (char character in data[i]) {
                if (character == ',') {
                    line[index] = read;

                    index++;
                    read = "";
                } else {
                    read += character;
                }
            }

            //now override our slot data here
            Slot slot = slots[i].GetComponent<Slot>();
            string itemName = line[0];
            int maxQuantity = int.Parse(line[1]);
            string imagePath = line[2];
            string itemDescription = line[3];
            Attribute itemAttribute = (Attribute)System.Enum.Parse(typeof(Attribute), line[4]);
            int quantity = int.Parse(line[5]);

            //create slot stuff
            slot.itemdata = new ItemData(itemName, maxQuantity, imagePath, itemDescription, itemAttribute);
            slot.quantity = quantity;
            slot.UpdateSlot();
        }
    }
    public static void SaveData() {
        //set path and formatter
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/StardewSmelter"; 
        FileStream stream = new FileStream(path, FileMode.Create);

        //Get our inventory slots into string
        string[] slots = new string[InventorySystem.slot.Length];
        for (int i = 0; i < InventorySystem.slot.Length; i++) {
            slots[i] = InventorySystem.slot[i].GetComponent<Slot>().ToString() + ",";
        }

        //Get our chest slots into string
        string[] chest = new string[StorageChest.StorageSlots.Count];
        for (int i = 0; i < StorageChest.StorageSlots.Count; i++) {
            chest[i] = StorageChest.StorageSlots[i].GetComponent<Slot>().ToString() + ",";
        }

        //create our stored data
        storedData charData = new storedData(slots, chest);

        Debug.LogError(charData.ToString());

        //close stream
        formatter.Serialize(stream, charData);
        stream.Close();
    }
    public static storedData LoadData() {
        //get path
        string path = Application.persistentDataPath + "/StardewSmelter";

        //if file exists
        if (File.Exists(path)) {
            //get stream of data
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            storedData data = formatter.Deserialize(stream) as storedData;

            stream.Close();

            Debug.Log(data.ToString());
            return data;
        } else {
            //did not find data
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }
}
[System.Serializable]
public class storedData {
    //stored data class
    public string[] inventory;
    public string[] chest;
    public storedData(string[] inventory, string[] chest) {
        //creation
        this.inventory = inventory;
        this.chest = chest; 
    }
    public override string ToString() {
        //to string override, for exporting
        string output = "";
        foreach (string item in inventory) {
            output += item;
        }
        foreach (string item in chest) {
            output += item;
        }
        return output;
    } 
}