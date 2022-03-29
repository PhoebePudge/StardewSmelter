using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour {
    public Transform heldItem;
    public bool holding = false;

    [SerializeField] GameObject pickupIconObject;
    private static GameObject pickupIcon = null;
    private void Start() { 
            pickupIcon = Instantiate(pickupIconObject);
            pickupIcon.SetActive(false);
            pickupIcon.transform.parent = pickupIconObject.transform.parent; 

    } 

    private void pickup() {
        Debug.Log("picked up");
        if (pick != null) {
            transform.parent.GetChild(1).GetComponent<Animator>().SetBool("Holding", true);
            holding = true;
            heldItem = pick.transform;
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
    private void drop() {
        Debug.LogError("Dropped");
        if (heldItem != null)
        {
            if (heldItem.GetComponent<Collider>() != null)
            {
                heldItem.GetComponent<Collider>().enabled = true;
            }
            transform.parent.GetChild(1).GetComponent<Animator>().SetBool("Holding", false);
            heldItem.GetComponent<Rigidbody>().useGravity = true;
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.SetParent(null);
        }
        heldItem = null;
        holding = false;
        
    }
    void Update() {
        if (pick != null)
        {
            float distance = Vector3.Distance(pick.transform.position, transform.position);
            if (distance < 2f)
            {
                pickupIcon.SetActive(true);
                pickupIcon.transform.position = Vector3.Lerp(pickupIcon.transform.position, Camera.main.WorldToScreenPoint(pick.transform.position) + new Vector3(0, 40 + Mathf.Sin(timer) * 5, 0), Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.LogError("Object is picked up");
                    pickup();
                }
            }
            else
            {
                pickupIcon.SetActive(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) { 
            drop();
        }  
        timer += Time.deltaTime * 2; 
    }

    float timer = 0;
    GameObject pick;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (other.transform.gameObject.tag == "Pickup")
        {
            Debug.Log("Found pickup object");
            pick = other.gameObject;
        }

        pickupIcon.transform.position = Camera.main.WorldToScreenPoint(other.transform.position) + new Vector3(0, 40, 0);
    }
    private void OnTriggerExit(Collider other)
    {
        pick = null;
        pickupIcon.SetActive(false);
    }
    private void OnTriggerStay(Collider other) {
        if (!holding)
        {
            //pickupIcon.SetActive(true);
            //pickupIcon.transform.position = Vector3.Lerp(pickupIcon.transform.position, Camera.main.WorldToScreenPoint(other.transform.position) + new Vector3(0, 40 + Mathf.Sin(timer) * 5, 0), Time.deltaTime);
        }
        else
        {
            //pickupIcon.SetActive(false);
        }

        /*
        if (other.tag == "Pickup") {
            Debug.LogError(other.name);

            if (!holding) { 
                pickupIcon.SetActive(true);
                pickupIcon.transform.position = Vector3.Lerp(pickupIcon.transform.position, Camera.main.WorldToScreenPoint(other.transform.position) + new Vector3(0,40 + Mathf.Sin(timer) * 5,0), Time.deltaTime);
            } else {
                pickupIcon.SetActive(false);
            }

            Debug.Log(Input.GetKeyDown(KeyCode.Space));
            Debug.Log(holding);

            if (Input.GetKeyDown(KeyCode.Space) & !holding) {
                Debug.Log("s");
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
        }*/
    } 
}
