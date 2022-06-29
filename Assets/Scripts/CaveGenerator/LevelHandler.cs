using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            //generate new level when player walks into ladder
            GameObject.FindGameObjectWithTag("CaveGenerator").GetComponent<CaveGenerator>().IncreaseLevel();
        }

    }
}
