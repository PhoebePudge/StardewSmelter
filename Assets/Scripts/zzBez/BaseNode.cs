using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}