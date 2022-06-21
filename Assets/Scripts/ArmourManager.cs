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
    public static void StaticSetArmour(Attribute attribute, bool enabled)
    {
        instance.SetArmour(attribute, enabled); 
    }
    public void SetArmour(Attribute attribute, bool enabled)
    {
        Debug.LogError("You are setting " + attribute.ToString() + " to " + enabled);

        switch (attribute)
        {
            case Attribute.ArmourHead:
                helm.SetActive(enabled);
                break;
            case Attribute.ArmourChest:
                chest.SetActive(enabled);
                break;
            case Attribute.ArmourBoot:
                legs.SetActive(enabled);
                break;
            case Attribute.ArmourGloves:
                arms.SetActive(enabled);
                break;
        }
    }
}
