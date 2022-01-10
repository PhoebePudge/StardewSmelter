using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.UI;

[CreateAssetMenu]
public class DialogueOptionsNode : BaseNode
{
    //The order in which we place items within this code
    //Is the order they will appear in the node

    //Giving our entry and exit points to our node
    [Input] public int entry;
    [Output] public int exit1;
    [Output] public int exit2;

    //[Output] public int exit;
    //Basic inputs for our conversations and speakers

    public string speakerName;
    public string dialogueLine;

    public Sprite sprite;
    public string answer1;
    public string answer2;

    

    public override string GetString()
    {
        // give us our chosen node use "/" for splitting data later
        return "DialogueNode/" + speakerName + "/" + dialogueLine + "/" + answer1 + "/" + answer2;
    }
    //Override our original object pocket 
    public override Sprite GetSprite()
    {
        return sprite;
    }
    //My add
    //public override Button GetButton1()
    //{
    //    return button1;
    //}
    //public override Button GetButton2()
    //{
    //    return button2;
    //}
}