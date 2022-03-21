using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
    }
}
