using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BloodParticle : MonoBehaviour{
    public static BloodParticle bloodParticle;
    public static RectTransform DamageIndicator;
    public static void CreateSplatter(Vector3 position, int damage) {
        DamageIndicator = GameObject.FindGameObjectWithTag("DamageIndicator").GetComponent<RectTransform>();
        bloodParticle.transform.position = position;
        bloodParticle.SetIndicator(position,damage);
        foreach (Transform item in bloodParticle.transform) {
            item.GetComponent<ParticleSystem>().Play();
        }
    }
    // Start is called before the first frame update
    void Start() {
        GameObject.DontDestroyOnLoad(gameObject);
        bloodParticle = this;
        DamageIndicator = GameObject.FindGameObjectWithTag("DamageIndicator").GetComponent<RectTransform>();
        DamageIndicator.gameObject.SetActive(false);
    }
    private void SetIndicator(Vector3 position, int damage)
    {
        DamageIndicator.position = Camera.main.WorldToScreenPoint(position);
        StartCoroutine(DamageStuff());
        DamageIndicator.gameObject.GetComponent<TextMeshProUGUI>().text = damage.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DamageStuff()
    {
        DamageIndicator.gameObject.SetActive(true);

        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForEndOfFrame();
            DamageIndicator.transform.Translate(new Vector3(0, 1, 0));
        }
        DamageIndicator.gameObject.SetActive(false);
    }
}
