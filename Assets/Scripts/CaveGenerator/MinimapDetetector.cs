using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDetetector : MonoBehaviour {
    public static List<GameObject> EnemyList = new List<GameObject>(); 
    private void OnTriggerEnter(Collider other) {
        //add monsters to enemy list when entered
        if (other.GetComponent<MonsterType>()) {
            EnemyList.Add(other.gameObject);
        } 
    }
    private void OnTriggerExit(Collider other) {
        //remove monsters to enemy list
        if (other.GetComponent<MonsterType>()) {
            EnemyList.Remove(other.gameObject);
        }
    }
}
