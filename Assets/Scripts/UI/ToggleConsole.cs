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
        for (int i = 0; i < transform.childCount; i++) { 
            transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
        }

        gameObject.GetComponent<Image>().enabled = !gameObject.GetComponent<Image>().enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            displayed = !displayed;
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
            }
            gameObject.GetComponent<Image>().enabled = !gameObject.GetComponent<Image>().enabled;
        }
    }
}
