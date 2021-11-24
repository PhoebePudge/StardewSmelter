using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform pTransform;
    private Vector3 offset;
    private float zoomOffset;
    // Start is called before the first frame update
    void Start()
    {
        offset = pTransform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        zoomOffset += Input.mouseScrollDelta.y;
        zoomOffset = Mathf.Clamp(zoomOffset, -3f, 3f);
        Vector3 scrollOffset = transform.forward * zoomOffset;
        transform.position = Vector3.Lerp(transform.position, (pTransform.position - offset) + scrollOffset, Time.deltaTime * 5);
         
        //transform.position = pTransform.position - offset;
    }
}
