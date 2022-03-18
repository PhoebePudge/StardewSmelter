using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{

	public string objectName;

	[SerializeField]
	private GameObject pickup;

	[SerializeField]
	private GameObject destroyEffect;

	// Start is called before the first frame update
	void Start()
    {
		objectName = this.gameObject.name;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OnTriggerEnter(Collider collision) {
    //	Destroy(this.gameObject);
    //}
    private void OnTriggerEnter(Collider other) { 
		Destroy(this.gameObject);
		if (other.gameObject.tag == "ToolCollider") {
			Destroy(this.gameObject);
		}
	}
}
