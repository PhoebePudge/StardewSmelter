using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    static WeaponManager instance;
    static ItemData WeaponData;
    static bool toolActive = false;
    void Start() {
        instance = this;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public static void SetTexture(ItemData itemData) { 
        WeaponData = itemData;
        instance.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", itemData.sprite.texture); 
        instance.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1); 
    }  
    public static void ClearTexture()
    {
        WeaponData = null;  
        instance.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
    }
    void Update() {
        if (WeaponData != null) {
            if (Input.GetMouseButtonDown(0)) {
                GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Animator>().Play("Player Swing");
                StartCoroutine(ParticleSwing());
            }
        }
    }
    IEnumerator ParticleSwing() {
        toolActive = true;
        transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        transform.GetChild(0).gameObject.SetActive(false);
        toolActive = false;
    }
    private void OnTriggerEnter(Collider other) {
        if (toolActive)
        {
            if (WeaponData != null & other.name != "Player")
            {
                if (WeaponData.itemName.Contains("Pickaxe"))
                {
                    if (other.GetComponent<MineObjects>() != null)
                    {
                        Debug.LogError("ww2");
                        other.GetComponent<MineObjects>().addItem();
                        toolActive = false;
                    }
                }
                if (WeaponData.itemName.Contains("Sword"))
                {
                    
                    if (other.gameObject.GetComponent<MonsterType>())
                    {
                        Debug.LogError("hit enemy " + other.name + " from a " + WeaponData.itemName + " for 1 damage ");
                        other.gameObject.GetComponent<MonsterType>().Damage(1);
                    }
                } 
            }
        }
    }
}
