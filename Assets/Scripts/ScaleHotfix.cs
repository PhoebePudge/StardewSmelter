using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHotfix : MonoBehaviour {
    //lock scale due to animator issue
    private void LateUpdate() {
        transform.localScale = new Vector3(100, 100, 100);
    }
}
