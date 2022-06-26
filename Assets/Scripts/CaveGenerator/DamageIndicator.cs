using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageIndicator : MonoBehaviour {

    static DamageIndicator instance; 
    void Start()
    {
        instance = this;
        instance.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DisplayDamage(Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position), 4f);
        }
    }
    public static void DisplayDamage(Vector3 position, float damage)
    {
        instance.StartCoroutine(instance.CreateInstance(position, damage));
    }
    IEnumerator CreateInstance(Vector3 position, float damage)
    {
        Debug.LogError("Stack here");
        GameObject instance = GameObject.Instantiate(transform.GetChild(0).gameObject);
        instance.transform.SetParent(transform, false);
        instance.SetActive(true);
        instance.GetComponent<TextMeshProUGUI>().text = damage.ToString();

        for (int i = 0; i < 40; i++)
        {
            instance.transform.position = position + new Vector3(Mathf.Cos(i), i, 0);
            yield return new WaitForSeconds(.1f);
        }

        Destroy(instance);
         
    }
}
