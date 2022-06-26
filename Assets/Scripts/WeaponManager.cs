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
                selectedWeapon.GetComponent<MeshRenderer>().materials[i].color = WeaponData.metals[i].col; 

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
    }
    void Update() {
        if (WeaponData != null) {
            if (Input.GetMouseButtonDown(0)) { 
                anim.SetTrigger("Attack"); 
                if (WeaponData.itemName.Contains("Pickaxe"))
                { 
                    if (HighlightMineable.selected != null)
                    {
                        Debug.LogError("You mine " + HighlightMineable.selected.name);
                        HighlightMineable.selected.GetComponent<MineObjects>().addItem(WeaponData.MetalLevel);
                    }
                } 
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if (Input.GetMouseButtonDown(0)) {
            if (WeaponData.itemName.Contains("Sword")) {
                if (other.gameObject.GetComponent<MonsterType>()) {
                    other.gameObject.GetComponent<MonsterType>().Damage(WeaponData.MetalLevel + 1);
                }
            }
        }
    }
    //private void OnTriggerStay(Collider other)
    //{ 
    //if (Input.GetMouseButtonDown(0))
    //{
    //    if (WeaponData != null & other.name != "Player")
    //    {
    //        if (WeaponData.itemName.Contains("Pickaxe"))
    //        {
    //            if (other.GetComponent<MineObjects>() != null)
    //            {
    //                Debug.LogError("You mined it here");
    //                other.GetComponent<MineObjects>().addItem(WeaponData.MetalLevel);
    //                toolActive = false;
    //            }
    //        }
    //        if (WeaponData.itemName.Contains("Sword"))
    //        {
    //            if (other.gameObject.GetComponent<MonsterType>())
    //            {
    //                other.gameObject.GetComponent<MonsterType>().Damage(WeaponData.MetalLevel + 1);
    //            }
    //        }
    //    }
    //}
    //} 
}
