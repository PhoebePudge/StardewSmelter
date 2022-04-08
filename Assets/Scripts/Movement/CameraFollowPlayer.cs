using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform pTransform;

    private Vector3 offset;
    private float zoomOffset;

    [SerializeField] Transform cameraFocusPoint;
    [SerializeField] float distancetoFocusPoint = 5f;

    private Quaternion Origional;
    Vector3 prevVelocityOffset;

    private void Awake() { 

        offset = pTransform.position - transform.position;
        Origional = transform.rotation;

    }

    void Update() {

        zoomOffset += Input.mouseScrollDelta.y;
        zoomOffset = Mathf.Clamp(zoomOffset, -3f, 3f);

        Vector3 scrollOffset = transform.forward * zoomOffset;
        Vector3 velocityOffset = Vector3.zero;

        if (pTransform.GetComponent<Rigidbody>() != null) { 
            velocityOffset = pTransform.GetComponent<Rigidbody>().velocity / 5f;
        }

        Vector3 destination = (pTransform.position - offset) + scrollOffset;

        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 5);

        Quaternion onLook = Origional;
        if (cameraFocusPoint != null) {

            Vector3 AveragePoint = (pTransform.position + cameraFocusPoint.transform.position) / 2;
            if (Vector3.Distance(transform.position, cameraFocusPoint.transform.position) <= distancetoFocusPoint) {
                onLook = Quaternion.LookRotation(AveragePoint - transform.position);
            }

        }
        //320
        //Debug.Log(320 / transform.position.x); 
        // Debug.Log(200 / transform.position.z);
        //200

        //camera width
        Vector3 screenSizeWorldSpace = Camera.main.ScreenToWorldPoint( new Vector3(Camera.main.pixelWidth, 0, Camera.main.pixelHeight));
        Vector3 WorldToPixelAspect = new Vector3(screenSizeWorldSpace.x / 320, 0, screenSizeWorldSpace.z / 200);
        Debug.Log(transform.position);
        Debug.Log(WorldToPixelAspect);
        Vector3 TransformedAspect  = new Vector3(transform.position.x / WorldToPixelAspect.x, 0, transform.position.y / WorldToPixelAspect.z);
        Debug.LogWarning(transform.position);
        Debug.LogWarning(TransformedAspect);
        Vector3 RoundedTransformedAspect = new Vector3(Mathf.RoundToInt( TransformedAspect.x), 0, Mathf.RoundToInt(TransformedAspect.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, onLook, Time.deltaTime);
        prevVelocityOffset = velocityOffset;
    }
}
