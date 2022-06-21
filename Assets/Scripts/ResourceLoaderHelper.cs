using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoaderHelper : MonoBehaviour
{
    public static ResourceLoaderHelper Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Sprite LoadResource(string spritepath)
    {
        if (Instance == null)
        {
            return null;
        }
        return Instance.loadResource(spritepath);
    }
    public Sprite loadResource(string spritepath)
    {
        return Resources.Load<Sprite>(spritepath); 
    }
}
