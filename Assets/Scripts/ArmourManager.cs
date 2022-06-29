using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourManager : MonoBehaviour {
    //gameobject armour references
    public GameObject helm;
    public GameObject chest;
    public GameObject legs;
    public GameObject arms;

    //compoent instance
    public static ArmourManager instance;
    void Start() {
        //set instance
        instance = this;
    } 
    public static void StaticSetArmour(Attribute attribute, bool enabled, Color[] colour) {
        //call instance set armour
        instance.SetArmour(attribute, enabled, colour);
    }
    public void SetArmour(Attribute attribute, bool enabled, Color[] colour) { 
        //set our armour and update its visual object dependant on what type of armour it is
        switch (attribute) {
            case Attribute.ArmourHead:
                helm.SetActive(enabled);
                for (int i = 0; i < helm.GetComponent<SkinnedMeshRenderer>().materials.Length; i++) { 
                    helm.GetComponent<SkinnedMeshRenderer>().materials[i].color = colour[i];
                } 
                break;
            case Attribute.ArmourChest:
                chest.SetActive(enabled);
                for (int i = 0; i < chest.GetComponent<SkinnedMeshRenderer>().materials.Length; i++) {
                    chest.GetComponent<SkinnedMeshRenderer>().materials[i].color = colour[i];
                } 
                break;
            case Attribute.ArmourBoot:
                legs.SetActive(enabled);
                for (int i = 0; i < legs.GetComponent<SkinnedMeshRenderer>().materials.Length; i++) {
                    legs.GetComponent<SkinnedMeshRenderer>().materials[i].color = colour[i];
                } 
                break;
            case Attribute.ArmourGloves:
                arms.SetActive(enabled);
                for (int i = 0; i < arms.GetComponent<SkinnedMeshRenderer>().materials.Length; i++) {
                    arms.GetComponent<SkinnedMeshRenderer>().materials[i].color = colour[i];
                } 
                break;
        }
    }
}
