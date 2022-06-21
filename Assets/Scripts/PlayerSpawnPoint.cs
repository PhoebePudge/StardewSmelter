using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnPoint : MonoBehaviour
{ 
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        { 
            GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        }
    } 
}
