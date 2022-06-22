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
    private float zoomOffset = 7f;

    [SerializeField] Transform cameraFocusPoint;
    [SerializeField] float distancetoFocusPoint = 5f;

    private Quaternion Origional;

    Vector3 prevVelocityOffset;

    Vector3 origionalDifference;

    private void Awake() {
        GameObject.DontDestroyOnLoad(gameObject);

        SetCamera();
    }

    void Update() {
        if (pTransform == null)
        {
            pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        } 

        zoomOffset -= Input.mouseScrollDelta.y; 
        zoomOffset = Mathf.Clamp(zoomOffset, 4f, 14f);
         
        GetComponent<Camera>().orthographicSize = zoomOffset;
        Camera.main.orthographicSize = zoomOffset;
          
        Vector3 destination = (pTransform.position - offset);
        transform.position = destination;
         
        SnapToCameraPixel();
    }
    void SnapToCameraPixel()
    {     
        Vector3 lowerLeft = Vector3.zero;
        Vector3 upperRight = new Vector3(320 / 12.86f, 180 / 12.86f, 0.00f); 

        Vector3 differenceinspace = lowerLeft - upperRight; 

        differenceinspace = Quaternion.Euler(45, 90, 0) * differenceinspace;

        Vector3 WorldToPixelAspect = new Vector3(Mathf.Abs(differenceinspace.x / 320), 0, Mathf.Abs(differenceinspace.z / 180));

        WorldToPixelAspect = (Quaternion.Euler(45, 90, 0) * WorldToPixelAspect);

        Vector3 newTransform = new Vector3(
            Mathf.RoundToInt(transform.position.x / WorldToPixelAspect.x) * WorldToPixelAspect.x,
            transform.position.y,
            Mathf.RoundToInt(transform.position.z / WorldToPixelAspect.z) * WorldToPixelAspect.z);

        transform.position = newTransform;
    }

    void SetCamera()
    {
        Vector3 upperRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        origionalDifference = lowerLeft - upperRight;
        if (pTransform == null)
        {
            pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        offset = pTransform.position - transform.position;
        Origional = transform.rotation;
    }
}
