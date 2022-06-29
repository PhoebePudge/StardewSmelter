using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Profiling;
public class FPSCounter : MonoBehaviour {
    bool toggleDisplay = false;

    //min and max fps
    int min;
    int max;

    //profiler recorder for memory reading
    ProfilerRecorder totalReservedMemoryRecorder;
    ProfilerRecorder gcReservedMemoryRecorder;
    ProfilerRecorder systemUsedMemoryRecorder;
    private void OnEnable() {
        //start memory reading
        totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
        gcReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        systemUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
    }
    void OnDisable() {
        //dispose memory readers after use
        totalReservedMemoryRecorder.Dispose();
        gcReservedMemoryRecorder.Dispose();
        systemUsedMemoryRecorder.Dispose();
    } 
    void Start() {
        //set variable defaults
        min = (int)Time.deltaTime;
        max = (int)Time.deltaTime;

        //invoke update every .2f seconds
        InvokeRepeating("UpdateText", 0f, .2f);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            toggleDisplay = !toggleDisplay;
        }
    } 
    void UpdateText() {
        //toggle display
        if (toggleDisplay) {
            GetComponent<TextMeshProUGUI>().enabled = true;
        } else {
            GetComponent<TextMeshProUGUI>().enabled = false;
            return;
        }

        //get memory int a string
        string sb = "";
        if (totalReservedMemoryRecorder.Valid)
            sb += " \n" + ($"Total Reserved Memory: {totalReservedMemoryRecorder.LastValue}");
        if (gcReservedMemoryRecorder.Valid)
            sb += " \n" + ($"GC Reserved Memory: {gcReservedMemoryRecorder.LastValue}");
        if (systemUsedMemoryRecorder.Valid)
            sb += " \n" + ($"System Used Memory: {systemUsedMemoryRecorder.LastValue}");

        //get framerate 
        int current = (int)(1f / Time.unscaledDeltaTime);
        int avgFrameRate = (int)(1f / Time.smoothDeltaTime);

        //get ms
        float currentMS = Mathf.Round((1000f / current) * 100f) / 100f;
        float avgMS = Mathf.Round((1000f / avgFrameRate) * 100f) / 100f;

        //set max framerate
        if (current > max) {
            max = (int)current;
        }

        //set min framerate
        if (current < min) {
            min = (int)current;
        }

        //set string output
        string text =
            "<color=green>FPS: " + current.ToString() + " [" + currentMS + " MS ]" + "\n"
            + "AVG: " + avgFrameRate + " [" + avgMS + " MS ]" + " \n"
            + "<color=red>MIN: " + min.ToString() + "<color=green> Max: " + max.ToString()
            + "<color=yellow>" + sb
            + "<color=grey>\nOS: " + SystemInfo.operatingSystem
            + "\nCPU: " + SystemInfo.processorType
            + "\nGPU: " + SystemInfo.graphicsDeviceName
            + "\nGPU: " + SystemInfo.graphicsDeviceType
            + "\nGPU: " + SystemInfo.graphicsShaderLevel
            + "\nRAM: " + SystemInfo.systemMemorySize
            + "\nSCR: " + Screen.currentResolution;

        //update to text mesh pro
        gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }
}
