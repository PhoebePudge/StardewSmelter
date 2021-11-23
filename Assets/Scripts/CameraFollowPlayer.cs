using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform pTransform;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = pTransform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pTransform.position - offset, Time.deltaTime * 5);
        //transform.position = pTransform.position - offset;
    }
}
