using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthSlider : MonoBehaviour
{
    static int segments = 10;
    [Range(0, 1)] public static float value;
    // Start is called before the first frame update
    void Start()
    {
        value = 1;
    }

    // Update is called once per frame
    public static void Damage(int amount)
    {
        value = value - ((float)amount / (float)segments);
        DamageIndicator.DisplayDamage(Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position), amount);
        //BloodParticle.CreateSplatter(GameObject.FindGameObjectWithTag("Player").transform.position, amount);
    }
    void Update()
    { 
        gameObject.GetComponent<Slider>().value = Mathf.RoundToInt(value * segments) / (float)segments;

        if (value <= 0)
        {
            //StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    { 
        WarningMessage.SetWarningMessage("You have died", "Returning you to the main menu, please play again");

        yield return new WaitForSeconds(1);

        Application.Quit();
    }
    
} 
