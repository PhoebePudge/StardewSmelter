using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Profiling;
public class FBXCounter : MonoBehaviour
{
    int min;
    int max;

    ProfilerRecorder totalReservedMemoryRecorder;
    ProfilerRecorder gcReservedMemoryRecorder;
    ProfilerRecorder systemUsedMemoryRecorder;

    private void OnEnable()
    {
        totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
        gcReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        systemUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
    }
    void OnDisable()
    {
        totalReservedMemoryRecorder.Dispose();
        gcReservedMemoryRecorder.Dispose();
        systemUsedMemoryRecorder.Dispose();
    }
    // Start is called before the first frame update
    void Start()
    {
        min = (int)Time.deltaTime;
        max = (int)Time.deltaTime;

        InvokeRepeating("UpdateText", 0f, .2f);
    }
    bool toggleDisplay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            toggleDisplay = !toggleDisplay;
        }
    }
    // Update is called once per frame
    void UpdateText()
    {
        if (toggleDisplay)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().enabled = false;
            return;
        }
        string sb = "";
        if (totalReservedMemoryRecorder.Valid)
            sb += " \n" + ($"Total Reserved Memory: {totalReservedMemoryRecorder.LastValue}");
        if (gcReservedMemoryRecorder.Valid)
            sb += " \n" + ($"GC Reserved Memory: {gcReservedMemoryRecorder.LastValue}");
        if (systemUsedMemoryRecorder.Valid)
            sb += " \n" + ($"System Used Memory: {systemUsedMemoryRecorder.LastValue}"); 



        int current = (int)(1f / Time.unscaledDeltaTime); 
        int avgFrameRate = (int)(1f / Time.smoothDeltaTime);
        float currentMS = Mathf.Round((1000f / current) * 100f) / 100f;
        float avgMS = Mathf.Round((1000f / avgFrameRate) * 100f) / 100f;

        if (current > max)
        {
            max = (int)current;
        }
        if (current < min)
        {
            min = (int)current;
        }

        string text =
            "<color=green>FPS: " + current.ToString() + " [" + currentMS +  " MS ]" + "\n"
            + "AVG: " + avgFrameRate + " [" + avgMS + " MS ]" + " \n"
            + "<color=red>MIN: " +  min.ToString() + "<color=green> Max: " + max.ToString() 
            + "<color=yellow>" + sb
            + "<color=grey>\nOS: " + SystemInfo.operatingSystem 
            + "\nCPU: " + SystemInfo.processorType
            + "\nGPU: " + SystemInfo.graphicsDeviceName
            + "\nGPU: " + SystemInfo.graphicsDeviceType
            + "\nGPU: " + SystemInfo.graphicsShaderLevel
            + "\nRAM: " + SystemInfo.systemMemorySize
            + "\nSCR: " + Screen.currentResolution;
        gameObject.GetComponent<TextMeshProUGUI>().text = text; 
    }
}
