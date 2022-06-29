using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoof : MonoBehaviour {
    //object references
    [SerializeField] GameObject Roof;
    [SerializeField] GameObject Beams;

    //hide when inside
    private void OnTriggerEnter(Collider other) {
        Roof.SetActive(false);
        Beams.SetActive(false);
    }

    //show when outside
    private void OnTriggerExit(Collider other) {
        Roof.SetActive(true);
        Beams.SetActive(true);
    }
}
