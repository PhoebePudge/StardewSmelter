using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdIK : MonoBehaviour
{
    [SerializeField] Transform Wing1;
    [SerializeField] Transform Wing2;
    [SerializeField] Transform Tail;

    float index = 0;

    [SerializeField] float Aplitude = 5f;
    [SerializeField] float WingOffset = .5f;
    [SerializeField] float TailAmplitude = 2f;
    [SerializeField] float Omega = 5f;

    void Update() { 
        //A wave animation effect for wings with IK
        index += Time.deltaTime;

        float y = Mathf.Abs(Aplitude * Mathf.Sin(Omega * index));
        Wing1.transform.localPosition = new Vector3(2f, WingOffset - ((Aplitude / 2) - y), 0); 
        Wing2.transform.localPosition = new Vector3(-2f, WingOffset - ((Aplitude / 2) - y), 0);

        float TailY = Mathf.Abs(TailAmplitude * Mathf.Sin(Omega * index));
        Tail.transform.localPosition = new Vector3(0, ((TailAmplitude / 2) - TailY), -2.4f);
    }
}
