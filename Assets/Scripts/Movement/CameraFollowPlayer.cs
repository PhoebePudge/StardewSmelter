using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform pTransform;

    [SerializeField] RectTransform reference;
    [SerializeField] Canvas canvasRef;
    private Vector3 offset;
    private float zoomOffset;

    [SerializeField] Transform cameraFocusPoint;
    [SerializeField] float distancetoFocusPoint = 5f;

    private Quaternion Origional;

    Vector3 prevVelocityOffset;

    Vector3 origionalDifference;

    private void Awake() {

        Vector3 upperRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        origionalDifference = lowerLeft - upperRight;
        if (pTransform == null)
        {
            pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        offset = pTransform.position - transform.position;
        Origional = transform.rotation;
        //transform.rotation = Quaternion.Euler(45, 0, 0); 
    }

    void Update() { 
        zoomOffset += Input.mouseScrollDelta.y;
        zoomOffset = Mathf.Clamp(zoomOffset, -3f, 3f);

        Vector3 scrollOffset = transform.up * zoomOffset;
        Vector3 velocityOffset = Vector3.zero;

        if (pTransform.GetComponent<Rigidbody>() != null) { 
            velocityOffset = pTransform.GetComponent<Rigidbody>().velocity / 5f;
        }

        Vector3 destination = (pTransform.position - offset);// + scrollOffset;

        //transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 5);
        transform.position = destination;

        Quaternion onLook = Origional;
        if (cameraFocusPoint != null) {

            Vector3 AveragePoint = (pTransform.position + cameraFocusPoint.transform.position) / 2;
            if (Vector3.Distance(transform.position, cameraFocusPoint.transform.position) <= distancetoFocusPoint) {
                onLook = Quaternion.LookRotation(AveragePoint - transform.position);
            }
        }


        /*

        //Vector3 upperRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector3 upperRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //Vector3 upperRight = lowerLeft + origionalDifference;

        Vector3 differenceinspace = lowerLeft - upperRight;

        Vector3 WorldToPixelAspect = new Vector3(
            Mathf.Abs(differenceinspace.x / 320),
            Mathf.Abs(differenceinspace.y / 200),
            Mathf.Abs(differenceinspace.z / 200));

        Vector3 newTransform = new Vector3(
            (Mathf.RoundToInt(transform.position.x / WorldToPixelAspect.x) * WorldToPixelAspect.x),
            (Mathf.RoundToInt(transform.position.y / WorldToPixelAspect.y) * WorldToPixelAspect.y),
            transform.position.z);
            //(Mathf.RoundToInt(transform.position.z / WorldToPixelAspect.z) * WorldToPixelAspect.z)); 

        transform.position = newTransform;


        */
        //transform.rotation = Quaternion.Euler(45, 0, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, onLook, Time.deltaTime);
        prevVelocityOffset = velocityOffset;
    }
}
