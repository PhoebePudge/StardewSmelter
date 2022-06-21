using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StorageChest : MonoBehaviour
{
    public Transform pivotPoint;
    public static List<Slot> StorageSlots;
    public static StorageChest instance = null;
    void Start()
    {
        if (StorageSlots == null)
        {
            StorageSlots = new List<Slot>();
            foreach (Transform item in gameObject.transform.GetChild(0).transform)
            {
                StorageSlots.Add(item.GetComponent<Slot>());
            }
            instance = this;
        }
        else
        {
            instance.pivotPoint = pivotPoint;
            Destroy(this);
        }
    }
    IEnumerator LerpSize(int a, int b)
    {
        float time = 0f; 
        while (time < b)
        {
            time += 0.05f;
            float progress = Mathf.Lerp(a, b, time); 
            transform.localScale = new Vector3(progress, progress, time);
            yield return new WaitForSeconds(0.005f);
        }
    }
    IEnumerator DecreaseLerp(int a, int b)
    { 
        float time = 0f;  
        while (time < a)
        {
            time += 0.1f;
            float progress = Mathf.Lerp(a, b, time);
            transform.localScale = new Vector3(progress, progress, time);
            yield return new WaitForSeconds(0.005f);
        }

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    } 
    void Update()
    {
        if (pivotPoint != null)
        {   
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, pivotPoint.position) < 2)
            {
                if (!gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    StartCoroutine(LerpSize(0, 1));
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    pivotPoint.transform.GetChild(0).GetComponent<Animator>().SetBool("Open", true);

                    pivotPoint.GetChild(0).GetComponent<ColourChange2>().ColourChange(true);
                    pivotPoint.GetChild(0).transform.GetChild(1).GetComponent<ColourChange2>().ColourChange(true);
                }
            }
            else
            {
                if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    StartCoroutine(DecreaseLerp(1, 0));
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    pivotPoint.transform.GetChild(0).GetComponent<Animator>().SetBool("Open", false);


                    pivotPoint.GetChild(0).GetComponent<ColourChange2>().ColourChange(false);
                    pivotPoint.GetChild(0).transform.GetChild(1).GetComponent<ColourChange2>().ColourChange(false);
                }
            }
        }
    }
}
