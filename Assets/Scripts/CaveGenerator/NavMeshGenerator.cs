using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour {
    //sort out some issues with the nav mesh generator
    public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    public void UpdateNavMesh() { 
        for (int j = 0; j < objectsToRotate.Length; j++) {
            objectsToRotate[j].localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        } 
        for (int i = 0; i < surfaces.Length; i++) {
            surfaces[i].BuildNavMesh();
        }
    } 
}
