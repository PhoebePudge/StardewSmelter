using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWorldCamera : MonoBehaviour
{
    private float zoomOffset;
    [SerializeField] float cameraDistance = 10f;
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform playerPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.Lerp(
            Quaternion.LookRotation(rotationPoint.position - transform.position),
            transform.rotation,
            Time.deltaTime);

        transform.rotation = rotation;

        zoomOffset += Input.mouseScrollDelta.y;
        zoomOffset = Mathf.Clamp(zoomOffset, -10f, 10f);
        Vector3 scrollOffset = transform.forward * zoomOffset;


        gameObject.transform.position = 
            Vector3.Lerp( 
                playerPoint.transform.position + (transform.forward * -cameraDistance) + scrollOffset,
                gameObject.transform.position,
                Time.deltaTime);
    }
}
