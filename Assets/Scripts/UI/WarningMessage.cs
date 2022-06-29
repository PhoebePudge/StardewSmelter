using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WarningMessage : MonoBehaviour {
    //component instance
    private static WarningMessage instance;
    void Start() {
        //if we have an instance, return
        if (instance != null) {
            return;
        }
        instance = this;

        //deactivate children instances
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }

        //disable image component
        instance.GetComponent<Image>().enabled = false;
    }
    public static void SetWarningMessage(string Title, string MessageText) {
        //set all its children to active
        foreach (Transform child in instance.transform) {
            child.gameObject.SetActive(true);
        }

        //enable image component for background
        instance.GetComponent<Image>().enabled = true;

        //update our text
        instance.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Title;
        instance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = MessageText;

        //follow cursor
        instance.StartCoroutine(instance.FollowCursor());
    }
    IEnumerator FollowCursor() {
        //follow cursor for 100 fixed updates
        gameObject.transform.position = Input.mousePosition;
        for (int i = 0; i < 100; i++) { 
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Input.mousePosition, Time.deltaTime * 2);
            yield return new WaitForFixedUpdate();
        }

        //disabled all children
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        instance.GetComponent<Image>().enabled = false;
    }
}
