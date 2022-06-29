using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoaderHelper : MonoBehaviour {
    public static ResourceLoaderHelper Instance;

    void Awake() {
        Instance = this;
    }

    public static Sprite LoadResource(string spritepath) {
        if (Instance == null) {
            return null;
        }
        return Instance.loadResource(spritepath);
    }
    public Sprite loadResource(string spritepath) {
        return Resources.Load<Sprite>(spritepath);
    }
}
