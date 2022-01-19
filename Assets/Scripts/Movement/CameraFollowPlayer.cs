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
        Vector3 velocityOffset = pTransform.GetComponent<Rigidbody>().velocity / 5f;

        Vector3 destination = (pTransform.position - offset) + scrollOffset;

        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 5);

        Quaternion onLook = Origional;
        if (cameraFocusPoint != null) {

            Vector3 AveragePoint = (pTransform.position + cameraFocusPoint.transform.position) / 2;
            if (Vector3.Distance(transform.position, cameraFocusPoint.transform.position) <= distancetoFocusPoint) {
                onLook = Quaternion.LookRotation(AveragePoint - transform.position);
            }

        }

        transform.rotation = Quaternion.Slerp(transform.rotation, onLook, Time.deltaTime);
        prevVelocityOffset = velocityOffset;
    }
}
