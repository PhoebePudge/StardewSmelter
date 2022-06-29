using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    UIHandler uiH;
    GameManager gM;
    
    [Header("Health", order = 0)]
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject healthBarFill;
    [SerializeField] Text healthBarText;

    // Start is called before the first frame update
    void Start()
    {
        uiH = transform.parent.gameObject.GetComponent<UIHandler>();
        gM = GameManager.Instance;

        healthBar.maxValue = gM.ReturnIntData(GameManager.PlayerDataAttributes.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Controls
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiH.events = UIHandler.UIEVENTS.PAUSE;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            uiH.events = UIHandler.UIEVENTS.INVENTORY;
        }

        if (gM.ReturnIntData(GameManager.PlayerDataAttributes.CurrentHealth) > 0) 
        {
            if (!healthBarFill.activeInHierarchy) { healthBarFill.SetActive(true); }

            healthBar.value = gM.ReturnIntData(GameManager.PlayerDataAttributes.CurrentHealth);
            healthBarText.text = gM.ReturnIntData(GameManager.PlayerDataAttributes.CurrentHealth).ToString() + " / " + gM.ReturnIntData(GameManager.PlayerDataAttributes.MaxHealth).ToString();
        }
        else { healthBarFill.SetActive(false); }
    }
}
