using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthSlider : MonoBehaviour {
    static int segments = 10;
    [Range(0, 1)] public static float value;
    void Start() {
        value = 1;
    }
    public static void Damage(int amount) {
        //when damaged, update damage counter and display damage indicator
        value = value - ((float)amount / (float)segments);
        DamageIndicator.DisplayDamage(Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position), amount);
    }
    void Update() {
        //update slide value
        gameObject.GetComponent<Slider>().value = Mathf.RoundToInt(value * segments) / (float)segments;
        if (value <= 0) {
        }
    }
    IEnumerator Death() {
        //dealth sitation
        //disabled in this demo
        Debug.LogError("Stack here");
        WarningMessage.SetWarningMessage("You have died", "Returning you to the main menu, please play again");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
