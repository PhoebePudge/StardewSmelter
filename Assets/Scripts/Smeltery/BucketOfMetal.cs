using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketOfMetal : MonoBehaviour
{
    public Metal oreType = null;
    public int oreQuantity;
    private int maxQuantity = 5;

    Transform child2;
    // Start is called before the first frame update
    void Start()
    {

        transform.GetComponent<MeshRenderer>().enabled = false;

        child2 = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
        child2.transform.SetParent(transform);
        child2.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
        child2.transform.localPosition = new Vector3(0, 0, -(oreQuantity / maxQuantity) * .55f);
        child2.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Destroy(child2.GetComponent<Collider>()); 
         
    }
    public void Check()
    {
        Debug.LogWarning("qqqqqqqqqqqqqqqqqq");
    }
    // Update is called once per frame
    void Update()
    {

        child2.GetComponent<MeshRenderer>().material = transform.GetComponent<MeshRenderer>().material;
    }
}
