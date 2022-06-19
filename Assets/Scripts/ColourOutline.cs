using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourOutline : MonoBehaviour
{
    public bool ShowColour;

    private List<Color> Colours;
    // Start is called before the first frame update
    void Start()
    {
        Colours = new List<Color>();
        for (int i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
        {
            Colours.Add(GetComponent<MeshRenderer>().materials[i].color);
        }
    }

    public void ColourChange(bool value)
    {  
        if (value)
        { 

            for (int i = 0; i < Colours.Count; i++)
            { 
                transform.GetComponent<MeshRenderer>().materials[i].color = Desaturate(Colours[i].r, Colours[i].g, Colours[i].b, .5f);
            }
        }
        else
        { 

            for (int i = 0; i < Colours.Count; i++)
            { 
                transform.GetComponent<MeshRenderer>().materials[i].color = Colours[i]; 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    ColourChange(ShowColour);
        //    ShowColour = !ShowColour;
        //}
    }
    private Color Desaturate(float r, float g, float b, float f = .2f)
    { 
        float L = 0.3f * r + 0.6f * g + 0.1f * b;
        float new_r = r + f * (L - r);
        float new_g = g + f * (L - g);
        float new_b = b + f * (L - b);

        return new Color (new_r, new_g, new_b);
    }

}
