using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{

    public bool weaponEquipped;

    public bool pickaxeEquipped;

    //public bool spellEquipped;

    private GameObject weapon;

    private GameObject pickaxe;

    //public GameObject Spell

    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("PlayerWeapon");
        pickaxe = GameObject.FindGameObjectWithTag("PlayerPickaxe");
    }

    private void LateUpdate()
    {
        if (weaponEquipped)
        {
            weapon.SetActive(true);
            pickaxe.SetActive(false);
        }
        if (pickaxeEquipped)
        {
            weapon.SetActive(false);
            pickaxe.SetActive(true);
        }
    }

    public void ChangeWeapon()
    {
        if (weaponEquipped)
        {
            weapon.SetActive(true);
            pickaxe.SetActive(false);
        }
        if (pickaxeEquipped)
        {
            weapon.SetActive(false);
            pickaxe.SetActive(true);
        }
    }
}
