using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHotfix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    { 
        transform.localScale = new Vector3(100,100,100);
    }
}
