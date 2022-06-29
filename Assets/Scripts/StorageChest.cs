using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StorageChest : MonoBehaviour {
    public Transform pivotPoint;
    public static List<Slot> StorageSlots;
    public static StorageChest instance = null;

    void Start() {
        //if we dont have any storage slots
        if (StorageSlots == null) {

            //set up the data to create one
            StorageSlots = new List<Slot>();
            foreach (Transform item in gameObject.transform.GetChild(0).transform) {
                StorageSlots.Add(item.GetComponent<Slot>());
            }
            instance = this;
        } else {

            //destroy this instance because we already have one
            instance.pivotPoint = pivotPoint;
            Destroy(this);
        }
    }
    //a lerp from small to big, called to increase size when opening
    IEnumerator LerpSize(int a, int b) {
        float time = 0f;
        while (time < b) {
            time += 0.05f;
            float progress = Mathf.Lerp(a, b, time);
            transform.localScale = new Vector3(progress, progress, time);
            yield return new WaitForSeconds(0.005f);
        }
    }
    //a lerp from big to small, called to decrease size when closing
    IEnumerator DecreaseLerp(int a, int b) {
        float time = 0f;
        while (time < a) {
            time += 0.1f;
            float progress = Mathf.Lerp(a, b, time);
            transform.localScale = new Vector3(progress, progress, time);
            yield return new WaitForSeconds(0.005f);
        }

        //disabled child objects
        foreach (Transform child in gameObject.transform) {
            child.gameObject.SetActive(false);
        }
    }
    void Update() {
        //if we have a pivot point
        if (pivotPoint != null) {

            //if we are in distance to player
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 2) {
                
                //when we switch to being active
                if (!gameObject.transform.GetChild(0).gameObject.activeInHierarchy) {

                    //call scale animation
                    StartCoroutine(LerpSize(0, 1));

                    //set stuff to active
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    //set animator to open
                    pivotPoint.transform.GetChild(0).GetComponent<Animator>().SetBool("Open", true);

                    //call colour change code
                    pivotPoint.GetChild(0).GetComponent<ColourChange2>().ColourChange(true);
                    pivotPoint.GetChild(0).transform.GetChild(1).GetComponent<ColourChange2>().ColourChange(true);
                }
            } else {

                //if we switch to being inactive
                if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy) {

                    //call decrease animation
                    StartCoroutine(DecreaseLerp(1, 0));

                    //deactivate objects
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);

                    //do close animation
                    pivotPoint.transform.GetChild(0).GetComponent<Animator>().SetBool("Open", false);

                    //call desaturate colour change code
                    pivotPoint.GetChild(0).GetComponent<ColourChange2>().ColourChange(false);
                    pivotPoint.GetChild(0).transform.GetChild(1).GetComponent<ColourChange2>().ColourChange(false);
                }
            }
        }
    }
}
