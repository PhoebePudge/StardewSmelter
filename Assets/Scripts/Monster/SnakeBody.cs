using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour {
    public float Gap = .3f;
    public int length = 10;
    public float maxSize = .3f;
    public float minSize = .1f;
    public GameObject BodyPart;
    public List<GameObject> BodyParts = new List<GameObject>();
    public List<Vector3> previousPoints = new List<Vector3>();
    public List<float> bodyHeights = new List<float>();
    void Start() {
        //add in body parts behind head
        for (int i = 0; i < length; ++i) {
            previousPoints.Add(transform.position + ((Vector3.forward * i) * Gap));
        }

        //instanciate each body part
        for (int i = 0; i < length; ++i) {
            GameObject body = GameObject.Instantiate(BodyPart);
            body.transform.localScale = Vector3.Lerp(new Vector3(maxSize, maxSize, maxSize), new Vector3(minSize, minSize, minSize), i * .1f);
            body.transform.position = previousPoints[i]; 
            BodyParts.Add(body);
        }

        //add in body heights
        for (int i = 0; i < length; ++i) {
            bodyHeights.Add(Mathf.Sin(i) / 10f);
        }
    }

    private Vector3 Round(Vector3 vector3, int decimalPlaces = 2) {
        //round vector positions to a specific decimal place
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++) {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }
    int o = 0;
    void Update() {
        //if the body is too far away, update every part of the point to the next step
        if (Vector3.Distance(transform.position, previousPoints[0]) > Gap) {
            o++;
            previousPoints.Insert(0, transform.position);
            previousPoints.RemoveAt(previousPoints.Count - 1);
            for (int i = 0; i < length; ++i) {
                bodyHeights[i] = Mathf.Sin(i + o) / 10f;
            }
        }
        for (int i = 0; i < BodyParts.Count; i++) {

            //update each position on the body
            if (i == 0) {
                BodyParts[i].transform.LookAt(transform.position);
            } else {
                BodyParts[i].transform.LookAt(previousPoints[i - 1]);
            }
            BodyParts[i].transform.position = Vector3.Lerp(BodyParts[i].transform.position, previousPoints[i] + new Vector3(Mathf.Sin(i) / 7f, bodyHeights[i], 0), Time.deltaTime * 10f);
        }
    }
    private void OnDestroy() {
        //destroy all parts on destroy
        foreach (var item in BodyParts) {
            Destroy(item);
        }
    }
}
