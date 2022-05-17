using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDetetector : MonoBehaviour
{
    public static List<GameObject> EnemyList = new List<GameObject> ();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MonsterType>())
        { 
            EnemyList.Add(other.gameObject);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MonsterType>())
        { 
            EnemyList.Remove(other.gameObject);
        }
    }
}
