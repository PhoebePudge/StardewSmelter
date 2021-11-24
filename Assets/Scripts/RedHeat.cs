using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHeat : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other) {  
        Debug.LogError(other.gameObject.GetComponent<MeshRenderer>().material.name);
        if (other.gameObject.GetComponent<MeshRenderer>() != null) { 
            if (other.gameObject.GetComponent<MeshRenderer>().material.name.Contains("HotMetal")) {
                other.gameObject.GetComponent<MeshRenderer>().material.SetVector("_HotPoint", transform.position);
                other.gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Scale", gameObject.transform.localScale.x);
            }
        }
    }
}
