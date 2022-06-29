using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour {
    public bool setActive = false;
    [SerializeField] GameObject optionCanvas; 
    void Start() {
        optionCanvas.SetActive(false);
        resolution720x360();
    }
    public void ToggleActive() {
        //toggle option menu active
        setActive = !setActive;
    } 
    void Update() {
        //toggle option menu with key press
        if (setActive != optionCanvas.activeSelf) {
            optionCanvas.SetActive(setActive);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            setActive = !setActive;
        }
    }
    //resolutions
    public void resolution2880x1440() {
        Screen.SetResolution(2880, 1440, false);
    }
    public void resolution720x360() {
        Screen.SetResolution(702, 360, false);

    }
    public void resolution1440x720() {
        Screen.SetResolution(1440, 720, false);
    }
}
