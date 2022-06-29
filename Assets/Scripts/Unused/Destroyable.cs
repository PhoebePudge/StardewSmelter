using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

	public string objectName; 
	[SerializeField] private GameObject pickup; 
	[SerializeField] private GameObject destroyEffect;
	 
	void Start() {
		objectName = this.gameObject.name;
	} 
	private void OnTriggerEnter(Collider other) {
		Destroy(this.gameObject);
		if (other.gameObject.tag == "ToolCollider") {
			Destroy(this.gameObject);
		}
	}
}
