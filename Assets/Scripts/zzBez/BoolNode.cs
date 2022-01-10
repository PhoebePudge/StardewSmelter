using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class BoolNode : BaseNode
{

    //Testing out new node types
    //Giving our entry and exit points to our node
    [Input] public int entry;
    [Output] public int exit;
    //
    bool switch1;

    public override string GetString()
    {
        // give us our chosen node
        return "DialogueNode/" + speakerName + "/" + dialogueLine;
    }

    //Override our original object pocket 
    public override Sprite GetSprite()
    {
        return sprite;
    }
}
