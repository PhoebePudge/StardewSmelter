using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

	public GameObject inventoryGameObject;
	public InventorySystem inventory;

	public int objectIndexNumber;

	private void Start() {
		inventoryGameObject = GameObject.FindGameObjectWithTag("InventorySystem");
		inventory = inventoryGameObject.GetComponent<InventorySystem>();
	}

	private void Update()
	{
        this.gameObject.transform.Rotate(0, 1, 0, Space.Self);
	}

	public void OnTriggerEnter(Collider collision) {

		if (collision.gameObject.tag == "Player") {
			inventory.itemPickedUp = objectIndexNumber;
			inventory.Pickup();
			Destroy(this.gameObject);
		}
	}

}
