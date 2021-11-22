using UnityEngine;
using UnityEngine.EventSystems;

//Starts the UI on Start-Up
class UIBootstrapper
{
    //Commenting this out so it doesnt load the UI when others are testing in Unity.
    //[RuntimeInitializeOnLoadMethod]
    static void Initalise()
    {
        GameObject gO = new GameObject();
        gO.name = "UI Controller";
        gO.AddComponent<UIHandler>();
        gO.AddComponent<EventSystem>();
        gO.AddComponent<StandaloneInputModule>();
    }
}

//Handles all UI Elements
public class UIHandler : MonoBehaviour
{
    //States
    public enum UIEVENTS
    {
        MAIN_MENU,
        SETTINGS,
        PLAY,
        PAUSE,
        INVENTORY
    }
    [SerializeField] UIEVENTS e_currentUIEvent = UIEVENTS.MAIN_MENU;
    [SerializeField] UIEVENTS e_oldUIEvent = UIEVENTS.SETTINGS;
    public UIEVENTS events
    {
        set { e_currentUIEvent = value; }
        get { return e_currentUIEvent; }
    }

    //UI Variables
    [SerializeField] GameObject gO_currentUIElement;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for any changes
        if(e_currentUIEvent != e_oldUIEvent)
        {
            //Clears old UI
            Destroy(gO_currentUIElement);

            //Creates New UI from Prefab
            switch (e_currentUIEvent)
            {
                case UIEVENTS.MAIN_MENU:
                {
                    gO_currentUIElement = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Main Menu"));
                    break;
                }
                case UIEVENTS.SETTINGS:
                {
                    gO_currentUIElement = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Settings"));
                    break;
                }
                case UIEVENTS.PLAY:
                {
                    gO_currentUIElement = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Play"));
                    break;
                }
                case UIEVENTS.PAUSE:
                {
                    gO_currentUIElement = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Pause"));
                    break;
                }
                case UIEVENTS.INVENTORY:
                {
                    gO_currentUIElement = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Inventory/Inventory"));
                    break;
                }
            }

            //Sets Parent
            gO_currentUIElement.transform.parent = transform;
            e_oldUIEvent = e_currentUIEvent;
        }
    }
}
