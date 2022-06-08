using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoof : MonoBehaviour
{
    [SerializeField] GameObject Roof;
    [SerializeField] GameObject Beams;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
    private void OnTriggerEnter(Collider other)
    {
        Roof.SetActive(false);
        Beams.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        Roof.SetActive(true);
        Beams.SetActive(true);
    }
}
