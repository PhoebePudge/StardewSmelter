using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    UIHandler uiH;
    GameManager gM;

    // Start is called before the first frame update
    void Start()
    {
        uiH = transform.parent.gameObject.GetComponent<UIHandler>();
        gM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        uiH.events = UIHandler.UIEVENTS.PLAY;
    }

    public void MainMenu()
    {
        gM.SaveData();
        gM.SceneChanger(0);
        uiH.events = UIHandler.UIEVENTS.MAIN_MENU;
    }
}
