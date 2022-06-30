using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoaderHelper : MonoBehaviour {
    public static ResourceLoaderHelper Instance;

    void Awake() {
        if (Instance == null) { 
            Instance = this;
        }
    }

    public static Sprite LoadResource(string spritepath) {
        if (Instance == null) {
            return null;
        }
        return Instance.loadResource(spritepath);
    }
    public Sprite loadResource(string spritepath) {
        Debug.LogError("loaded " + spritepath);
        return Resources.Load<Sprite>(spritepath);
    }
}
