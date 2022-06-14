using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SmelteryButton : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick()
    {
        SmelteryDisplayPanel.SelectedMetalIndex = index;
        SmelteryDisplayPanel.UpdatePanel = true;
        Debug.LogError("You clicked a button " + index);
    }
}
