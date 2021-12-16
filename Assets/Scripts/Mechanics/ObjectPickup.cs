using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour {
    private Transform heldItem; 
    private bool holding = false; 
 
    void Update() {
        if (Input.GetKeyDown(KeyCode.V)) {
            if (holding) {
                if (heldItem.GetComponent<Collider>() != null) {
                    heldItem.GetComponent<Collider>().enabled = true;
                }
                heldItem.GetComponent<Rigidbody>().useGravity = true;
                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem.SetParent(null);
                heldItem = null; 
                holding = false; 
            } 
        }

        if (heldItem != null) { 
            //any updates for when its  being held?
        }
    } 
    private void OnTriggerStay(Collider other) {
        if (other.name != "Player") { 
            if (Input.GetKey(KeyCode.C) & holding == false) { 
                if (other.transform.gameObject.layer == LayerMask.NameToLayer("Pickup")) {
                    holding = true;
                    heldItem = other.transform;
                    heldItem.SetParent(transform);
                    heldItem.localPosition = new Vector3();
                    heldItem.GetComponent<Rigidbody>().useGravity = false;
                    heldItem.GetComponent<Rigidbody>().isKinematic = true;
                    heldItem.rotation = heldItem.rotation * transform.rotation;
                    if (heldItem.GetComponent<Collider>() != null) {
                        heldItem.GetComponent<Collider>().enabled = false;
                    }
                }
            }
        }
    }
}
