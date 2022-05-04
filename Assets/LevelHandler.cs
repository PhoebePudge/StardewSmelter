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
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError(collision.gameObject.name); 
        if (collision.gameObject.name == "Player")
        {
            GameObject.FindGameObjectWithTag("CaveGenerator").GetComponent<CaveGenerator>().IncreaseLevel();
        }
        
    }
}
