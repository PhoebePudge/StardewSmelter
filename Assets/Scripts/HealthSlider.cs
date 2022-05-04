using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    }
    void Update()
    { 
        gameObject.GetComponent<Slider>().value = Mathf.RoundToInt(value * segments) / (float)segments;
    }
    
} 
