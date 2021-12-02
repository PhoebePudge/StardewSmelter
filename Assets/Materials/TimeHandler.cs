using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Material skybox;
    void Start()
    {
        skybox = RenderSettings.skybox;
    }

    // Update is called once per frame
    float time;
    void Update()
    {
        time += Time.deltaTime * 0.2f;
        if (time >= 1) {
            time = 0;
        }
        skybox.SetFloat("_GradientTime", time);
    }
}
