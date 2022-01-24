using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransitionScene : MonoBehaviour
{
    [SerializeField] string entrance;

    public GameObject interactCanvas;
    
    private void OnTriggerEnter(Collider other) {
         
        if (other.gameObject.name == "Player") { 
            interactCanvas.SetActive(true);
            SceneManager.LoadScene(entrance);
        }
        else
        {
            interactCanvas.SetActive(false);
        }
    }
}
