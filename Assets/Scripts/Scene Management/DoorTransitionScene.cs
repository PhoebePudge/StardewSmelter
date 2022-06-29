using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransitionScene : MonoBehaviour {
    [SerializeField] string entrance;

    private void OnTriggerEnter(Collider other) {
        //transition to specified scene on trigger enter
        if (other.gameObject.name == "Player") {

            other.gameObject.transform.position = Vector3.zero;
            SceneManager.LoadScene(entrance);

        } else {
            return;
        }
    }
}
