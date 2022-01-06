using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    UIHandler uiH;
    GameManager gM;

    [SerializeField] GameObject continueButton;

    //void Start()
    //{
    //    uiH = transform.parent.gameObject.GetComponent<UIHandler>();
    //    gM = GameManager.Instance;

    //    if (gM.LoadData()) { continueButton.SetActive(true); }
    //}

    public void NewGame()
    {
        //PlaceHolder for now
        gM.InitaliseData();
        SceneManager.LoadScene(1);
        uiH.events = UIHandler.UIEVENTS.PLAY;
    }

    public void ContinueGame()
    {
        gM.SceneChanger(gM.ReturnIntData(GameManager.PlayerDataAttributes.Scene));
        uiH.events = UIHandler.UIEVENTS.PLAY;
    }

    public void Settings()
    {
        uiH.events = UIHandler.UIEVENTS.SETTINGS;
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
