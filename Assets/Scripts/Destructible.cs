using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    public string thisObject;

    private void Start()
    {
        thisObject = this.gameObject.name;
        //destructible = this.GameObject
    }

    private void DestroyObject()
    {
        if (thisObject == ("Rock1(Clone)"))
        {
            // Need to know where our list is
            //Instantiate()
        }
    }
}
