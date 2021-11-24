using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleConsole : MonoBehaviour
{
    public static bool displayed = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
        gameObject.GetComponent<Image>().enabled = !gameObject.GetComponent<Image>().enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            displayed = !displayed; 
            gameObject.transform.GetChild(0).gameObject.SetActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
            gameObject.GetComponent<Image>().enabled = !gameObject.GetComponent<Image>().enabled;
        }
    }
}
