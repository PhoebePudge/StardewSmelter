using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ReturnToSurfaceButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);

            transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level: " + GameObject.FindGameObjectWithTag("CaveGenerator").GetComponent<CaveGenerator>().currentLevel.ToString();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
         
    }

    public void ReturnButtonPress()
    {
        SceneManager.LoadScene(1);
    }
}
