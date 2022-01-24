using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ObjectActivatorBootstrapper
{
    [RuntimeInitializeOnLoadMethod]
    static void Initalise()
    {
        GameObject gO = new GameObject();
        gO.name = "Object Activator";
        gO.AddComponent<ActivatorSwitches>();
        gO.tag = "ObjectActivator";
    }
}

public class ActivatorSwitches : MonoBehaviour
{

    public bool demoHasBeenActivated;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Invoke("hitSwitch", 3);
    }

    private void hitSwitch()
    {
        demoHasBeenActivated = true;
    }

}
