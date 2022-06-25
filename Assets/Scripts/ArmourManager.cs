using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourManager : MonoBehaviour
{
    public GameObject helm;
    public GameObject chest;
    public GameObject legs;
    public GameObject arms;

    public static ArmourManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void StaticSetArmour(Attribute attribute, bool enabled, Color[] colour)
    {
        instance.SetArmour(attribute, enabled, colour); 
    }
    public void SetArmour(Attribute attribute, bool enabled, Color[] colour)
    {
        Debug.LogError("You are setting " + attribute.ToString() + " to " + enabled);

        switch (attribute)
        {
            case Attribute.ArmourHead:
                helm.SetActive(enabled); 
                for (int i = 0; i < helm.GetComponent<SkinnedMeshRenderer>().materials.Length; i++)
                {
                    Debug.LogError(colour[i]);
                    helm.GetComponent<SkinnedMeshRenderer>().materials[i].color = colour[i];
                }

                break;
            case Attribute.ArmourChest:
                chest.SetActive(enabled);
                for (int i = 0; i < chest.GetComponent<SkinnedMeshRenderer>().materials.Length; i++)
                {
                    chest.GetComponent<SkinnedMeshRenderer>().materials[i].color = colour[i];
                }

                break;
            case Attribute.ArmourBoot:
                legs.SetActive(enabled);
                for (int i = 0; i < legs.GetComponent<SkinnedMeshRenderer>().materials.Length; i++)
                {
                    legs.GetComponent<SkinnedMeshRenderer>().materials[i].color = colour[i];
                }

                break;
            case Attribute.ArmourGloves:
                arms.SetActive(enabled);
                for (int i = 0; i < arms.GetComponent<SkinnedMeshRenderer>().materials.Length; i++)
                {
                    arms.GetComponent<SkinnedMeshRenderer>().materials[i].color = colour[i];
                }

                break;
        }
    }
}
