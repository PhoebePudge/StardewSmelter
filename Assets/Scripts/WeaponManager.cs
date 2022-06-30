using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes {
    None,
    Axe,
    Dagger,
    Pickaxe,
    Sword,
    Claymore,
    ShortSword,
    WarHammer
}
public class WeaponManager : MonoBehaviour {
    //Each visual object representation
    [SerializeField] GameObject WeaponAxe;
    [SerializeField] GameObject WeaponDagger;
    [SerializeField] GameObject WeaponPickaxe;
    [SerializeField] GameObject WeaponSword;
    [SerializeField] GameObject WeaponClaymore;
    [SerializeField] GameObject WeaponShortSword;
    [SerializeField] GameObject WeaponWarHammer;

    //Reference to whatever weapon we are currently using
    private static Transform selectedWeapon;

    //Component instance
    static WeaponManager instance;

    //Weapon data of selected weapon
    public static ItemWeapon WeaponData;

    //probably can be removed
    [SerializeField] GameObject ToolVisual;

    //player animation
    [SerializeField] Animator anim;
    void Start() {
        instance = this;
        ClearWeapon();
    }

    //set our new or updated weapon
    public static void SetWeapon(ItemWeapon itemData) {

        //set variables
        WeaponData = itemData;

        //set animator
        instance.anim.SetBool("HoldingWeapon", true);


        //set previously selected weapon to disabled
        if (selectedWeapon != null) {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = null;
        }

        //dependant on weapon type we have, choose its gameobject and set it to the correct animation
        switch (itemData.type) {
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


        //Set weapon materials to reflect materials on weapon object
        for (int i = 0; i < selectedWeapon.GetComponent<MeshRenderer>().materials.Length; i++) { 
            if (WeaponData.metals[i] != null) {
                selectedWeapon.GetComponent<MeshRenderer>().materials[i].color = WeaponData.metals[i].col; 
            }
        }
    }

    //Clear currently selected wepon
    public static void ClearWeapon() {
        //fix animation
        instance.anim.SetBool("HoldingWeapon", false);

        //disabled object
        if (selectedWeapon != null) {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = null;
        }

        //clear data
        WeaponData = null;
    }
    void Update() {
        //if we have a weapon equiped
        if (WeaponData != null) {
            //and we attack
            if (Input.GetMouseButtonDown(0)) {
                anim.SetTrigger("Attack");

                //if its a pickaxe
                if (WeaponData.type == WeaponTypes.Pickaxe) {
                    //we have a object to mine highlighted
                    if (HighlightMineable.selected != null) {
                        //look at object
                        Quaternion rotation = Quaternion.LookRotation(HighlightMineable.selected.transform.position);
                        rotation.x = 0;
                        rotation.z = 0;
                        gameObject.transform.parent.transform.rotation = rotation;

                        //call mine object code
                        HighlightMineable.selected.GetComponent<MineObjects>().addItem(WeaponData.MetalLevel);
                    }
                }
            }
        }
    } 
    private void OnTriggerStay(Collider other) {
        if (WeaponData == null) {
            return;
        }
        //when we collider with a monster and click attack
        if (Input.GetMouseButtonDown(0)) {
            if (WeaponData.itemName.Contains("Sword")) {
                if (other.gameObject.GetComponent<MonsterType>()) {

                    //deal our damage
                    other.gameObject.GetComponent<MonsterType>().Damage(WeaponData.MetalLevel + 1);
                }
            }
        }
    }
}
