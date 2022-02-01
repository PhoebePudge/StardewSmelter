using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEmitters : MonoBehaviour
{
    public Material material;
    float timer = 0;
    float timerTrigger = 2;
    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (timer > timerTrigger) {
            timerTrigger = Random.Range(.1f, 2f);
            timer = 0;
             
            GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);

            gm.transform.SetParent(transform);

            gm.transform.position = transform.position;
            gm.transform.rotation = transform.rotation;
            gm.AddComponent<SmokeParticle>();

            float tone = Random.value / 2 - .5f;
            gm.GetComponent<Renderer>().material = material;
            gm.GetComponent<Renderer>().material.color = new Color(tone, tone, tone);

            float rnd = Random.Range(0.3f, 0.8f);
            gm.transform.localScale = new Vector3(rnd * .1f, rnd * .1f, rnd * .1f);
        }
    }
}

public class SmokeParticle : MonoBehaviour{
    float timer = 0;
    private void Update() {
        timer += Time.deltaTime;
        transform.Translate(new Vector3(-0.2f * Mathf.Sin(Time.deltaTime), 1 * Time.deltaTime, 0));

        if (timer > 4) {
            GameObject.Destroy(gameObject);
        }
    }
}
