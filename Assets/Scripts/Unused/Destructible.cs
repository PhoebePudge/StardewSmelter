using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour { 
    public string objectName; 
    [SerializeField] private GameObject pickup; 
    [SerializeField] private GameObject destroyEffect; 
    private void Start() {
        objectName = this.gameObject.name; 
    } 
    private void DestroyObject() {
        Vector3 spawnFromGround = new Vector3(0, 1); 
        Destroy(this.gameObject);
        Instantiate(pickup, transform.position + spawnFromGround, transform.rotation);
        Instantiate(destroyEffect, transform.position, transform.rotation);
    }

    public void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.name == "Pickaxe") {
            DestroyObject();
        }
    }

    private void OnDestroy() {
        Invoke("RemoveEffect", 1);
    }

    private void RemoveEffect() {
        Destroy(destroyEffect);
    }
}
