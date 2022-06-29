using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ReturnToSurfaceButton : MonoBehaviour {
    void Update() {
        //if we are in cave
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            //set buttons to true
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);

            //set its level data to reflect the current one
            transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level: " + GameObject.FindGameObjectWithTag("CaveGenerator").GetComponent<CaveGenerator>().currentLevel.ToString();
        } else {

            //set buttons to false
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void ReturnButtonPress() {
        //load blacksmith on button press
        SceneManager.LoadScene(1);
    }
}
