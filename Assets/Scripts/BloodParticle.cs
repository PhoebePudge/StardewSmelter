using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BloodParticle : MonoBehaviour {
    public static BloodParticle bloodParticle;
    public static void CreateSplatter(Vector3 position, int damage) {
        //create a splatter at a specific position
        bloodParticle.transform.position = position;
        bloodParticle.SetIndicator(position, damage);
        foreach (Transform item in bloodParticle.transform) {
            //play children particle systems
            item.GetComponent<ParticleSystem>().Play();
        }
    }
    void Start() {
        GameObject.DontDestroyOnLoad(gameObject);
        bloodParticle = this;
    }
    private void SetIndicator(Vector3 position, int damage) {
        //set damage indicator
        StartCoroutine(DamageStuff());
    }
    IEnumerator DamageStuff() {
        for (int i = 0; i < 50; i++) {
            yield return new WaitForEndOfFrame();
        }
    }
}
