using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    public string objectName;


    [SerializeField]
    private GameObject pickup;

    private void Start()
    {
        objectName = this.gameObject.name;
        //destructible = this.GameObject
    }

    private void DestroyObject()
    {
        if (objectName == ("default"))
        {
            // Need to know where our list is
            Destroy(this.gameObject);
            Instantiate(pickup, transform.position, transform.rotation);

        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        
        
        if (collision.gameObject.tag == "ToolCollider")
        {
            DestroyObject();
            Debug.Log("ToolCollision");
        }
    }

    //public void OnTriggerEnter(Collision collision)
    //{
        
    //}   
}
