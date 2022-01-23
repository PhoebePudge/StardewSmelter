using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairGenerator : MonoBehaviour
{
    public Transform endPoint;
    public Material material;
    int stepAmount = 15;
    void Awake() {
        List<Transform> stepArr = new List<Transform>();
        
        float length = endPoint.position.x - transform.position.x;
        float width = endPoint.position.z - transform.position.z;
        for (int i = 1; i < stepAmount + 1; i++) {
            GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gm.name = "Step" + i;
            float height = Mathf.Lerp(transform.position.y - endPoint.position.y, 0, i / (float)stepAmount);

            gm.transform.localScale = new Vector3(length / stepAmount, height, width); 
            gm.transform.position = new Vector3(
                Mathf.Lerp(transform.position.x - ((length / stepAmount) / 2f), endPoint.position.x, i / (float)stepAmount),
                Mathf.Lerp(transform.position.y, endPoint.position.y, i / (float)stepAmount) - (height / 2f),
                transform.position.z + (width / 2f));
            gm.transform.SetParent(transform);
            gm.GetComponent<Renderer>().material = material;
        } 
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, new Vector3(endPoint.position.x, endPoint.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, endPoint.position.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, endPoint.transform.position.z), endPoint.transform.position);
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, endPoint.transform.position.z), new Vector3(transform.position.x, transform.position.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(endPoint.transform.position.x, endPoint.transform.position.y, endPoint.transform.position.z), new Vector3(endPoint.transform.position.x, endPoint.transform.position.y, transform.position.z));
         
    }
}
