using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CameraFollowPlayer : MonoBehaviour {
    [SerializeField] Transform pTransform;

    [SerializeField] RectTransform reference;
    [SerializeField] Canvas canvasRef;
    private Vector3 offset;
    private Vector3 origionalOffset;
    private float zoomOffset = 7f;

    [SerializeField] Transform cameraFocusPoint;
    [SerializeField] float distancetoFocusPoint = 5f;

    private Quaternion Origional;

    Vector3 prevVelocityOffset;

    Vector3 origionalDifference;

    private void Awake() {
        //set to do not destroy
        GameObject.DontDestroyOnLoad(gameObject); 
        SetCamera(); 
    }
    private void OnLevelWasLoaded(int level) { 
        //set rotations and offset for specific scenes
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            transform.rotation = Quaternion.Euler(45, 45, 0);
            offset = origionalOffset + new Vector3(0,-2f,5f);
        } else {
            transform.rotation = Quaternion.Euler(45, 90, 0);
            offset = origionalOffset;
        }

    }
    void Update() {
        //find player transform if we dont have it
        if (pTransform == null) {
            pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //get zoom offset
        zoomOffset -= Input.mouseScrollDelta.y;
        zoomOffset = Mathf.Clamp(zoomOffset, 4f, 14f);

        //set orthographic size
        GetComponent<Camera>().orthographicSize = zoomOffset;
        Camera.main.orthographicSize = zoomOffset;

        //get destination position
        Vector3 destination = (pTransform.position - offset);
        transform.position = destination; 
    }
    float time = 0;
    public Vector3 newWorldToPixelAspect = new Vector3(0.09775105f, 0, 0.03092904f);
    void SnapToCameraPixel() {
        //Snap to pixel code
        //This only worked on specific rotations, which is why this code is not in use,
        //may be worth further research into this topic

        Vector3 lowerLeft = Vector3.zero;
        Vector3 upperRight = new Vector3(320 / 12.86f, 180 / 12.86f, 0.00f);

        Vector3 differenceinspace = lowerLeft - upperRight;

        differenceinspace = Quaternion.Euler(45, 90, 0) * differenceinspace;

        Vector3 WorldToPixelAspect = new Vector3(Mathf.Abs(differenceinspace.x / 320), 0, Mathf.Abs(differenceinspace.z / 180));

        WorldToPixelAspect = (Quaternion.Euler(45, 90, 0) * WorldToPixelAspect);


        WorldToPixelAspect = newWorldToPixelAspect;

        //time += Time.deltaTime;
        Vector3 newTransform = new Vector3(
            Mathf.RoundToInt(transform.position.x / WorldToPixelAspect.x) * WorldToPixelAspect.x + Mathf.Sin(time),
            transform.position.y,
            Mathf.RoundToInt(transform.position.z / WorldToPixelAspect.z) * WorldToPixelAspect.z);




        transform.position = newTransform;
    }

    void SetCamera() {
        //get viewport positions
        Vector3 upperRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        origionalDifference = lowerLeft - upperRight;
        if (pTransform == null) {
            pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //get offset
        offset = pTransform.position - transform.position;
        origionalOffset = offset;

        //set rotation
        Origional = transform.rotation;
    }
}
