using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWorldCamera : MonoBehaviour
{
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform playerPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(rotationPoint);
        gameObject.transform.position = playerPoint.transform.position + (transform.forward * -10);
    }
}
