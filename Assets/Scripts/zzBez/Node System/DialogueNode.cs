using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueNode : BaseNode {


    //The order in which we place items within this code
    //Is the order they will appear in the node

    //Giving our entry and exit points to our node
    [Input] public int entry;
    [Output] public int exit;
    //[Output] public int exit;
    //Basic inputs for our conversations and speakers
    public string speakerName;
    public string dialogueLine;
    public Sprite sprite;

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