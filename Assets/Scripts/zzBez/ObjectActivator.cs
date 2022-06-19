using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectActivator : MonoBehaviour
{
    public GameObject toActivate1;
    public GameObject toActivate2;

    public ActivatorSwitches aSwitches;

    public GameObject dialogueSwitch;
    
    public GameObject dialogueCanvas;
    
    private void Start()
    { 
        dialogueSwitch = GameObject.FindGameObjectWithTag("ObjectActivator");
        aSwitches = dialogueSwitch.gameObject.GetComponent<ActivatorSwitches>();

        dialogueCanvas.SetActive(true);
        if (aSwitches.demoHasBeenActivated == true)
        {
            toActivate1.SetActive(true);
            aSwitches.demoHasBeenActivated = true;
            toActivate2.SetActive(false);
        }
        if (aSwitches.demoHasBeenActivated == false)
        {
            toActivate2.SetActive(true);
            toActivate1.SetActive(false);                      
        }
    }
}
