using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCastController : MonoBehaviour
{
    [Range(0f, 1f)] public float progress;
    private Vector3 origin;
    [SerializeField] float offset = 1f;
    // Start is called before the first frame update
    void Start()
    {
        origin = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = Vector3.Lerp(origin, new Vector3(origin.x, origin.y + offset, origin.z), progress);
    }
}
