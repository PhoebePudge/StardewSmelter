using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightMineable : MonoBehaviour {
    public static GameObject selected = null;
    private Color previousColour;
    private Color Desaturate(float r, float g, float b, float f = .2f) {
        //desaturate colour
        float L = 0.3f * r + 0.6f * g + 0.1f * b;
        float new_r = r + f * (L - r);
        float new_g = g + f * (L - g);
        float new_b = b + f * (L - b);

        return new Color(new_r, new_g, new_b);
    }
    private void setSelected(GameObject other) {
        //set variables
        selected = other;
        previousColour = selected.GetComponent<MeshRenderer>().material.color;

        //update to new colour
        selected.GetComponent<MeshRenderer>().material.color = Desaturate(previousColour.r, previousColour.g, previousColour.b);
        selected.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    private void clearSelected(GameObject previous) {
        //set back to origional colour
        selected.GetComponent<MeshRenderer>().material.color = previousColour;
        selected = null;
    }
    private void OnTriggerEnter(Collider other) {
        //if we dont have any weapon equiped, return
        if (WeaponManager.WeaponData == null) {
            return;
        }

        //if we dont have pickaxe equiped, return
        if (WeaponManager.WeaponData.type != WeaponTypes.Pickaxe) {
            return;
        }

        //if we are looking at mineable object
        if (other.gameObject.GetComponent<MineObjects>()) {
            //set selected if we dont have one alreay
            if (selected == null) {
                setSelected(other.gameObject);
            }

            //clear previous selected, and set this new one
            if (other.gameObject != selected) {
                clearSelected(other.gameObject);
                setSelected(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        //Clear object if we leave it
        if (other.gameObject.GetComponent<MineObjects>()) {
            if (other.gameObject == selected) {
                clearSelected(other.gameObject);
            }
        }
    }
}
