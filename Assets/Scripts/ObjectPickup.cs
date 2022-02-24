using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour {
    public Transform heldItem;
    public bool holding = false;

    [SerializeField] GameObject pickupIconObject;
    private GameObject pickupIcon;
    private void Start() {
        pickupIcon = Instantiate(pickupIconObject);
        pickupIcon.SetActive(false);
        pickupIcon.transform.parent = pickupIconObject.transform.parent;

    }
    bool justPicked = false;
    void Update() { 
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (holding & !justPicked) {
                if (heldItem.GetComponent<Collider>() != null) {
                    heldItem.GetComponent<Collider>().enabled = true;
                }
                transform.parent.GetChild(1).GetComponent<Animator>().SetBool("Holding", false);
                heldItem.GetComponent<Rigidbody>().useGravity = true;
                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem.SetParent(null); 
                heldItem = null; 
                holding = false;
            }
        }

        if (heldItem != null) {
            pickupIcon.SetActive(false);
        }

        timer += Time.deltaTime * 2;
        justPicked = false;
    }

    float timer = 0;
    private void OnTriggerEnter(Collider other) {
        pickupIcon.transform.position = Camera.main.WorldToScreenPoint(other.transform.position) + new Vector3(0, 40, 0);
    }
    private void OnTriggerStay(Collider other) {
        
        if (other.tag == "Pickup") {
            Debug.LogError(other.name);
            if (!holding) { 
                pickupIcon.SetActive(true);
                
                pickupIcon.transform.position = Vector3.Lerp(pickupIcon.transform.position, Camera.main.WorldToScreenPoint(other.transform.position) + new Vector3(0,40 + Mathf.Sin(timer) * 5,0), Time.deltaTime);
            } else {
                pickupIcon.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Space) & !holding) {
                justPicked = true; 
                if (other.transform.gameObject.layer == LayerMask.NameToLayer("Pickup")) {
                    transform.parent.GetChild(1).GetComponent<Animator>().SetBool("Holding", true);
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
    private void OnTriggerExit(Collider other) {
        pickupIcon.SetActive(false);
    }
}
