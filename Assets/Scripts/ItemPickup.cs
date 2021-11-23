using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Transform heldItem; 
    private bool holding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (holding) {
                if (heldItem.GetComponent<Collider>() != null) {
                    heldItem.GetComponent<Collider>().enabled = true;
                }
                heldItem.SetParent(null);
                heldItem = null; 
                holding = false;
            } else {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 5f)) {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Pickup")) {
                        holding = true;
                        heldItem = hit.transform;
                        heldItem.SetParent(transform); 
                        heldItem.localPosition = new Vector3(); 
                        if (heldItem.GetComponent<Collider>() != null) {
                            heldItem.GetComponent<Collider>().enabled = false;
                        }
                    }
                }
            }
        }

        if (heldItem != null) { 
            //any updates for when its  being held?
        }
    } 
    private void OnDrawGizmos() {
        //Debug.DrawRay(transform.position, transform.forward, Color.green, 5f);
    }
}
