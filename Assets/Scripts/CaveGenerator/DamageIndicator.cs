using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageIndicator : MonoBehaviour {

    static DamageIndicator instance;
    void Start() {
        //set instance and hide child
        instance = this;
        instance.transform.GetChild(0).gameObject.SetActive(false);
    } 
    public static void DisplayDamage(Vector3 position, float damage) {
        instance.StartCoroutine(instance.CreateInstance(position, damage));
    }
    IEnumerator CreateInstance(Vector3 position, float damage) {
        //create a text mesh pro from the child as reference
        GameObject instance = GameObject.Instantiate(transform.GetChild(0).gameObject);
        instance.transform.SetParent(transform, false);
        instance.SetActive(true);

        //set damage to current damage
        instance.GetComponent<TextMeshProUGUI>().text = damage.ToString();
         
        //move up with waves
        for (int i = 0; i < 40; i++) {
            instance.transform.position = position + new Vector3(Mathf.Cos(i), i, 0);
            yield return new WaitForSeconds(.1f);
        }
        //destroy it
        Destroy(instance); 
    }
}