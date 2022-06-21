using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour
{
    [SerializeField] GameObject optionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        optionCanvas.SetActive(false);
        Screen.SetResolution(702, 360, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            optionCanvas.SetActive(!optionCanvas.activeInHierarchy);
        }
    }
    public void resolution2880x1440()
    {
        Screen.SetResolution(2880, 1440, false);
    }
    public void resolution720x360() 
    {
        Screen.SetResolution(702, 360, false);

    }
    public void resolution1440x720()
    {
        Screen.SetResolution(1440, 720, false);
    }
}
