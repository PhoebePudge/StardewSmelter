using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [Header("Volume Variables", order = 0)]
    [SerializeField] Slider s_Master;
    [SerializeField] Slider s_Music;
    [SerializeField] Slider s_SFX;
    [SerializeField] Slider s_Voices;

    [SerializeField] AudioMixer am_Master;

    [Header("Video Variables", order = 1)]
    [SerializeField] Dropdown d_Resolutions;
    List<Resolution> r_sortedList;
    bool b_isFullscreen;
    //int i_screenWidth;
    //int i_screenHeight;

    UIHandler uiH;
    
    //Loads the Values from Player Data or Default
    void Start()
    {
        uiH = transform.parent.gameObject.GetComponent<UIHandler>();

        //Grabs Volume Data from Player Prefs
        if (PlayerPrefs.HasKey("masterVolume")) { s_Master.value = PlayerPrefs.GetFloat("masterVolume"); } else { s_Master.value = 1; }
        s_Master.onValueChanged.AddListener(delegate { VolumeChange("master"); });

        if (PlayerPrefs.HasKey("musicVolume")) { s_Music.value = PlayerPrefs.GetFloat("musicVolume"); } else { s_Music.value = 1; }
        s_Music.onValueChanged.AddListener(delegate { VolumeChange("music"); });

        if (PlayerPrefs.HasKey("sfxVolume")) { s_SFX.value = PlayerPrefs.GetFloat("sfxVolume"); } else { s_SFX.value = 1; }
        s_SFX.onValueChanged.AddListener(delegate { VolumeChange("sfx"); });

        if (PlayerPrefs.HasKey("voicesVolume")) { s_Voices.value = PlayerPrefs.GetFloat("voicesVolume"); } else { s_Voices.value = 1; }
        s_Voices.onValueChanged.AddListener(delegate { VolumeChange("voices"); });

        //Grabs Video Data from Player Perfs
        if(PlayerPrefs.HasKey("fullscreen")) { int num = PlayerPrefs.GetInt("fullscreen"); if (num == 1) { b_isFullscreen = true; } else { b_isFullscreen = false; } } else { b_isFullscreen = false; }
        //if(PlayerPrefs.HasKey("width")) { i_screenWidth = PlayerPrefs.GetInt("width"); } else { i_screenWidth = Screen.width; }
        //if(PlayerPrefs.HasKey("height")) { i_screenWidth = PlayerPrefs.GetInt("height"); } else { i_screenWidth = Screen.height; }

        ResolutionAdder();
    }

    //Changes Volume
    public void VolumeChange(string tag)
    {
        switch (tag)
        {
            case "master":
            {
                am_Master.SetFloat("masterVolume", (Mathf.Log10(s_Master.value) * 20));
                PlayerPrefs.SetFloat("masterVolume", s_Master.value);
                break;
            }
            case "music":
            {
                am_Master.SetFloat("musicVolume", (Mathf.Log10(s_Music.value) * 20));
                PlayerPrefs.SetFloat("musicVolume", s_Music.value);
                break;
            }
            case "sfx":
            {
                am_Master.SetFloat("sfxVolume", (Mathf.Log10(s_SFX.value) * 20));
                PlayerPrefs.SetFloat("sfxVolume", s_SFX.value);
                break;
            }
            case "voices":
            {
                am_Master.SetFloat("voicesVolume", (Mathf.Log10(s_Voices.value) * 20));
                PlayerPrefs.SetFloat("voicesVolume", s_Voices.value);
                break;
            }
        }
    }

    //Adds all resolutions at 60hz to the Dropdown
    public void ResolutionAdder()
    {
        d_Resolutions.ClearOptions();
        Resolution[] resolutions = Screen.resolutions;
        r_sortedList = new List<Resolution>();
        List<string> options = new List<string>();
        int currentRes = 0;

        for (int i = 0; i < resolutions.Length; ++i)
        {
            if(resolutions[i].refreshRate == 60)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRate;
                options.Add(option);
                r_sortedList.Add(resolutions[i]);

                if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentRes = i;
                }
            }
        }

        d_Resolutions.AddOptions(options);
        d_Resolutions.value = currentRes;
        d_Resolutions.RefreshShownValue();
    }

    //Switches the Resolution
    public void ResolutionSwitcher(int index)
    {
        Screen.SetResolution(r_sortedList[index].width, r_sortedList[index].height, b_isFullscreen);
        PlayerPrefs.SetInt("width", r_sortedList[index].width);
        PlayerPrefs.SetInt("height", r_sortedList[index].height);
    }

    public void Fullscreen(bool state)
    {
        Screen.fullScreen = state;

        if (state == true)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }
    }

    //Main Menu Switcher
    public void MainMenu()
    {
        uiH.events = UIHandler.UIEVENTS.MAIN_MENU;
    }
}
