using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairGenerator : MonoBehaviour
{
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
        for (int i = 1; i < stepAmount + 1; i++) {
            GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gm.name = "Step" + i;
            float height = Mathf.Lerp(transform.position.y - endPoint.position.y, 0, i / (float)stepAmount);

            gm.transform.localScale = new Vector3(Mathf.Abs(length / stepAmount), Mathf.Abs(height), Mathf.Abs(width)); 
            gm.transform.position = new Vector3(
                Mathf.Lerp(transform.position.x - ((length / stepAmount) / 2f), endPoint.position.x, i / (float)stepAmount),
                Mathf.Lerp(transform.position.y, endPoint.position.y, i / (float)stepAmount) - (height / 2f),
                transform.position.z + (width / 2f));
            gm.transform.SetParent(transform); 

            gm.GetComponent<Renderer>().material = new Material( material); 
            gm.GetComponent<Renderer>().material.color = Color.Lerp(startLerp, endLerp, (float)i / (float)stepAmount);
            gm.tag = "Stair";
        }
        if (rotate)
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }
    private void OnDrawGizmos() {
        /*
        Quaternion rotation = Quaternion.Euler(0, -45, 0);

        Vector3 a1 = transform.position;
        Vector3 a2 = new Vector3(transform.position.x, transform.position.y, endPoint.transform.position.z);

        Vector3 b1 = new Vector3(transform.position.x, endPoint.position.y, transform.position.z);
        Vector3 b2 = new Vector3(transform.position.x, endPoint.position.y, endPoint.position.z);

        Vector3 c1 = new Vector3(endPoint.position.x, endPoint.position.y, transform.position.z);
        Vector3 c2 = endPoint.transform.position;

        Gizmos.color = Color.black;

        


        Mesh m = new Mesh();

        m.vertices = new Vector3[6] {
            //0
            a1, 
            //1
            a2, 

            //2
            b1, 
            //3
            b2, 

            //4
            c1, 
            //5
            c2
        };
         
        m.triangles = new int[] {
            //Frount
            //c1, a1, c2
            4, 0, 5,
            //a1, a2, c2
            0, 1, 5,

            //Sides
            //a2, b2, c2
            1, 3, 5,
            //a1, c1, b1
            0, 4, 2,

            //Back
            //a2, a1, b1
            1, 0, 2,
            //b2, a2, b1
            3, 1, 2
        };

        m.RecalculateNormals();

        Gizmos.color = Color.white;
        Gizmos.DrawMesh(m, Vector3.zero , Quaternion.Euler(0, -45, 0)); 

        */
    }

}
