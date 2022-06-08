using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{


    static WeaponManager instance;
    static ItemWeapon WeaponData;
    static bool toolActive = false;
    static GameObject toolVisual;
    [SerializeField] GameObject ToolVisual;
    [SerializeField] Animator anim;
    void Start() {
        instance = this;
        toolVisual = ToolVisual;
        ClearWeapon();
        //toolVisual.transform.GetChild(0).gameObject.SetActive(false);
    }

    public static void SetWeapon(ItemWeapon itemData) { 
        WeaponData = itemData;
        toolVisual.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", itemData.sprite.texture);
        toolVisual.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1);
        toolVisual.GetComponent<MeshRenderer>().enabled = true;
    }  
    public static void ClearWeapon()
    {
        WeaponData = null;
        toolVisual.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
        toolVisual.GetComponent<MeshRenderer>().enabled = false;
    }
    void Update() {
        if (WeaponData != null) {
            if (Input.GetMouseButtonDown(0)) {
                Debug.LogError("You are attacking");
                anim.SetTrigger("Attack");
                //StartCoroutine(ParticleSwing());
            }
        }
    }
    /*
    IEnumerator ParticleSwing() {
        toolActive = true;
        toolVisual.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        toolVisual.transform.GetChild(0).gameObject.SetActive(false);
        toolActive = false;
    }*/



















    Color otherColour;
    Color selectedColour = Color.red;
    GameObject viewedObject;
    private void OnTriggerEnter(Collider other) {
        if (WeaponData != null & other.name != "Player")
        {
            if (WeaponData.itemName.Contains("Pickaxe"))
            {
                //viewed object exists, adn this is a metal
                if (viewedObject != null & (other.name.Contains("Metal") | other.name.Contains("Rock")))
                {
                    if (other.name.Contains("Rock"))
                    {
                        if (viewedObject.name.Contains("Rock"))
                        {
                            viewedObject.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = otherColour;
                        }
                        else
                        {
                            viewedObject.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = otherColour;
                        }

                        //set new viewed
                        viewedObject = other.gameObject;
                        otherColour = other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color;
                        other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = selectedColour;
                    }
                    else
                    {
                        //return viewed object material back to normal
                        if (viewedObject.name.Contains("Rock"))
                        {
                            viewedObject.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = otherColour;
                        }
                        else
                        {
                            viewedObject.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = otherColour;
                        }

                        //set new viewed
                        viewedObject = other.gameObject;
                        otherColour = other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color;
                        other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = selectedColour;
                    }

                }
                else if (other.name.Contains("Metal") | other.name.Contains("Rock"))
                {


                    viewedObject = other.gameObject;
                    if (other.name.Contains("Rock"))
                    {
                        otherColour = other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color;
                        other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = selectedColour;
                    }
                    else
                    {
                        otherColour = other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color;
                        other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = selectedColour;
                    }
                }
            }
        }


        if (toolActive)
        {
            if (WeaponData != null & other.name != "Player")
            {
                if (WeaponData.itemName.Contains("Pickaxe"))
                { 
                    if (other.GetComponent<MineObjects>() != null)
                    {
                        if (viewedObject != null)
                        {
                            viewedObject.GetComponent<MineObjects>().addItem(WeaponData.MetalLevel);
                            toolActive = false;
                        }
                        else
                        {
                            other.GetComponent<MineObjects>().addItem(WeaponData.MetalLevel);
                            toolActive = false;
                        }
                    }
                }
                if (WeaponData.itemName.Contains("Sword"))
                {
                    if (other.gameObject.GetComponent<MonsterType>())
                    {
                        //Debug.LogError("hit enemy " + other.name + " from a " + WeaponData.itemName + " for 1 damage ");
                        other.gameObject.GetComponent<MonsterType>().Damage(WeaponData.MetalLevel + 1);
                    }
                } 
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == viewedObject)
        {
            if (other.name.Contains("Rock"))
            {
                other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = otherColour;
                viewedObject = null;
            }
            else
            {
                other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = otherColour;
                viewedObject = null;
            }
        }
    }
}
