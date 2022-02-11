using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CraftingPanel : MonoBehaviour
{
    [SerializeField] GameObject basepoint;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(basepoint.transform.position); 
         
    }
}
