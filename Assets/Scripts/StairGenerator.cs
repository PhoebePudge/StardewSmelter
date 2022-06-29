using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairGenerator : MonoBehaviour {
    public Transform endPoint;
    public Material material;
    public Color startLerp;
    public Color endLerp;
    int stepAmount = 15;
    public bool rotate = true;
    void Awake() { 
        List<Transform> stepArr = new List<Transform>();

        float length = endPoint.position.x - transform.position.x;
        float width = endPoint.position.z - transform.position.z;

        //generate through each step
        for (int i = 1; i < stepAmount + 1; i++) {

            //create a cube
            GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gm.name = "Step" + i;

            //set its height
            float height = Mathf.Lerp(transform.position.y - endPoint.position.y, 0, i / (float)stepAmount);

            //set its local scale and position based on step information
            gm.transform.localScale = new Vector3(Mathf.Abs(length / stepAmount), Mathf.Abs(height), Mathf.Abs(width));
            gm.transform.position = new Vector3(
                Mathf.Lerp(transform.position.x - ((length / stepAmount) / 2f), endPoint.position.x, i / (float)stepAmount),
                Mathf.Lerp(transform.position.y, endPoint.position.y, i / (float)stepAmount) - (height / 2f),
                transform.position.z + (width / 2f));

            //set its parent
            gm.transform.SetParent(transform);

            //set its material and colour
            gm.GetComponent<Renderer>().material = new Material(material);
            gm.GetComponent<Renderer>().material.color = Color.Lerp(startLerp, endLerp, (float)i / (float)stepAmount);
            gm.tag = "Stair";
        }
        if (rotate)
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }
}
