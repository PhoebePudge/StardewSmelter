using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    None,
    Axe,
    Dagger,
    Pickaxe,
    Sword,
    Claymore,
    ShortSword,
    WarHammer
}
public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject WeaponAxe;
    [SerializeField] GameObject WeaponDagger;
    [SerializeField] GameObject WeaponPickaxe;
    [SerializeField] GameObject WeaponSword;
    [SerializeField] GameObject WeaponClaymore;
    [SerializeField] GameObject WeaponShortSword;
    [SerializeField] GameObject WeaponWarHammer;


    private static Transform selectedWeapon;

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

        instance.anim.SetBool("HoldingWeapon", true);

        if (selectedWeapon != null)
        {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = null;
        }

        switch (itemData.type)  
        {
            case WeaponTypes.None:
                break;
            case WeaponTypes.Axe:
                instance.WeaponAxe.SetActive(true);
                selectedWeapon = instance.WeaponAxe.transform;
                instance.anim.SetInteger("WeaponType", 0);

                break;
            case WeaponTypes.Dagger:
                instance.WeaponDagger.SetActive(true);
                selectedWeapon = instance.WeaponDagger.transform;
                instance.anim.SetInteger("WeaponType", 1);

                break;
            case WeaponTypes.Pickaxe:
                instance.WeaponPickaxe.SetActive(true);
                selectedWeapon = instance.WeaponPickaxe.transform;
                instance.anim.SetInteger("WeaponType", 2);

                break;
            case WeaponTypes.Sword:
                instance.WeaponSword.SetActive(true);
                selectedWeapon = instance.WeaponSword.transform;
                instance.anim.SetInteger("WeaponType", 3);

                break;
            case WeaponTypes.Claymore:
                instance.WeaponClaymore.SetActive(true);
                selectedWeapon = instance.WeaponClaymore.transform;
                instance.anim.SetInteger("WeaponType", 4);

                break;
            case WeaponTypes.ShortSword:
                instance.WeaponShortSword.SetActive(true);
                selectedWeapon = instance.WeaponShortSword.transform;
                instance.anim.SetInteger("WeaponType", 3);

                break;
            case WeaponTypes.WarHammer:
                instance.WeaponWarHammer.SetActive(true);
                selectedWeapon = instance.WeaponWarHammer.transform;
                instance.anim.SetInteger("WeaponType", 4);

                break;
            default:
                break;
        }
         
        for (int i = 0; i < selectedWeapon.GetComponent<MeshRenderer>().materials.Length; i++)
        { 
            Debug.LogError(WeaponData.metals[i].col);
            if (WeaponData.metals[i] != null)
            {
                selectedWeapon.GetComponent<MeshRenderer>().material.color = WeaponData.metals[i].col; 

                Debug.LogError("Setting new mat here to " + WeaponData.metals[i].col);
            }  
        } 
    }  
    public static void ClearWeapon()
    {
        instance.anim.SetBool("HoldingWeapon", false);

        if (selectedWeapon != null)
        {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = null;
        }
        WeaponData = null;
        //toolVisual.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
        //toolVisual.GetComponent<MeshRenderer>().enabled = false;
    }
    void Update() {
        if (WeaponData != null) {
            if (Input.GetMouseButtonDown(0)) { 
                anim.SetTrigger("Attack");
                //StartCoroutine(ParticleSwing());
            }
        }
    }

    //Color otherColour;
    //Color selectedColour = Color.red;
    //GameObject viewedObject;
    private void OnTriggerEnter(Collider other) {
        //if (WeaponData != null & other.name != "Player")
        //{
        //    if (WeaponData.itemName.Contains("Pickaxe"))
        //    {
        //        //viewed object exists, adn this is a metal
        //        if (viewedObject != null & (other.name.Contains("Metal") | other.name.Contains("Rock")))
        //        {
        //            if (other.name.Contains("Rock"))
        //            {
        //                if (viewedObject.name.Contains("Rock"))
        //                {
        //                    viewedObject.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = otherColour;
        //                }
        //                else
        //                {
        //                    viewedObject.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = otherColour;
        //                }

        //                //set new viewed
        //                viewedObject = other.gameObject;
        //                otherColour = other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color;
        //                other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = selectedColour;
        //            }
        //            else
        //            {
        //                //return viewed object material back to normal
        //                if (viewedObject.name.Contains("Rock"))
        //                {
        //                    viewedObject.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = otherColour;
        //                }
        //                else
        //                {
        //                    viewedObject.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = otherColour;
        //                }

        //                //set new viewed
        //                viewedObject = other.gameObject;
        //                otherColour = other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color;
        //                other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = selectedColour;
        //            }

        //        }
        //        else if (other.name.Contains("Metal") | other.name.Contains("Rock"))
        //        {


        //            viewedObject = other.gameObject;
        //            if (other.name.Contains("Rock"))
        //            {
        //                otherColour = other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color;
        //                other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = selectedColour;
        //            }
        //            else
        //            {
        //                otherColour = other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color;
        //                other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = selectedColour;
        //            }
        //        }
        //    }
        //}


        if (toolActive)
        {
            if (WeaponData != null & other.name != "Player")
            {
                if (WeaponData.itemName.Contains("Pickaxe"))
                { 
                    if (other.GetComponent<MineObjects>() != null)
                    {
                        //if (viewedObject != null)
                        //{
                        //    other.GetComponent<MineObjects>().addItem(WeaponData.MetalLevel);
                        //    toolActive = false;
                        //}
                        //else
                        //{
                            other.GetComponent<MineObjects>().addItem(WeaponData.MetalLevel);
                            toolActive = false;
                        //}
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
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject == viewedObject)
    //    {
    //        if (other.name.Contains("Rock"))
    //        {
    //            other.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = otherColour;
    //            viewedObject = null;
    //        }
    //        else
    //        {
    //            other.transform.GetChild(1).transform.GetComponent<Renderer>().material.color = otherColour;
    //            viewedObject = null;
    //        }
    //    }
    //}
}
