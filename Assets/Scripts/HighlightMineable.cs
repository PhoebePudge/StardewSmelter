using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightMineable : MonoBehaviour
{
    public static GameObject selected = null;
    private Color previousColour;
    private Color Desaturate(float r, float g, float b, float f = .2f)
    {
        float L = 0.3f * r + 0.6f * g + 0.1f * b;
        float new_r = r + f * (L - r);
        float new_g = g + f * (L - g);
        float new_b = b + f * (L - b);

        return new Color(new_r, new_g, new_b);
    }
    private void setSelected(GameObject other)
    { 
        selected = other; 
        previousColour = selected.GetComponent<MeshRenderer>().material.color;
        selected.GetComponent<MeshRenderer>().material.color = Desaturate(previousColour.r, previousColour.g, previousColour.b);
        selected.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    private void clearSelected(GameObject previous)
    { 
        selected.GetComponent<MeshRenderer>().material.color = previousColour; 
        selected = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (WeaponManager.WeaponData.type != WeaponTypes.Pickaxe)
        {
            return;
        }
        if (other.gameObject.GetComponent<MineObjects>())
        {
            if (selected == null)
            {
                setSelected(other.gameObject);
            }
            if (other.gameObject != selected)
            {
                clearSelected(other.gameObject);
                setSelected(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.gameObject.GetComponent<MineObjects>())
        {
            if (other.gameObject == selected)
            {
                clearSelected(other.gameObject);
            }
        }
    }
}
