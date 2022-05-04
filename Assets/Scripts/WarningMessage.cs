using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WarningMessage : MonoBehaviour
{
    private static WarningMessage instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        instance.GetComponent<Image>().enabled = false;
    }  
    public static void SetWarningMessage(string Title, string MessageText)
    {
        foreach (Transform child in instance.transform)
        {
            child.gameObject.SetActive(true);
        }
        instance.GetComponent<Image>().enabled = true;

        instance.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Title;
        instance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = MessageText;
        instance.StartCoroutine(instance.FollowCursor());
    }
    IEnumerator FollowCursor()
    {
        gameObject.transform.position = Input.mousePosition;
        for (int i = 0; i < 100; i++)
        {
            Debug.Log("ss");
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Input.mousePosition, Time.deltaTime * 2);
            yield return new WaitForFixedUpdate();
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        instance.GetComponent<Image>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
