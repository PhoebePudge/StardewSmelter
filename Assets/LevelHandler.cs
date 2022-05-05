using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) { 
        Debug.LogError(other.gameObject.name); 
        if (other.gameObject.name == "Player")
        {
            Debug.LogError("Calling level increase");
            GameObject.FindGameObjectWithTag("CaveGenerator").GetComponent<CaveGenerator>().IncreaseLevel();
        }
        
    }
}
