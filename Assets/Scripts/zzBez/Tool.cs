using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

	public void OnTriggerEnter(Collider collision) {
		
		if (collision.gameObject.tag == "Mineable") {
			Debug.Log("finally");
			//Destroy(this.gameObject);			
		}
	}
}
