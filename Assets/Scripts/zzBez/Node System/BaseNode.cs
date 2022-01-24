using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class BaseNode : Node {

    //Overrideable data string to help decouple our script a bit
	public virtual string GetString()
    {
        return null;
    }
    //Overrideable sprite pocket 
    public virtual Sprite GetSprite()
    {
        return null;
    }

    //public virtual Button GetButton1()
    //{
    //    return null;
    //}
    //public virtual Button GetButton2()
    //{
    //    return null;
    //}
}