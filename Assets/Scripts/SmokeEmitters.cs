using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEmitters : MonoBehaviour {
    public Material material;
    float timer = 0;
    float timerTrigger = 2;
    void Update() {
        timer += Time.deltaTime;

        if (timer > timerTrigger) {
            timerTrigger = Random.Range(.1f, 2f);
            timer = 0;

            //create a cube
            GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //set its parent
            gm.transform.SetParent(transform);

            //set a default position
            gm.transform.position = transform.position;
            gm.transform.rotation = transform.rotation;

            //add movement component
            gm.AddComponent<SmokeParticle>();
             
            //choose a random tone for the material
            float tone = Random.value / 2 - .5f;
            gm.GetComponent<Renderer>().material = material;
            gm.GetComponent<Renderer>().material.color = new Color(tone, tone, tone);
             
            //choose a random scale
            float rnd = Random.Range(0.3f, 0.8f);
            gm.transform.localScale = new Vector3(rnd * .1f, rnd * .1f, rnd * .1f);
        }
    }
}

public class SmokeParticle : MonoBehaviour {
    float timer = 0;
    private void Update() {
        //move the smoke particles upwards and with a wave
        timer += Time.deltaTime;
        transform.Translate(new Vector3(-0.2f * Mathf.Sin(Time.deltaTime), 1 * Time.deltaTime, 0));

        if (timer > 4) {
            GameObject.Destroy(gameObject);
        }
    }
}
