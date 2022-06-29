using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Invoke("RemoveEffect", 1);
    }

    private void RemoveEffect() {
        Destroy(this.gameObject);
    }
}
