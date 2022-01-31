using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    private void Update()
    {
        this.gameObject.transform.Rotate(0, 5, 0, Space.Self);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            //add to player inventory
        }
    }
    
}
