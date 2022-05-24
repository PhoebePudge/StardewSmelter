using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourOutline : MonoBehaviour
{ 
    public bool ShowColour
    {
        get { return ShowColour; }
        set
        {
            ShowColour = value;
            ColourChange(value);
        }
    }

    private Material[] materials;
    public Material DefaultMaterial;
    // Start is called before the first frame update
    void Start()
    {
        materials = transform.GetComponent<Renderer>().materials;
    }

    void ColourChange(bool value)
    {
        Debug.LogError("ssssss");
        if (value)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                transform.GetComponent<Renderer>().materials[i] = DefaultMaterial;
            }
        }
        else
        {
            for (int i = 0; i < materials.Length; i++)
            {
                transform.GetComponent<Renderer>().materials[i] = materials[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ShowColour = !ShowColour;
        }
    }
    
}
