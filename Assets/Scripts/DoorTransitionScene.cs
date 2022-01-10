using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransitionScene : MonoBehaviour
{
    [SerializeField] string entrance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
         
        if (other.gameObject.name == "Player") {
            Debug.Log("s");
            SceneManager.LoadScene(entrance);
        }
    }
}
